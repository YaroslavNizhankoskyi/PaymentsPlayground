namespace PaymentsPlayground.Models
{
    public class UserPayment : Entity
    {
        public decimal Amount { get; set; }

        public string SenderId { get; set; }

        public string RecieverId { get; set; }

        public User Sender { get; set; }

        public User Reciever { get; set; }

        public Transaction Transaction { get; set; }
    }
}
