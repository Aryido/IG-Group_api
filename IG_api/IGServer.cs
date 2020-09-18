using IGWebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WatsonWebsocket;

namespace IG_api
{
    class IGServer
    {
        static string _Hostname = "localhost";
        static int _Port = 9000;
        static string _ClientIpPort = null;
        

        public static WatsonWsServer wss;
        public bool connectingStatus = false;

        public IGServer()
        {
            wss = new WatsonWsServer(_Hostname, _Port, false);
            wss.ClientConnected += (s, e) =>
            {
                Console.WriteLine("Client connected: " + e.IpPort);
                connectingStatus = true;
                _ClientIpPort=e.IpPort;
            };

            wss.ClientDisconnected += (s, e) =>
            {
                Console.WriteLine("Client disconnected: " + e.IpPort);
                connectingStatus = false;
            };

            wss.MessageReceived += (s, e) =>
            {
                Console.WriteLine("Server message received from " + e.IpPort + ": " + Encoding.UTF8.GetString(e.Data));
                _ClientIpPort = e.IpPort;

            };

            wss.Start();
        }


        public void SendToClient(Dictionary<string, L1LsPriceData> dic, Dictionary<string, string> dicEpicAndTypeName, Dictionary<string, string> dicEpicAndInstName)
        {  

            if (_ClientIpPort!=null)
            {
                foreach (var kvp in dic)
                {
                    string[] arrayStr =kvp.Key.Split(':');
                    string epic = arrayStr[1];
                    string bid=kvp.Value.Bid.ToString();
                    string offer= kvp.Value.Offer.ToString();
                    string hight = kvp.Value.High.ToString();
                    string low = kvp.Value.Low.ToString();


                    string typeName = dicEpicAndTypeName[epic];
                    string InstructmentName = dicEpicAndInstName[epic];

                    StringBuilder sb = new StringBuilder();
                    string totalstr = epic+","+ bid + ","+ offer + ","+ hight + ","+ low +","+ typeName+","+ InstructmentName;
                    wss.SendAsync(_ClientIpPort, totalstr);                  
                }
           
            }
            //wss.SendAsync(_ClientIpPort, "11111");
        }
    }
}
