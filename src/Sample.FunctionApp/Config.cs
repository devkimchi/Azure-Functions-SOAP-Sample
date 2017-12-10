using System;

namespace Sample.FunctionApp
{
    /// <summary>
    /// This represents the app settings configuration entity.
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Gets the WCF service endpoint URL.
        /// </summary>
        public static string WcfServiceEndpoint =>
            Environment.GetEnvironmentVariable("WcfServiceEndpoint");
    }
}
