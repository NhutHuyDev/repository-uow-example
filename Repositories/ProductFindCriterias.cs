namespace repository_uow_example.Repositories
{
    public class ProductFindCriterias : PagingCriterias
    {
        public double MinPrice { get; set; } = double.MinValue;
        public double MaxPrice { get; set; } = double.MaxValue;
        public IEnumerable<Guid> Ids { get; set; } = [];
        public string Name { get; set; } = string.Empty;
        public static ProductFindCriterias Empty => new() { };
    }
}
