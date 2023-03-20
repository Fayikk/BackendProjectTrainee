using Iyzipay;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System.Diagnostics;

namespace MulakatCalisma.Helper
{
    public class API
    {
        protected Options options;

        public void Initialize()
        {
            options = new Options();
            options.ApiKey = "sandbox-ESlmmML1EgQH28beWiyfXaHqQ0NdqyH2";
            options.SecretKey = "sandbox-5DQ1wwH3v1W3UXPKwGUPE7w2uXBBPdR2";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
        }

        protected void PrintResponse<T>(T resource)
        {
            TraceListener consoleListener = new ConsoleTraceListener();
            Trace.Listeners.Add(consoleListener);
            Trace.WriteLine(JsonConvert.SerializeObject(resource, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }
    }
}
