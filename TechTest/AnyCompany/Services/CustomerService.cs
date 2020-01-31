using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Repositories;

namespace AnyCompany.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;
        public CustomerService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
        }
        public Customer GetById(int id)
        {
            var customer = customerRepository.Load(id);

            if (customer == null) return null;

            customer.Orders = orderRepository.GetCustomerOrders(customer.CustomerId);

            return customer;
        }

        public List<Customer> GetAll()
        {
            var customers = customerRepository.LoadAll();
            
            customers.ForEach(x => { x.Orders = orderRepository.GetCustomerOrders(x.CustomerId); });

            return customers;
        }
    }
}
