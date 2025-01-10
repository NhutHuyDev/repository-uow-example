namespace repository_uow_example.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public required string OrderReference { get; set; }
        public List<OrderItem> Items { get; set; } = [];
    }
}
