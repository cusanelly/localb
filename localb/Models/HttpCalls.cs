using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using localb.Entities;
using Microsoft.Extensions.Options;


namespace localb.Models
{    
    public interface IHttpCalls
    {
        Task<IEnumerable<Ad_List>> BuyOnline(int max = 10);
        
    }
    public class HttpCalls : IHttpCalls
    {        
        private readonly IOptions<LocalBSettings> _localBSettings;
        public string Url { get; set; }
        public List<Ad_List> Prospects { get; set; }
        public string Next { get; set; }
        
        public HttpCalls(IOptions<LocalBSettings> localBSeetings)
        {
            _localBSettings = localBSeetings;
            Url = $"{_localBSettings.Value.Url}/{_localBSettings.Value.BuyOnlineEndpoint}";
            Prospects = new List<Ad_List>();
        }       
        public async Task<IEnumerable<Ad_List>> BuyOnline(int max = 10)
        {                 
            var client = new HttpClient();

            var response = (String.IsNullOrEmpty(Next)) ?
                await client.GetStringAsync(Url + "/usd/.json?fields=profile,location_string,temp_price,bank_name,msg") :                
                await client.GetStringAsync(Next);
            var obj = JsonConvert.DeserializeObject<Rootobject>(response);
            
            #region FILTER KEYWORD
            // Filtramos los resultados
            IEnumerable<Ad_List> result = obj.data.ad_list.Where(t =>
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


            Next = obj.pagination.next;
            if (result.Count() > 0)                
                Prospects.AddRange(result);
            if (Prospects.Count < max)
                await BuyOnline(max: max);
            
            return Prospects;            
        }        
    }
}
