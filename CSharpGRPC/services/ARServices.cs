using System;
using System.Collections.Generic;
using System.Text;

using Msg;
using Grpc.Core;

using System.Threading.Tasks;
using System.Linq;

namespace CSharpGRPC.services
{
    public class ARServices: Msg.MsgServices.MsgServicesBase
    {
        public override Task<Response> ConfigMap(Map request, ServerCallContext context)
        {
            /*
            this function receives the config map and returns the Response response.
            param request: this parameter is the config map received.
            TODO: please add the response of getting message of config map to your main program.
            the following code shows you how to operate the Data Structure Map
            */
            double roomwidth = request.Roomwidth;
            double roomheight = request.Roomheight;
            Console.WriteLine("room width: {0}\n", roomwidth);
            Console.WriteLine("room height: {0}\n", roomheight);
            Google.Protobuf.Collections.RepeatedField<Block> blocks = request.Blocks.Clone();
            foreach (Block block in blocks)
            {
                Block.Types.Type type = block.Type;
                string str = "";
                if (type == Block.Types.Type.Cube)
                {
                    str = "Cube";
                }
                if (type == Block.Types.Type.Cylinder)
                {
                    str = "Cylinder";
                }
                double w = block.W;
                double h = block.H;
                Point pos = block.Pos;
                Console.WriteLine("Block Type: {0}, w: {1}, h:{2}, posx: {3}, posy: {4}", str, w, h, pos.Posx, pos.Posy);
            }


            // the following code return the Response to the Client.
            // if the received request goes wrong, please modify Ok to Error.
            Response response = new Response();
            response.Status = Response.Types.Status.Ok;
            return Task.FromResult(response);
        }

        public override Task<Response> RobotPath(RBPath request, ServerCallContext context)
        {
            /*
             * this function receives the robot path and return the Response response.
             * param request contains the robot path message and starttime, endtime.
             * starttime and endtime may be zero, which means the client doesn't send them.
             * TODO: please add the response of getting message of robot path to your main program.
            the following code shows you how to operate the Data Structure RBPath
             */
            int starttime = request.Starttime;
            int endtime = request.Endtime;
            Console.WriteLine("start time: {0}, end time: {1}\n", starttime, endtime);
            Google.Protobuf.Collections.RepeatedField<Point> points = request.Pos.Clone();
            foreach (Point point in points)
            {
                double posx = point.Posx;
                double posy = point.Posy;
                Console.WriteLine("position x: {0}, position y: {1}", posx, posy);
            }

            // the following code return the Response to the Client.
            // if the received request goes wrong, please modify Ok to Error.
            Response response = new Response();
            response.Status = Response.Types.Status.Ok;
            return Task.FromResult(response);
        }

        public override Task<Response> ControlCommand(ControlCmd request, ServerCallContext context)
        {
            /*
             * this function receives the control command from control site and return the Response response.
             * param request: it contains the ControlCmd paremeter.
             * TODO: please add the reponse of message of getting control command to your main program.
             * the following code shows you how to operate the Data Structure ControlCmd
             */
            ControlCmd.Types.CtrlCmd cmd = request.Cmd;
            string str = "";
            if (cmd == ControlCmd.Types.CtrlCmd.Start)
            {
                str = "Start";
            }
            else if (cmd == ControlCmd.Types.CtrlCmd.Stop)
            {
                str = "Stop";
            }
            else if(cmd == ControlCmd.Types.CtrlCmd.Connect)
            {
                str = "Connect";
            }
            Console.WriteLine("received control command: {0}", str);
            // the following code return the Response to the Client.
            // if the received request goes wrong, please modify Ok to Error.
            Response response = new Response();
            response.Status = Response.Types.Status.Ok;
            return Task.FromResult(response);
        }

        public override Task<Response> VoiceResult(VoiceStr request, ServerCallContext context)
        {
            /*
             * this function receives the recognition result of voice file sent to robot site.
             * param request contains the string of voice recognition result.
             * TODO: please add the response of getting message of recognition to your main program.
             * the following code shows you how to operate the Data Structure VoiceStr
             */
            int timestamp = request.Timestamp;
            string voiceresult = request.Voice;
            Console.WriteLine("time stamp: {0}, voice recognition result: {1}", timestamp, voiceresult);
            // the following code return the Response to the Client.
            // if the received request goes wrong, please modify Ok to Error.
            Response response = new Response();
            response.Status = Response.Types.Status.Ok;
            return Task.FromResult(response);
        }
    }
}
