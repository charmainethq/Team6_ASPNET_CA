﻿using Azure.Core;
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
        public static int CreateOrder(int customerId)
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

        public static List<Order> GetOrdersByCustomer(int customerId)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                string sql = "SELECT * FROM Orders WHERE CustomerID = @customerId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            OrderID = Convert.ToInt32(reader["OrderId"]),
                            CustomerID = Convert.ToInt32(reader["CustomerId"]),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"])
                        };

                        orders.Add(order);
                    }
                }
            }
            return orders;
        }
        public static List<OrderItem> GetOrderItemsByOrder(int orderId)
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
                            Price = (float)(reader["Price"]),
                            ActivationCodes = (ActivationCode)(reader["ActivationCodes"]), //not sure 
                        };

                        orderItems.Add(orderItem);
                    }
                }
            }

            return orderItems;
        }

        public void CreateOrderItem(OrderItem orderItem)
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
                    cmd.Parameters.AddWithValue("@price", orderItem.Price);
                    cmd.Parameters.AddWithValue("@activationCodes", orderItem.ActivationCodes);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateProductActivationCodes(int productId, int orderId, string activationCodes)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("UPDATE ActivationCode SET Code = @activationCodes WHERE ProductID = @productId AND OrderID = @orderId", connection))
                {
                    cmd.Parameters.AddWithValue("@activationCodes", activationCodes);
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Order> GetAllOrders()
        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                var sql = "SELECT o.OrderID, o.CustomerID, o.OrderDate, oi.OrderItemId, oi.ProductId, oi.Quantity, oi.Price " +
                            "FROM Orders o " +
                            "INNER JOIN OrderItem oi ON o.OrderID = oi.OrderID " +
                            "ORDER BY o.OrderID DESC";
                var orders = new List<Order>();
                var orderDictionary = new Dictionary<int, Order>();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderId = Convert.ToInt32(reader["OrderID"]);
                            if (!orderDictionary.TryGetValue(orderId, out var order))
                            {
                                order = new Order
                                {
                                    OrderID = orderId,
                                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    OrderItems = new List<OrderItem>()
                                };
                                orderDictionary.Add(orderId, order);
                                orders.Add(order);
                            }

                            var orderItem = new OrderItem
                            {
                                OrderItemId = Convert.ToInt32(reader["OrderItemId"]),
                                ProductID = Convert.ToInt32(reader["ProductId"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = (float) (reader["Price"])
                            };

                            order.OrderItems.Add(orderItem);
                        }
                    }
                }

                return orders;
            }
        }
        public static Product GetProductById(int productId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))

            {
                conn.Open();

                string sql = "SELECT * FROM Products WHERE ProdId = @productId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", productId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = (int)reader["ProdId"],
                            Name = (string)reader["Name"],
                            Description = (string)reader["Description"],
                            Price = (float)reader["Price"]
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

    }
}




