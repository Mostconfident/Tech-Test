using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Repositories;
using AnyCompany.Services;
using Moq;
using NUnit.Framework;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class OrderServiceTest
    {

        private ICalculationService calculationService;
        [SetUp]
        public void Setup()
        {
            
            this.calculationService = new CalculationService();
        }

        [TestCase(1, 2,true)]
        [TestCase(2, 16, true)]
        [TestCase(2, 18, true)]
        public void Should_PlaceOrder(int customerId, int orderId, bool expected)
        {
            var order = new Order {OrderId = orderId, Amount = 100d};
            
            var orderService = new OrderService(MockCustomerOrderData.GetCustomerService_GetById_Return_Customer(customerId),
                calculationService, MockCustomerOrderData.GetOrderRepository_OrderExists(false));

            var isOrderPlaced = orderService.PlaceOrder(order, customerId);

            Assert.AreEqual(expected, isOrderPlaced);

        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        public void Should_have_throw_exception_when_order_exist(int customerId, int orderId)
        {
            var order = new Order { OrderId = orderId, Amount = 100d };
           
            var orderService = new OrderService(MockCustomerOrderData.GetCustomerService_GetById_Return_Customer(customerId), 
                calculationService, MockCustomerOrderData.GetOrderRepository_OrderExists(true));


            Assert.Throws<ArgumentException>(
                () => orderService.PlaceOrder(order, customerId));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Should_have_throw_exception_when_customer_does_not_exist(int customerId)
        {
            var order = new Order { OrderId = 1, Amount = 100d };

            var mockOrderRepository = new Mock<IOrderRepository>();

            var orderService = new OrderService(MockCustomerOrderData.GetCustomerService_GetById_Return_Null(customerId), 
                calculationService, mockOrderRepository.Object);


            Assert.Throws<ArgumentException>(
                () => orderService.PlaceOrder(order, customerId));
        }

    }
}
