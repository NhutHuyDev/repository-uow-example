namespace repository_uow_example.Repositories
{
    public class PagingCriterias
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = int.MaxValue;
    }
}
