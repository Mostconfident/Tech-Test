using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnyCompany.Repositories;

namespace AnyCompany
{
    public static class CustomerRepository  
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.CustomerId = int.Parse(reader["CustomerId"].ToString());
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }

            connection.Close();

            return customer.CustomerId == 0 ? null : customer;
        }

        public static List<Customer> LoadAll()
        {
            var customers = new List<Customer>();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer",
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    CustomerId = int.Parse(reader["CustomerId"].ToString()),
                    Name = reader["Name"].ToString(),
                    DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                    Country = reader["Country"].ToString()

                });
            }

            connection.Close();

            return customers;
        }
    }
}
