using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace localb.Entities
{
    public interface ILocalBSettings { }
    public class LocalBSettings
    {
        public string ApiKey { get; set; }
        public string Url { get; set; }
        public string BuyOnlineEndpoint { get; set; }
        public string Countries { get; set; }
        public string PaymentMethods { get; set; }
    }
}
