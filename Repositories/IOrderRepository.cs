using repository_uow_example.Entities;

namespace repository_uow_example.Repositories
{
    public interface IOrderRepository
    {
        Order? FindById(Guid id);
        Order? FindByReference(string reference);
        IEnumerable<Order> Find(OrderFindCriterias criterias, OrderSortBy sortBy = OrderSortBy.ReferenceAscending);
        Order? Add(Order order);
        int DeleteAll();
    }
}
