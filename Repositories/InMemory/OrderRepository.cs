using repository_uow_example.Entities;

namespace repository_uow_example.Repositories.InMemory
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> orders = [];

        public Order? Add(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

            if (orders.Any(o => o.OrderReference == order.OrderReference))
            {
                throw new ArgumentException("Duplicated order reference");
            }

            orders.Add(order);

            return order;
        }

        public int DeleteAll()
        {
            var c = orders.Count;

            orders.Clear();
            return c;
        }

        public IEnumerable<Order> Find(OrderFindCriterias criterias, OrderSortBy sortBy = OrderSortBy.ReferenceAscending)
        {
            var query = from o in orders select o;

            if (criterias.Ids.Any())
            {
                query = query.Where(o => criterias.Ids.Contains(o.Id));
            }

            if (criterias.CustomerIds.Any())
            {
                query = query.Where(o => criterias.CustomerIds.Contains(o.CustomerId));
            }

            if (criterias.Skip > 0)
            {
                query = query.Skip(criterias.Skip);
            }

            if (criterias.Take > 0 && criterias.Take != int.MaxValue)
            {
                query = query.Take(criterias.Take);
            }

            if (sortBy == OrderSortBy.ReferenceAscending)
            {
                query = query.OrderBy(o => o.OrderReference);
            }
            else
            {
                query = query.OrderByDescending(o => o.OrderReference);
            }

            return query;
        }

        public Order? FindById(Guid id)
        {
            return orders.Where(o => o.Id == id).FirstOrDefault();
        }

        public Order? FindByReference(string reference)
        {
            return orders.Where(o => o.OrderReference == reference).FirstOrDefault();
        }
    }
}
