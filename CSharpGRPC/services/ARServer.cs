using System;
using System.Collections.Generic;
using System.Text;

using Msg;
using Grpc.Core;

namespace CSharpGRPC.services
{
    class ARServer
    {
        const int Port = 8888;
        const string IpAddr = "localhost";
        public static void StartServer()
        {
            Server server = new Server
            {
                Services =
                {
                    MsgServices.BindService(new ARServices())
                },
                Ports =
                {
                    new ServerPort(IpAddr, Port, ServerCredentials.Insecure)
                }
            };
            server.Start();
            Console.WriteLine("Start AR Listen Server on port " + Port);
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }
    }
}
