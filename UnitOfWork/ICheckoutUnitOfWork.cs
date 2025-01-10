using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using repository_uow_example.Entities;

namespace repository_uow_example.UnitOfWork
{
    public interface ICheckoutUnitOfWork
    {
        void CreateOrder(Order order);
        void SaveChanges();
    }
}
