using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Http;
using Newtonsoft.Json;
using localb.Entities;
using Microsoft.Extensions.Options;
using System.Net;

namespace localb.Models
{
    public interface IHttpCalls
    {
        Task<Rootobject> BuyOnline();
        //Task<string> BuyOnline();
    }
    public class HttpCalls : IHttpCalls
    {
        private readonly IOptions<LocalBSettings> _localBSettings;
        public string Url { get; set; }
        public HttpCalls(IOptions<LocalBSettings> localBSeetings)
        {
            _localBSettings = localBSeetings;
            Url = $"{_localBSettings.Value.Url}/{_localBSettings.Value.BuyOnlineEndpoint}";
        }

        public async Task<Rootobject> BuyOnline()
        {
            //API Key
            HMACSHA256 hash = new HMACSHA256();
            // API Nonce
            int timestamp = DateTime.Now.Millisecond;
            //API Signature
            var client = new HttpClient();
            WebHeaderCollection headers = new WebHeaderCollection
            {
                
                { "",""}
            };
            var response = await client.GetStringAsync(Url + "/ve/Venezuela/.json?fields=location_string,temp_price,currency,min_amount,max_amount,temp_price_usd,bank_name,msg");
            return JsonConvert.DeserializeObject<Rootobject>(response);
            //return response;
        }
    }
}
