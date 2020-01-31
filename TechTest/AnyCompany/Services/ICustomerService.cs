using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Services
{
    public interface ICustomerService
    {
        Customer GetById(int id);

        List<Customer> GetAll();
    }
}
