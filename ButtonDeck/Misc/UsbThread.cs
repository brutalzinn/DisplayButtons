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
    public class UsbThread
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

        public UsbThread()
        {
            baseThread = new Thread(RunServer);
        }

        public void Start()
        {
            baseThread?.Start();
        }

        private void RunServer()
        {
            //    tcpServer = new ButtonDeck.Backend.Usb.ReadWriteAsync(new DeckServiceProvider(), Constants.PORT_NUMBER);
            //  tcpServer.Start();
          //  ButtonDeck.Backend.Usb.ReadWriteAsync.MyUsbDevice.Open();
        //    ButtonDeck.Backend.Usb.ReadWriteAsync.MyUsbDevice.
        }

        public void Stop()
        {
            tcpServer?.Stop();
            baseThread?.Abort();
        }
    }
}
