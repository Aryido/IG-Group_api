using dto.endpoint.browse;
using IGWebApiClient;
using SampleWPFTrader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG_api
{
    class MarketData
    {
          
        public Action<Dictionary<string, L1LsPriceData>> Updated;
               

        public static Dictionary<string, L1LsPriceData>  DataModelDict = new Dictionary<string, L1LsPriceData>();
        public Dictionary<string, string> MarketDataEpicsDict = new Dictionary<string, string>();
        public Dictionary<string, string> MarketDataInstrumentNameDict = new Dictionary<string, string>();
 
        public static List<string> ErrorMarketDataEpicsList= new List<string>();

        public List<HierarchyNode> hierarchyNodeList = new List<HierarchyNode>();

        public static FileProcessing fp = new FileProcessing();

        static IGStreamingApiClient streamClient = InitialiseClient.getIGStreamingApiClient();
        static IgRestApiClient restClient = InitialiseClient.getIgRestApiClient();

        IGServer igServer = new IGServer();


        public async Task MarketDataList()
        {           
            Task<Boolean> connectionEstablished = InitialiseClient.ConnectionStatus();           
            var connectionStatus= await connectionEstablished;

            if (connectionStatus)
            {
                
                if (fp.existFunction())
                {                  
                    MarketDataEpicsDict = fp.readEpicAndNodeNameFileTo();
                    MarketDataInstrumentNameDict = fp.readEpicAndInstrumentNameFileTo();
                    ErrorMarketDataEpicsList = fp.readErrorFileTo();
                }
                else
                {
                    GetMarketDataEpics(connectionStatus);
                }

                //var epics = new List<string> { "CS.D.BITCOIN.CFD.IP",  //Bitcoin
                //                               "CS.D.ETHUSD.CFD.IP",  //ETH

                //                               "IX.D.EMGMKT.IFD.IP",  //All
                //                               "IX.D.TAIEX.IFD.IP",
                //                               "IX.D.CANNABIS.IFD.IP",  //Cannabis Index
                //                               "IX.D.FANG.IFD.IP",  //NYSE Fang
                //                               "IX.D.StoxxBank.IFD.IP",  //other
                //                               "IX.D.FTSE.FWS2.IP",  //FTSE
                //                               "IX.D.FTSE.CFD.IP",
                //                               "IX.D.DOW.FWS.IP",  //Wall St.
                //                               "IX.D.DOW.IFD.IP",
                //                               "IX.D.DAX.FWS2.IP",  //Germany
                //                               "IX.D.DAX.IFD.IP",
                //                               "TM.D.MDAX.FWS2.IP",
                //                               "TM.D.MDAX.IFD.IP",
                //                               "TM.D.TECDAX.FWS2.IP",
                //                               "TM.D.TECDAX.IFD.IP",
                //                               "IX.D.SPTRD.FWS2.IP",  //US500
                //                               "IX.D.SPTRD.IFD.IP",
                //                               "IX.D.ASX.FWS2.IP",  //Australia
                //                               "IX.D.ASX.IFD.IP",
                //                               //"IX.D.XINHUA.IFD.IP",  //China A50(資料有缺失)
                //                               "IX.D.STXE.FWS2.IP",  //EU Stock50
                //                               "IX.D.STXE.IFD.IP",


                //                               "IX.D.HSCHIN.IFD.IP",// Hong Kong
                //                               "IX.D.HANGSENG.IFD.IP",

                //                               "IX.D.NIKKEI.IFD.IP"// Japan


                //                             };              

                RedisExchange redisExchange = new RedisExchange();
                bool rcs=redisExchange.RedisConnectionStatus();
                if (rcs)
                {
                    string value = "Hello World";
                    redisExchange.db.StringSet("Test", value);

                }

                var test = redisExchange.db.StringGet("Test");

                var listener = new MarketDetailsTableListerner();
                listener.Update += Listener_UpdateMarketData;

                //if (ErrorMarketDataEpicsList!=null && ErrorMarketDataEpicsList.Count!=0)
                //{
                //    foreach (string epicError in ErrorMarketDataEpicsList)
                //    {
                //        if (MarketDataEpicsDict.ContainsKey(epicError))
                //        {
                //            MarketDataEpicsDict.Remove(epicError);
                //        }
                //    }
                //}

                //try
                //{
                //    streamClient.SubscribeToMarketDetails(MarketDataEpicsDict.Keys.ToList(), listener);
                //}
                //catch (Exception exception)
                //{
                //    string a = "123";
                //}

                int i = 0;
                foreach (KeyValuePair<string, string> dict in MarketDataEpicsDict)
                {
                    try
                    {
                        streamClient.SubscribeToMarketDetails(new List<string> { dict.Key }, listener);
                    }
                    catch (Exception exception)
                    {
                        ErrorMarketDataEpicsList.Add(dict.Key);
                        fp.createErrorFile(ErrorMarketDataEpicsList);
                    }
                    i++;
                }
            }        
        }

        private void Listener_UpdateMarketData(object sender, UpdateArgs<L1LsPriceData> e)
        {

            L1LsPriceData DataModle = new L1LsPriceData();
           
            if (DataModelDict.TryGetValue(e.ItemName, out DataModle))
            {
               
                DataModle.Bid = e.UpdateData.Bid;
                DataModle.Offer = e.UpdateData.Offer;
                DataModle.High = e.UpdateData.High;
                DataModle.Low = e.UpdateData.Low;

                igServer.SendToClient(DataModelDict, MarketDataEpicsDict, MarketDataInstrumentNameDict);
                Updated?.Invoke(DataModelDict);

               
            }
            else
            {              
                DataModelDict.Add(e.ItemName, e.UpdateData);               
            }

            
        }


        /// <summary>
        /// MarketDataEpicsDict<key,value>
        /// key:market.epic
        /// value:node.name
        /// </summary>
        /// <param name="connectionStatus"></param>
        /// <returns>MarketDataEpicsDict</returns>
        private async Task<Dictionary<string, string>> GetMarketDataEpics(bool connectionStatus)
        {         
            if (connectionStatus)
            {
                hierarchyNodeList = await GetNodeData(connectionStatus);

                foreach (var node in hierarchyNodeList) 
                {
                    var response1 = await restClient.browse(node.id);

                    //for (int i=0; i< response1.Response.markets.Count; i++ )
                    //{
                    //    if (i==2)
                    //    {
                    //        break;
                    //    }
                    //    var market = response1.Response.markets[i];
                    //    MarketDataEpicsDict.Add(market.epic, node.name);
                    //    MarketDataInstrumentNameDict.Add(market.epic, market.instrumentName);
                    //}

                    if (response1.Response.markets.Count >= 1)
                    {
                        var market = response1.Response.markets[0];
                        MarketDataEpicsDict.Add(market.epic, node.name);
                        MarketDataInstrumentNameDict.Add(market.epic, market.instrumentName);
                    }

                    //foreach (var market in response1.Response.markets)
                    //{
                    //    MarketDataEpicsDict.Add(market.epic, node.name);
                    //    MarketDataInstrumentNameDict.Add(market.epic,market.instrumentName);
                    //}
                }
            }
            
            fp.createFile(MarketDataEpicsDict);
            fp.createFile2(MarketDataInstrumentNameDict);
            return MarketDataEpicsDict;
        }

        private async Task<List<HierarchyNode>> GetNodeData(bool connectionStatus)
        {
            HierarchyNode hn = new HierarchyNode();
            hn.id = "93268";
            hn.name = "Stock Indices";
            if (connectionStatus)
            {
                var response = await restClient.browse(hn.id);              
                foreach (var node in response.Response.nodes)
                {
                    hierarchyNodeList.Add(node);
                }              
            }         
            return hierarchyNodeList;
        }

    }
}
