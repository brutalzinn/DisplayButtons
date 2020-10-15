using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ScribeBot.Engine.Wrappers
{
    [MoonSharpUserData]
    class SocketWrapper
    {

        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        private static SocketWrapper instance;

        public static SocketWrapper Instance
        {
            get
            {
                return instance;
            }
        }
  


        public SocketWrapper()
        {

            instance = this;

        }
       public  bool Status()
        {
            return !((clientSocket.Client.Poll(1000, SelectMode.SelectRead) && (clientSocket.Available == 0)) || !clientSocket.Connected);

        }

        public void Connect(string ip, int port)
        {
            try
            {
 clientSocket.Connect(ip, port);
            }
            catch(Exception eee)
            {
                Debug.WriteLine(eee);

            }
          

        }
        public  void Write(string msg)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
            NetworkStream stream = clientSocket.GetStream();

            stream.Write(data, 0, data.Length);

        }
        public  string Read()
        {
       
            NetworkStream stream = clientSocket.GetStream();

            Byte[]  data = new Byte[256];

            String responseData = String.Empty;

            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
      
            return responseData;
        }

    }
}
