namespace PaymentsPlayground.Models
{
    public class Wallet : Entity
    {
        public string UserId { get; set; }
        public decimal Account { get; set; }

        public User User { get; set; }
    }
}
