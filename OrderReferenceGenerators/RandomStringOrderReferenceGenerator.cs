using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using repository_uow_example;

namespace RepositorySample.OrderReferenceGenerators
{
    internal class RandomStringOrderReferenceGenerator : IOrderReferenceGenerator
    {
        private static readonly Random random = new();
        public string Next(int length)
        {
            if (length <= 0 || length > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
