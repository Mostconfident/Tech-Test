using System;
using AnyCompany.Repositories;
using AnyCompany.Services;

namespace AnyCompany
{
    public class OrderService
    {
        //private readonly OrderRepository orderRepository = new OrderRepository();

        private readonly ICustomerService customerService;
        private readonly ICalculationService vatCalculationService;
        private readonly IOrderRepository orderRepository;

        public OrderService(ICustomerService customerService, ICalculationService vatCalculationService, IOrderRepository orderRepository)
        {
            this.customerService = customerService;
            this.vatCalculationService = vatCalculationService;
            this.orderRepository = orderRepository;
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            var customer = customerService.GetById(customerId);

            if (customer == null)
                throw new ArgumentException($"Customer with Id: {customerId} not found");

            var orderExists = orderRepository.Exists(order.OrderId);

            if (orderExists)
                throw new ArgumentException($"Order with Id: {order.OrderId} already exists");

            if (order.Amount <= 0)
                return false;

            order.VAT = vatCalculationService.GetVatByCountryCode(customer.Country);

            order.CustomerId = customer.CustomerId;

            orderRepository.Save(order);

            return true;
        }
    }
}
