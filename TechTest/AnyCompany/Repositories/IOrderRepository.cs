using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetCustomerOrders(int customerId);

        Order GetById(int orderId);

        bool Exists(int orderId);

        void Save(Order order);
    }
}
