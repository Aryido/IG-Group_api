using dto.endpoint.auth.session.v2;
using IGWebApiClient;
using IGWebApiClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG_api
{
    class InitialiseClient
    {
        private static class LoginInfo
        {
            public readonly static string env = "live";
            public readonly static string user = "XXXX";
            public readonly static string pwd = "XXXX";
            public readonly static string apikey = "XXXX";

        }

        private static IgRestApiClient restClient = new IgRestApiClient(LoginInfo.env, new Dispatcher());
        private static IGStreamingApiClient streamClient = new IGStreamingApiClient();

        public static IgRestApiClient getIgRestApiClient()
        {
            return restClient;
        }
        public static IGStreamingApiClient getIGStreamingApiClient()
        {
            return streamClient;
        }
        
        private InitialiseClient()
        {

        }

        /// <summary>
        /// establish connection
        /// </summary>
        /// <returns>Boolean</returns>
        public static async Task<Boolean> ConnectionStatus()
        {
            bool connectionEstablished = false;

            var ar = new AuthenticationRequest { identifier = LoginInfo.user, password = LoginInfo.pwd };

            var response = await restClient.SecureAuthenticate(ar, LoginInfo.apikey);

            if (response && (response.Response != null) && (response.Response.accounts.Count > 0))
            {
                ConversationContext context = restClient.GetConversationContext();

                if ((context != null) && (response.Response.lightstreamerEndpoint != null) &&
                    (context.apiKey != null) && (context.xSecurityToken != null) && (context.cst != null))
                {
                    var CurrentAccountId = response.Response.currentAccountId;
                    connectionEstablished = streamClient.Connect(CurrentAccountId, context.cst,
                                                                 context.xSecurityToken, context.apiKey,
                                                                 response.Response.lightstreamerEndpoint);
                }
            }
            return connectionEstablished;
        }

        class Dispatcher : PropertyEventDispatcher
        {
            public void addEventMessage(string message)
            {

            }
            public void BeginInvoke(Action a)
            {

            }
        }

    }
}
