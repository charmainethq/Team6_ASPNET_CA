using Azure.Core;
using Azure;
using Microsoft.Data.SqlClient;
using Team6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Team6.Data
{
    public class CartData
    {
        public static List<OrderItem> GetAllOrders(int customerId)
        {
            List<OrderItem> orders = new List<OrderItem>();
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                string sql = @"SELECT * FROM OrderItem,Order,Customer 
                            where OrderItem.OrderID = Order.OrderID, Order.CustomerID = Customer.CustomerID
                            and Customer.CustomerID = '" + customerId + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderItem item = new OrderItem()
                    {
                        ProductID = (int)reader["ProductID"],
                        ProductName = (string)reader["Name"],
                        ProductDescription = (string)reader["Description"],
                        Price = (float)reader["Price"],
                        Quantity = (int)reader["Quantity"],
                    };
                    orders.Add(item);
                }
            }
            return orders;
        }
        public static Product GetProductById(int productId)
        {
            Product product = null;
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                string sql = @"SELECT * FROM Products WHERE ProdId '= " + productId + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    product = new Product()
                    {
                        ProductId = (int)reader["ProductID"],
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        Price = (float)reader["Price"],
                    };
                }
                return product;
            }
        }
    }  
}




