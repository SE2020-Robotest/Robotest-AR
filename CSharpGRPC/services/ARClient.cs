using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Msg;
using Grpc.Core;
using System.Threading.Tasks;
using System.Linq;

namespace CSharpGRPC.services
{
    public class ARClient
    {
        public enum Receiver
        {
            AR = 0,
            CONTROL = 1,
            ROBOT = 2
        }

        Dictionary<Receiver, string> Ips = new Dictionary<Receiver, string>
        {
            {Receiver.AR, "localhost" },
            {Receiver.CONTROL, "localhost" },
            {Receiver.ROBOT, "localhost" }
        };
        Dictionary<Receiver, int> Ports = new Dictionary<Receiver, int>
        {
            {Receiver.AR, 8888 },
            {Receiver.CONTROL, 8888 },
            {Receiver.ROBOT, 8888 }
        };

        private string GetReceiverAddr(Receiver receiver)
        {
            string ip = Ips[receiver];
            int port = Ports[receiver];
            string address = ip + ":" + String.Format("{0}", port);
            return address;
        }

        public List<VoiceData> GenVoiceDataList(string filename)
        {
            List<VoiceData> voicedata = new List<VoiceData>();
            if (File.Exists(filename))
            {
                FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(file);
                byte[] byteArray = br.ReadBytes((int)file.Length);
                int totalsize = byteArray.Length;
                int buffersize = 1024 * 1024; // 1M
                int lastByteSize = totalsize % buffersize; //the last one byte size
                int loopTimes = totalsize / buffersize;
                for(int i = 0; i <= loopTimes; i++)
                {
                    Google.Protobuf.ByteString sbytes;
                    if(i == loopTimes)
                    {
                        sbytes = Google.Protobuf.ByteString.CopyFrom(byteArray, i * buffersize, lastByteSize);
                    }
                    else
                    {
                        sbytes = Google.Protobuf.ByteString.CopyFrom(byteArray, i * buffersize, buffersize);
                    }
                    VoiceData data = new VoiceData
                    {
                        File = sbytes
                    };
                    voicedata.Add(data);
                }
            }
            return voicedata;
        }

        public async Task<int> SendVoiceByte(Receiver receiver, List<VoiceData> voicedata)
        {
            /*
             * this function send voice file to the robot site.
             * return the response from server 0->OK, 1->Error
             */
            string address = GetReceiverAddr(receiver);
            var channel = new Channel(address, ChannelCredentials.Insecure);
            var client = new Msg.MsgServices.MsgServicesClient(channel);
            using(var call = client.SendVoiceFile())
            {
                foreach(var item in voicedata)
                {
                    await call.RequestStream.WriteAsync(item);
                }
                await call.RequestStream.CompleteAsync();

                // feedback response
                Response res = await call.ResponseAsync;
                if(res.Status == Response.Types.Status.Error)
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
