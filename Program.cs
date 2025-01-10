using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using repository_uow_example.Entities;
using repository_uow_example.Repositories;
using repository_uow_example.Repositories.InMemory;
using repository_uow_example.Repositories.SqlServer;

namespace repository_uow_example
{
    internal class Program
    {
        static void Main()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: true).Build();

            string connectionString = config.GetConnectionString("ShopDb") ?? string.Empty;

            if (connectionString.Length > 0)
            {
                Console.WriteLine("Using SQL Server Repositories");

                var sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                var trans = sqlConnection.BeginTransaction();

                var productRepository = new Repositories.SqlServer.ProductRepository(sqlConnection, trans);

                productRepository.DeleteAll();

                InsertSampleProducts(productRepository);
                QueryProducts(productRepository);

                trans.Commit();
            }
            else
            {
                Console.WriteLine("Using In Memory Repositories");

                var productRepository = new Repositories.InMemory.ProductRepository();
                
                productRepository.DeleteAll();

                InsertSampleProducts(productRepository);
                QueryProducts(productRepository);
            }
        }

        private static void QueryProducts(IProductRepository productRepository)
        {
            Console.WriteLine("Product.Price >= 1000");
            var products = productRepository.Find(new ProductFindCriterias()
            {
                MinPrice = 1000,
            });
            PrintProducts(products);

            Console.WriteLine("Product.Price <= 1000");
            products = productRepository.Find(new ProductFindCriterias()
            {
                MaxPrice = 1000,
            });
            PrintProducts(products);

            Console.WriteLine("Product.Name contains iPad");
            products = productRepository.Find(new ProductFindCriterias()
            {
                Name = "iPad",
            });
            PrintProducts(products);
        }

        private static void InsertSampleProducts(IProductRepository productRepository)
        {
            productRepository.Add(new Product()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "Apple iPhone",
                Price = 999,
                Quantity = 70,
            });

            productRepository.Add(new Product()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "Apple iPad",
                Price = 799,
                Quantity = 10,
            });

            productRepository.Add(new Product()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000003"),
                Name = "Apple Macbook",
                Price = 1399,
                Quantity = 20,
            });
        }

        private static void PrintProducts(IEnumerable<Product> products)
        {
            Console.WriteLine(new string('-', 95));
            Console.WriteLine($"| {"ID",-36} | {"Name",-40} | {"Price",-10} |");
            Console.WriteLine(new string('-', 95));

            foreach (var product in products)
            {
                Console.WriteLine($"| {product.Id,-36} | {product.Name,-40} | {product.Price,-10:C} |");
            }
            Console.WriteLine(new string('-', 95) + "\n");
        }
    }
}
