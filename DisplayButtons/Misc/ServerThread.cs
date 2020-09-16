using DisplayButtons.Backend.Networking.TcpLib;
using DisplayButtons.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisplayButtons.Misc
{
    public class ServerThread
    {
        readonly Thread baseThread;

        public Thread BaseThread {
            get {
                return baseThread;
            }
        }

        TcpServer tcpServer;

        public TcpServer TcpServer {
            get {
                return tcpServer;
            }
        }

        public ServerThread()
        {
            baseThread = new Thread(RunServer);
        }

        public void Start()
        {
            baseThread?.Start();
        }

        private void RunServer()
        {
            tcpServer = new TcpServer(new DeckServiceProvider(), ApplicationSettingsManager.Settings.PORT);
            tcpServer.Start();
            
        }

        public void Stop()
        {
            tcpServer?.Stop();
            baseThread?.Interrupt();
        }
    }
}
