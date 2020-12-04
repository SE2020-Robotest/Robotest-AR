using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using Msg;
using Grpc.Core;

namespace CSharpGRPC.services
{
    class ARServer
    {
        const int Port = 8888;
        const string IpAddr = "localhost";
		static private bool _AR_SERVER_RUNNING = true;
		static private object locker = new object();
		
        public static void StartServer()
        {
			_AR_SERVER_RUNNING = true;
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
            //Console.WriteLine("Start AR Listen Server on port " + Port);
			//Debug.Log("Start AR Listen Server on port" + Port);
            while(_AR_SERVER_RUNNING){
			}
			//Console.ReadKey();
            server.ShutdownAsync().Wait();
			Debug.Log("AR Server Stopped");
        }
		
		public static void StopServer()
		{
			lock(locker)
			{
				_AR_SERVER_RUNNING = false;
			}
		}
    }
}
