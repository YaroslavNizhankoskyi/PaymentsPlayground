namespace PaymentsPlayground.Models
{
    public class Entity : IEntity, ISoftDeletable
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
