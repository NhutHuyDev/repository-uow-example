namespace repository_uow_example.Repositories
{
    public class OrderFindCriterias : PagingCriterias
    {
        public IEnumerable<Guid> Ids { get; set; } = [];
        public IEnumerable<Guid> CustomerIds { get; set; } = [];
    }
}
