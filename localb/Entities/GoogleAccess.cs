using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace localb.Entities
{
    public interface IGoogleAccess { }
    public class GoogleAccess
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
