using MinecraftServerRCON;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Engine.Wrappers
{
    [MoonSharpUserData]
    class MinecraftWrapper
    {

        public static void SendCommand(string ip,int port, string password, string command)
        {

            using (var rcon = RCONClient.INSTANCE)
            {
                rcon.setupStream(ip, port,password: password);
               var answer = rcon.sendMessage(RCONMessageType.Command, command);
                Console.WriteLine(answer.RemoveColorCodes());
            }
        }
    }
}
