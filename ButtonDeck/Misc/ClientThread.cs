using ButtonDeck.Backend.Networking.TcpLib;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

    namespace ButtonDeck.Misc
    {
        public class ClientThread
        {
            readonly Thread baseThread;

            public Thread BaseThread
            {
                get
                {
                    return baseThread;
                }
            }

            TcpClient tcpClient;

            public TcpClient TcpClient
            {
                get
                {
                    return tcpClient;
                }
            }

            public ClientThread()
            {
                baseThread = new Thread(RunServer);
            }

            public void Start()
            {
                baseThread?.Start();
            }

            private void RunServer()
            {
                tcpClient = new TcpClient("127.0.0.1", Constants.PORT_NUMBER);
            tcpClient.Start();

            }

            public void Stop()
            {
            tcpClient?.Stop();
                baseThread?.Abort();
            }
        }
    }


