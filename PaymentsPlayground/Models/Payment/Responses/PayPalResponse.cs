using Newtonsoft.Json;

namespace PaymentsPlayground.Models.Payment.Responses
{
    public class PayPalResponse
    {
        public string payer_email { get; set; }
        public string payer_id { get; set; }
        public string payer_status { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string txn_id { get; set; }
        public string mc_currency { get; set; }
        public string mc_fee { get; set; }
        public string mc_gross { get; set; }
        public string protection_eligibility { get; set; }
        public string payment_fee { get; set; }
        public string payment_gross { get; set; }
        public string payment_status { get; set; }
        public string payment_type { get; set; }
        public string handling_amount { get; set; }
        public string shipping { get; set; }
        public string item_name { get; set; }
        public string quantity { get; set; }
        public string txn_type { get; set; }
        public DateTime payment_date { get; set; }
        public string receiver_id { get; set; }
        public string notify_version { get; set; }
        public string custom { get; set; }
        public string verify_sign { get; set; }
    }

}
