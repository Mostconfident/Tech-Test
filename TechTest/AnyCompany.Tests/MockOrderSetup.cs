using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Repositories;
using AnyCompany.Services;
using Moq;

namespace AnyCompany.Tests
{
    public static class MockCustomerOrderData
    {
        public static ICustomerService GetCustomerService_GetById_Return_Customer(int customerId)
        {
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Customer
                {
                    CustomerId = customerId,
                    Country = "UK",
                    DateOfBirth = DateTime.Now,
                    Name = "Foo"
                });

            return mockCustomerService.Object;
        }
        public static ICustomerService GetCustomerService_GetById_Return_Null(int customerId)
        {
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(x => x.GetById(customerId))
                .Returns((Customer)null);

            return mockCustomerService.Object;
        }

        public static IOrderRepository GetOrderRepository_OrderExists(bool exists)
        {
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(exists);

            return mockOrderRepository.Object;
        }

        public static IOrderRepository GetOrderRepository_Orders(int customerId)
        {
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(x => x.GetCustomerOrders(customerId))
                .Returns<List<Order>>(c =>
                {
                    var orders = new List<Order>
                    {
                        new Order
                            {
                                CustomerId = customerId,
                                OrderId = 1,
                                VAT = 0d,
                                Amount = 100

                            }
                    };

                    return orders;
                });

            return mockOrderRepository.Object;
        }
    }
}
