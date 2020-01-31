using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repositories
{
    public class CustomerRepositoryWrapper : ICustomerRepository
    {
        public Customer Load(int customerId)
        {
            return CustomerRepository.Load(customerId);
        }

        public bool Exists(int customerId)
        {
            return CustomerRepository.Load(customerId) != null;
        }

        public List<Customer> LoadAll()
        {
            return CustomerRepository.LoadAll();
        }
    }
}
