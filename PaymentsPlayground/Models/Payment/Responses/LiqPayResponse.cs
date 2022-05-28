using Newtonsoft.Json;

namespace PaymentsPlayground.Models.Payment.Responses
{
    public class LiqPayResponse
    {
        public int payment_id { get; set; }
        public string action { get; set; }
        public string status { get; set; }
        public int version { get; set; }
        public string type { get; set; }
        public string paytype { get; set; }
        public string public_key { get; set; }
        public int acq_id { get; set; }
        public string order_id { get; set; }
        public string liqpay_order_id { get; set; }
        public string description { get; set; }
        public string sender_phone { get; set; }
        public string sender_card_mask2 { get; set; }
        public string sender_card_bank { get; set; }
        public string sender_card_type { get; set; }
        public int sender_card_country { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public double sender_commission { get; set; }
        public double receiver_commission { get; set; }
        public double agent_commission { get; set; }
        public double amount_debit { get; set; }
        public double amount_credit { get; set; }
        public double commission_debit { get; set; }
        public double commission_credit { get; set; }
        public string currency_debit { get; set; }
        public string currency_credit { get; set; }
        public double sender_bonus { get; set; }
        public double amount_bonus { get; set; }
        public string mpi_eci { get; set; }
        public bool is_3ds { get; set; }
        public string language { get; set; }
        public string product_category { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public long create_date { get; set; }
        public long end_date { get; set; }
        public int transaction_id { get; set; }
        public string err_description { get; set; }
    }
}
