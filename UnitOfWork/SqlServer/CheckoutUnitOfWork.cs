using Microsoft.Data.SqlClient;
using repository_uow_example.Entities;
using repository_uow_example.Repositories.SqlServer;

namespace repository_uow_example.UnitOfWork.SqlServer
{
    public class CheckoutUnitOfWork : ICheckoutUnitOfWork
    {
        private readonly SqlConnection connection;
        private readonly SqlTransaction transaction;
        private OrderRepository? orderRepository;
        private ProductRepository? productRepository;

        public CheckoutUnitOfWork(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.transaction = connection.BeginTransaction();
        }

        public void CreateOrder(Order order)
        {
            this.orderRepository = new(this.connection, this.transaction);
            this.productRepository = new(this.connection, this.transaction);


            foreach (var item in order.Items)
            {
                var product = this.productRepository.FindById(item.ProductId) ?? throw new Exception($"Product not found: {item.ProductId}");

                if (product.Quantity - item.Quantity < 0)
                {
                    throw new Exception("product.Quantity < item.Quantity");
                }
                product.Quantity -= item.Quantity;

                this.productRepository.Update(product);
            }

            this.orderRepository.Add(order);
        }

        public void SaveChanges()
        {
            this.transaction.Commit();
        }
        public void Rollback()
        {
            this.transaction?.Rollback();
        }
        public void Dispose()
        {
            this.transaction?.Dispose();
            this.connection?.Dispose();
        }
    }
}
