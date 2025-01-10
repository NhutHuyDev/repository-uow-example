using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using repository_uow_example.Entities;

namespace repository_uow_example.Repositories.InMemory
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products = [];

        public Product? Add(Product product)
        {
            products.Add(product);

            return product;
        }

        public int DeleteAll()
        {
            int count = products.Count;
            products.Clear();
            return count;
        }

        public IEnumerable<Product> Find(ProductFindCriterias criterias, ProductSortBy sortBy = ProductSortBy.NameAscending)
        {
            var query = from o in products select o;

            if (criterias.Ids.Any())
            {
                query = query.Where(p => criterias.Ids.Contains(p.Id));
            }

            if (criterias.MinPrice != double.MinValue)
            {
                query = query.Where(p => p.Price >= criterias.MinPrice);
            }

            if (criterias.MaxPrice != double.MaxValue)
            {
                query = query.Where(p => p.Price <= criterias.MaxPrice);
            }

            if (!string.IsNullOrEmpty(criterias.Name))
            {
                query = query.Where(p => p.Name.Contains(criterias.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (criterias.Skip > 0)
            {
                query = query.Skip(criterias.Skip);
            }

            if (criterias.Take > 0 && criterias.Take != int.MaxValue)
            {
                query = query.Take(criterias.Take);
            }

            return query;
        }

        public Product? FindById(Guid id)
        {
            return products.Where(p => p.Id == id).FirstOrDefault();
        }

        public int Update(Product product)
        {
            var p = products.Where(p => p.Id == product.Id).FirstOrDefault();
            if (p != null)
            {
                products.Remove(p);
                products.Add(product);

                return 1;
            }

            return 0;
        }
    }
}
