using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections;
using System.ServiceModel;
using ButtonDeck.Backend.Networking.Implementation;
using System.Diagnostics;

namespace ButtonDeck.Backend.Networking.TcpLib
{
    /// <summary>
    /// This class holds useful information for keeping track of each client connected
    /// to the server, and provides the means for sending/receiving data to the remote
    /// host.
    /// </summary>f
    public class ConnectionState
    {
        internal Guid connectionGuid = Guid.NewGuid();

        public Guid ConnectionGuid {
            get {
                return connectionGuid;
            }
        }

        internal Socket _conn;
        internal TcpServer _server;
        internal TcpClient _client;
        internal TcpServiceProvider _provider;
        internal byte[] _buffer;

        /// <summary>
        /// Tells you the IP Address of the remote host.
        /// </summary>
        public EndPoint RemoteEndPoint {
            get { return _conn.RemoteEndPoint; }
        }

        /// <summary>
        /// Returns the number of bytes waiting to be read.
        /// </summary>
        public int AvailableData {
            get { return _conn.Available; }
        }

        /// <summary>
        /// Tells you if the socket is connected.
        /// </summary>
        public bool Connected {
            get { return _conn.Connected; }
        }

        /// <summary>
        /// Reads data on the socket, returns the number of bytes read.
        /// </summary>
        public int Read(byte[] buffer, int offset, int count)
        {
            try {
                if (_conn.Available > 0)
                    return _conn.Receive(buffer, offset, count, SocketFlags.None);
                else return 0;
            } catch {
                return 0;
            }
        }

        /// <summary>
        /// Sends Data to the remote host.
        /// </summary>
        public bool Write(byte[] buffer, int offset, int count)
        {
            try {
                _conn.Send(buffer, offset, count, SocketFlags.None);
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Ends connection with the remote host.
        /// </summary>
        public void EndConnection()
        {
            if (_conn != null && _conn.Connected) {
                _conn.Shutdown(SocketShutdown.Both);
                _conn.Close();
            }
            _server.DropConnection(this);
        }
        public void EndConnection_Client()
        {
            if (_conn != null && _conn.Connected)
            {
                _conn.Shutdown(SocketShutdown.Both);
                _conn.Close();
            }
            _client.DropConnection(this);
        }
    }



    /// <summary>
    /// Allows to provide the server with the actual code that is goint to service
    /// incoming connections.
    /// </summary>
    public abstract class TcpServiceProvider : ICloneable
    {
        /// <summary>
        /// Provides a new instance of the object.
        /// </summary>
        public virtual object Clone()
        {
            throw new Exception("Derived clases must override Clone method.");
        }
        public abstract void OnRetryConnect(ConnectionState state);
        /// <summary>
        /// Gets executed when the server accepts a new connection.
        /// </summary>
        /// 
        public abstract void OnAcceptConnection(ConnectionState state);

        /// <summary>
        /// Gets executed when the server detects incoming data.
        /// This method is called only if OnAcceptConnection has already finished.
        /// </summary>
        public abstract void OnReceiveData(ConnectionState state);

        /// <summary>
        /// Gets executed when the server needs to shutdown the connection.
        /// </summary>
        public abstract void OnDropConnection(ConnectionState state);
    }



    public class TcpServer
    {
        private int _port;
        private Socket _listener;
        private TcpServiceProvider _provider;
        private readonly ArrayList _connections;

        public ArrayList Connections {
            get {
                return _connections;
            }
        }

        private int _maxConnections = 100;

        private AsyncCallback ConnectionReady;
        private WaitCallback AcceptConnection;
        private AsyncCallback ReceivedDataReady;

        /// <summary>
        /// Initializes server. To start accepting connections call Start method.
        /// </summary>
        public TcpServer(TcpServiceProvider provider, int port)
        {
            _provider = provider;
            _port = port;
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            _connections = new ArrayList();
            ConnectionReady = new AsyncCallback(ConnectionReady_Handler);
            AcceptConnection = new WaitCallback(AcceptConnection_Handler);
            ReceivedDataReady = new AsyncCallback(ReceivedDataReady_Handler);
        }


        /// <summary>
        /// Start accepting connections.
        /// A false return value tell you that the port is not available.
        /// </summary>
        public bool Start()
        {
            try {
                _listener.Bind(new IPEndPoint(IPAddress.Any, _port));
                _listener.Listen(100);
                _listener.BeginAccept(ConnectionReady, null);
                return true;
            } catch (Exception) {
                return false;
            }
        }


        /// <summary>
        /// Callback function: A new connection is waiting.
        /// </summary>
        private void ConnectionReady_Handler(IAsyncResult ar)
        {
            lock (this) {
                if (_listener == null) return;
                Socket conn = _listener.EndAccept(ar);
                if (_connections.Count >= _maxConnections) {
                    //Max number of connections reached.
                    string msg = "SE001: Server busy";
                    conn.Send(Encoding.UTF8.GetBytes(msg), 0, msg.Length, SocketFlags.None);
                    conn.Shutdown(SocketShutdown.Both);
                    conn.Close();
                } else {
                    //Start servicing a new connection
                    ConnectionState st = new ConnectionState
                    {
                        _conn = conn,
                        _server = this,
                        _provider = (TcpServiceProvider)_provider.Clone(),
                        _buffer = new byte[4]
                    };
                    _connections.Add(st);
                    //Queue the rest of the job to be executed latter
                    ThreadPool.QueueUserWorkItem(AcceptConnection, st);
                }
                //Resume the listening callback loop
               _listener.BeginAccept(ConnectionReady, null);
            }
        }


        /// <summary>
        /// Executes OnAcceptConnection method from the service provider.
        /// </summary>
        private void AcceptConnection_Handler(object state)
        {
            Debug.WriteLine("CALL WHEN STEPED");
            ConnectionState st = state as ConnectionState;
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            try { st._provider.OnAcceptConnection(st); } catch {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
                //report error in provider... Probably to the EventLog
            }
            //Starts the ReceiveData callback loop
            if (st._conn.Connected)
                st._conn.BeginReceive(st._buffer, 0, 0, SocketFlags.None,
                 new AsyncCallback(ReceivedDataReady_Handler), st);
        }


        /// <summary>
        /// Executes OnReceiveData method from the service provider.
        /// </summary>
        private void ReceivedDataReady_Handler(IAsyncResult ar)
        {
            try {
                ConnectionState st = ar.AsyncState as ConnectionState;
                st._conn.EndReceive(ar);
                //Im considering the following condition as a signal that the
                //remote host droped the connection.
                if (st._conn.Available == 0) DropConnection(st);
                else {
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
                    try { st._provider.OnReceiveData(st); } catch (Exception) {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
                        //report error in the provider
                    }
                    //Resume ReceivedData callback loop
                    if (st._conn.Connected)
                        st._conn.BeginReceive(st._buffer, 0, 0, SocketFlags.None,
                        ReceivedDataReady, st);
                }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            } catch (Exception) {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
            }
        }


        /// <summary>
        /// Shutsdown the server
        /// </summary>
        public void Stop()
        {
            lock (this) {
                _listener.Close();
                _listener = null;
                //Close all active connections
                foreach (object obj in _connections) {
                    ConnectionState st = obj as ConnectionState;
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
                    try { st._provider.OnDropConnection(st); } catch {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
                        //some error in the provider
                    }
                    try {
                        st._conn.Shutdown(SocketShutdown.Both);
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
                    } catch (Exception) {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
                    }
                    st._conn.Close();
                }
                _connections.Clear();
            }
        }


        /// <summary>
        /// Removes a connection from the list
        /// </summary>
        internal void DropConnection(ConnectionState st)
        {
            lock (this) {
                st._conn.Shutdown(SocketShutdown.Both);
                st._conn.Close();
                if (_connections.Contains(st))
                    _connections.Remove(st);
            }
        }


        public int MaxConnections {
            get {
                return _maxConnections;
            }
            set {
                _maxConnections = value;
            }
        }


        public int CurrentConnections {
            get {
                lock (this) { return _connections.Count; }
            }
        }
    }

    public class TcpClient
    {
        private int _port;

        private Socket _listener;
        private TcpServiceProvider _provider;
        private readonly ArrayList _connections;

        public ArrayList Connections
        {
            get
            {
                return _connections;
            }
        }

        private int _maxConnections = 100;

        private AsyncCallback ConnectionReady;
        private WaitCallback AcceptConnection;
        private AsyncCallback ReceivedDataReady;

        /// <summary>
        /// Initializes server. To start accepting connections call Start method.
        /// </summary>
        public TcpClient(TcpServiceProvider provider, int port)
        {
            _provider = provider;
            _port = port;
          //  _ip = ip;

            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _connections = new ArrayList();

            ConnectionReady = new AsyncCallback(ConnectionReady_Handler);
            AcceptConnection = new WaitCallback(AcceptConnection_Handler);
            ReceivedDataReady = new AsyncCallback(ReceivedDataReady_Handler);

        }


        /// <summary>
        /// Start accepting connections.
        /// A false return value tell you that the port is not available.
        /// </summary>
        public bool Start()
        {
            try
            {
           IPAddress ip_usable = IPAddress.Parse("127.0.0.1");
             //   IPAddress ipAddress = ipHostInfo.AddressList[0];  
           IPEndPoint remoteEP = new IPEndPoint(ip_usable, _port);
                // _listener.Connect(ip_usable, _port);
                //   _listener.Listen(1000) ;   
             //   _listener.Listen(100);
                _listener.BeginConnect(remoteEP,
                       ConnectionReady, _listener);
                //  NetworkPacketExtensions.SendPacket(_listener.,new UsbInteractPacket());
                //  _listener.send
                //      _listener.Bind(new IPEndPoint(IPAddress.Any, _port));
                // _listener.Listen(100);
                //   _listener.BeginAccept(ConnectionReady, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

      
        /// <summary>
        /// Callback function: A new connection is waiting.
        /// </summary>
        private void ConnectionReady_Handler(IAsyncResult ar)
        {
        
            lock (this)
            {

               
                IPAddress ip_usable = IPAddress.Parse("127.0.0.1");

                IPEndPoint remoteEP = new IPEndPoint(ip_usable, _port);

                Socket conn = (Socket)ar.AsyncState;

                // conn.EndConnect(ar);
            
                    //Start servicing a new connection
                    ConnectionState st = new ConnectionState
                    {
                        _conn = conn,
                        _client = this,
                        _provider = (TcpServiceProvider)_provider.Clone(),
                        _buffer = new byte[4]
                    };
                    _connections.Add(st);
                    //Queue the rest of the job to be executed latter
                    ThreadPool.QueueUserWorkItem(AcceptConnection, st);


            

            }
        }


        /// <summary>
        /// Executes OnAcceptConnection method from the service provider.
        /// </summary>
        private void AcceptConnection_Handler(object state)
        {
            Debug.WriteLine("Calling accept connection handler");
   
            ConnectionState st = state as ConnectionState;
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            try { st._provider.OnAcceptConnection(st); }
            catch
            {

             //   st._provider.OnRetryConnect(st);
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
                //report error in provider... Probably to the EventLog
            }
            //Starts the ReceiveData callback loop
            if (st._conn.Connected)
                Debug.WriteLine("Starts the ReceiveData callback loop");
            try
            {

            
  st._conn.BeginReceive(st._buffer, 0, 0, SocketFlags.None,
                ReceivedDataReady, st);
            }
            catch
            {


            }
            
          

            
          
        }


        /// <summary>
        /// Executes OnReceiveData method from the service provider.
        /// </summary>
        private void ReceivedDataReady_Handler(IAsyncResult ar)
        {
            try
            {
                Debug.WriteLine("REICEIVED DATRA READY");
                ConnectionState st = ar.AsyncState as ConnectionState;
           //st._conn.EndReceive(ar);
                //Im considering the following condition as a signal that the
                //remote host droped the connection.
                if (st._conn.Available == 0)
                {
                    Debug.WriteLine("drop connection");

                    //   Stop();
                    //st._provider.OnDropConnection(st);
                   st._provider.OnRetryConnect(st);
                DropConnection(st);
                }
                else
                {
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
                    try { st._provider.OnReceiveData(st); }
                    catch (Exception)
                    {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
                        //report error in the provider
                    }
                    //Resume ReceivedData callback loop
                    if (st._conn.Connected)
                        Debug.WriteLine("Resume ReceivedData callback loop");

                    st._conn.BeginReceive(st._buffer, 0, 0, SocketFlags.None,
                        ReceivedDataReady, st);
                }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            }
            catch (Exception)
            {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
            }
        }


        /// <summary>
        /// Shutsdown the server
        /// </summary>
        public void Stop()
        {
            lock (this)
            {
                _listener.Close();
                _listener = null;
                //Close all active connections
                foreach (object obj in _connections)
                {
                    ConnectionState st = obj as ConnectionState;
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
                    try {
                        st._provider.OnDropConnection(st);
                    }
                    catch
                    {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
                        //some error in the provider
                    }
                    try
                    {
                        st._conn.Shutdown(SocketShutdown.Both);
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
                    }
                    catch (Exception)
                    {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
                    }
                    st._conn.Close();
                }
                _connections.Clear();
            }
        }


        /// <summary>
        /// Removes a connection from the list
        /// </summary>
        internal void DropConnection(ConnectionState st)
        {
            lock (this)
            {
               
            st._conn.Shutdown(SocketShutdown.Both);
               st._conn.Close();
                if (_connections.Contains(st))
                    _connections.Remove(st);
      
            }
        }


        public int MaxConnections
        {
            get
            {
                return _maxConnections;
            }
            set
            {
                _maxConnections = value;
            }
        }


        public int CurrentConnections
        {
            get
            {
                lock (this) { return _connections.Count; }
            }
        }
    }
}