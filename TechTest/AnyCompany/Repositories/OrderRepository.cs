using System.Collections.Generic;
using System.Data.SqlClient;
using AnyCompany.Repositories;

namespace AnyCompany
{
    public class OrderRepository : IOrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public List<Order> GetCustomerOrders(int customerId)
        {
            var orders = new List<Order>();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand($"SELECT * FROM Orders Where CustomerId = {customerId}",
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(new Order
                {
                    OrderId = int.Parse(reader["OrderId"].ToString()),
                    VAT = double.Parse(reader["VAT"].ToString()),
                    Amount = double.Parse(reader["Amount"].ToString()),
                    CustomerId = int.Parse(reader["CustomerId"].ToString())
                });
            }

            connection.Close();

            return orders;
        }

        public Order GetById(int orderId)
        {
            Order order = new Order();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand($"SELECT * FROM Orders WHERE OrderId = {orderId}" ,
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {               
                order.OrderId = int.Parse(reader["OrderId"].ToString());
                order.VAT = double.Parse(reader["VAT"].ToString());
                order.Amount = double.Parse(reader["Amount"].ToString());
                order.CustomerId = int.Parse(reader["CustomerId"].ToString());
            }

            connection.Close();

            return order.OrderId == 0 ? null : order;
        }

        public bool Exists(int orderId)
        {
            var order = this.GetById(orderId);

            return this.GetById(orderId) != null;
        }

        public void Save(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);
            command.Parameters.AddWithValue("@CustomerId", order.CustomerId);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
