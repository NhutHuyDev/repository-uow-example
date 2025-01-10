namespace repository_uow_example.Entities
{
    public class Product
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required double Price { get; set; }
        public required int Quantity { get; set; }
    }
}
