using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
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
        private String msg;


        public SocketWrapper()
        {

            instance = this;

        }

    
        public static void Connect(string ip, int port)
        {

            instance.clientSocket.Connect(ip, port);

        }
        public static void Write()
        {
            NetworkStream serverStream = instance.clientSocket.GetStream();

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(instance.msg);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }
        public static string Read()
        {
            NetworkStream serverStream = instance.clientSocket.GetStream();
            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, (int)instance.clientSocket.ReceiveBufferSize);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            return returndata;
        }

    }
}
