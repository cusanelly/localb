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
using LocalBitcoins;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace localb.Models
{    
    public interface IHttpCalls
    {
        Task<IEnumerable<Ad_List>> BuyOnline(string next = "", int max = 10);
        
    }
    public class HttpCalls : IHttpCalls
    {        
        private readonly IOptions<LocalBSettings> _localBSettings;
        public string Url { get; set; }
        public List<Ad_List> Prospects { get; }
        public HttpCalls(IOptions<LocalBSettings> localBSeetings)
        {
            _localBSettings = localBSeetings;
            Url = $"{_localBSettings.Value.Url}/{_localBSettings.Value.BuyOnlineEndpoint}";
            Prospects = new List<Ad_List>();
        }       
        public async Task<IEnumerable<Ad_List>> BuyOnline(string next = "", int max = 10)
        {
            //LocalBitcoinsClient Client = new LocalBitcoinsClient(_localBSettings.Value.ApiKey, _localBSettings.Value.ApiSecret);
            //var res = await Client.PublicMarket_BuyBitcoinsOnlineByCurrency("ves");            
            var client = new HttpClient();
            var response = (String.IsNullOrEmpty(next)) ?
                await client.GetStringAsync(Url + "/usd/.json?fields=profile,location_string,temp_price,bank_name,msg") :
                //await client.GetStringAsync(Url + "/usd/.json") :
                await client.GetStringAsync(next);
            var obj = JsonConvert.DeserializeObject<Rootobject>(response);
            //var result = obj.data.ad_list;

            #region FILTER KEYWORD
            // Filtramos los resultados
            var result = obj.data.ad_list.Where(t =>
                t.data.bank_name.ToLower().Contains("banesco") ||
                t.data.msg.ToLower().Contains("banesco") ||
                t.data.bank_name.ToLower().Contains("banesco panama") ||
                t.data.msg.ToLower().Contains("banesco panama") ||
                t.data.bank_name.ToLower().Contains("bofa") ||
                t.data.msg.ToLower().Contains("bofa") ||
                t.data.bank_name.ToLower().Contains("zelle") ||
                t.data.msg.ToLower().Contains("zelle") ||
                t.data.bank_name.ToLower().Contains("bank of america") ||
                t.data.msg.ToLower().Contains("bank of america") ||
                t.data.bank_name.ToLower().Contains("boa") ||
                t.data.msg.ToLower().Contains("boa") 
                ).Select(t => t);
            #endregion

            if (result.Count() > 0)
                Prospects.AddRange(result);
            if (Prospects.Count() < max)            
                await BuyOnline(next:obj.pagination.next,max: max);             
            
            return Prospects;            
        }        
    }
}
