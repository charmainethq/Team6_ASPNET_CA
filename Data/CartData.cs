using Azure.Core;
using Azure;
using Microsoft.Data.SqlClient;
using Team6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Storage;

namespace Team6.Data
{
    public class CartData
    {
        public static int CreateOrder(int customerId) //Inserts a new order into the Orders table with the given customerId and returns the ID of the new order.
        {
            int orderId = 0;

            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                string sql = "INSERT INTO Orders (CustomerId) VALUES (@customerId);" +
                               "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    conn.Open();
                    orderId = (int)cmd.ExecuteScalar();
                }
            }

            return orderId;
        }

        
        public static List<OrderItem> GetOrderItemsByOrder(int orderId) //Retrieves all order items for the given orderId from the OrderItems table.
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                string sql = "SELECT * FROM OrderItems WHERE OrderID = @orderId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        OrderItem orderItem = new OrderItem
                        {
                            OrderItemId = Convert.ToInt32(reader["OrderItemId"]),
                            OrderID = Convert.ToInt32(reader["OrderId"]),
                            ProductID = Convert.ToInt32(reader["ProductId"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            UnitPrice = (float)(double)(reader["UnitPrice"]),
                            ActivationCodes = (List<ActivationCode>)(reader["ActivationCodes"]),
                        };

                        orderItems.Add(orderItem);
                    }
                }
            }

            return orderItems;
        }

        public void CreateOrderItem(OrderItem orderItem) //Inserts a new order item into the OrderItems table based on the provided OrderItem object.
        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                string sql = "INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price, ActivationCodes) " +
                               "VALUES (@orderId, @productId, @quantity, @price, @activationCodes)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderItem.OrderID);
                    cmd.Parameters.AddWithValue("@productId", orderItem.ProductID);
                    cmd.Parameters.AddWithValue("@quantity", orderItem.Quantity);
                    cmd.Parameters.AddWithValue("@price", orderItem.UnitPrice);
                    cmd.Parameters.AddWithValue("@activationCodes", orderItem.ActivationCodes);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static Product GetProductById(int productId) //Retrieves a product from the Products table based on the provided productId.
        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))

            {
                conn.Open();

                string sql = "SELECT * FROM Products WHERE ProductId = @productId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", productId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = (int)reader["ProductId"],
                            Name = (string)reader["Name"],
                            ProductImage = (string)reader["ProductImage"],
                            Description = (string)reader["Description"],
                            UnitPrice = (float)(double)reader["UnitPrice"]
                        };
                        return product;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public static void ClearCart(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                string sql = "DELETE FROM CartItems WHERE CustomerID = @customerId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}




