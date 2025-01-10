using repository_uow_example.Entities;

namespace repository_uow_example.Repositories
{
    public interface IProductRepository
    {
        Product? FindById(Guid id);
        IEnumerable<Product> Find(ProductFindCriterias criterias, ProductSortBy sortBy = ProductSortBy.NameAscending);
        Product? Add(Product product);
        int DeleteAll();
        int Update(Product product);
    }
}
