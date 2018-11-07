using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace localb.Models
{



    public class Rootobject
    {
        public Pagination pagination { get; set; }
        public Data data { get; set; }
    }

    public class Pagination
    {
        public string next { get; set; }
        public string prev { get; set; }
    }

    public class Data
    {
        public Ad_List[] ad_list { get; set; }
        public string ad_count { get; set; }
    }

    public class Ad_List
    {
        public data data { get; set; }
        public Actions actions { get; set; }
    }

    public class data
    {
        public string visible { get; set; }
        public string hidden_by_opening_hours { get; set; }
        public string location_string { get; set; }
        public string countrycode { get; set; }
        public string city { get; set; }
        public string trade_type { get; set; }
        public string online_provider { get; set; }
        public string first_time_limit_btc { get; set; }
        public string volume_coefficient_btc { get; set; }
        public string sms_verification_required { get; set; }
        public string reference_type { get; set; }
        public string display_reference { get; set; }
        public string currency { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string min_amount { get; set; }
        public string max_amount { get; set; }
        public string max_amount_available { get; set; }
        public string limit_to_fiat_amounts { get; set; }
        public string ad_id { get; set; }
        public string temp_price { get; set; }
        public string temp_price_usd { get; set; }
        public string ing { get; set; }
        public Profile profile { get; set; }
        public string require_feedback_score { get; set; }
        public string require_trade_volume { get; set; }
        public string require_trusted_by_advertiser { get; set; }
        public string payment_window_minutes { get; set; }
        public string bank_name { get; set; }
        public string track_max_amount { get; set; }
        public string atm_model { get; set; }
        public string msg { get; set; }
    }

    public class Profile
    {
        public string username { get; set; }
        public string name { get; set; }
        public string last_online { get; set; }
        public string trade_count { get; set; }
        public string feedback_score { get; set; }
    }

    public class Actions
    {
        public string public_view { get; set; }
    }

}

