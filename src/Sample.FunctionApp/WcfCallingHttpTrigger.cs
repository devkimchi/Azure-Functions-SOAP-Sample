using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

using Sample.WcfProxy.ServiceReference1;

namespace Sample.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity for WCF call.
    /// </summary>
    public static class WcfCallingHttpTrigger
    {
        /// <summary>
        /// Invokes the HTTP trigger function.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="value">Value to pass.</param>
        /// <param name="log"><see cref="TraceWriter"/> instance.</param>
        /// <returns>Returns <see cref="HttpResponseMessage"/> instance.</returns>
        [FunctionName("WcfCallingHttpTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "data/{value}")] HttpRequestMessage req,
            int value,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // Instantiates the binding object.
            // Depending on the security definition, the security mode should be adjusted.
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);

            // If necessary, appropriate client credential type should be set.
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            // Instantiates the endpoint address.
            var endpoint = new EndpointAddress(Config.WcfServiceEndpoint);

            using (var client = new Service1Client(binding, endpoint))
            {
                // If necessary, username and password should be provided.
                //client.ClientCredentials.UserName.UserName = "username";
                //client.ClientCredentials.UserName.Password = "password";

                var result = await client.GetDataAsync(value).ConfigureAwait(false);

                log.Info($"{result} received.");

                return req.CreateResponse(HttpStatusCode.OK, result);
            }
        }
    }
}
