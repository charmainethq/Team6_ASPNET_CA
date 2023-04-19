using Azure.Core;
using Azure;
using Microsoft.Data.SqlClient;
using Team6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Storage;
using Castle.Core.Resource;

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

        public static List<Order> GetOrdersByCustomer(int customerId) //Retrieves all orders for the given customerId from the Orders table.
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
                            Price = (float)(double)(reader["UnitPrice"]),
                            ActivationCodes = (List<ActivationCode>)(reader["ActivationCodes"]), //not sure 
                        };

                        orderItems.Add(orderItem);
                    }
                }
            }

            return orderItems;
        }


        public static void CreateOrderItem(OrderItem orderItem) //Inserts a new order item into the OrderItems table based on the provided OrderItem object.

        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                string sql = @"
                       INSERT INTO OrderItems (OrderItemId, OrderId, ProductId, Quantity)
                       VALUES(@OrderItemId, @OrderId, @ProductId, @Quantity)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderItemId", orderItem.OrderItemId);
                    cmd.Parameters.AddWithValue("@ProductId", orderItem.ProductID);
                    cmd.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
                    cmd.Parameters.AddWithValue("@OrderId", orderItem.OrderID);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void CreateOrder(OrderItem orderItem, int? customerId, DateTime time) //Inserts a new order  into the Orders table based on the provided OrderItem object.

        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                string sql = @"
                       INSERT INTO Orders (OrderId, CustomerId, OrderDate)
                       VALUES(@OrderId, @ProductId, @OrderDate)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderItem.OrderID);
                    cmd.Parameters.AddWithValue("@ProductId", customerId);
                    cmd.Parameters.AddWithValue("@OrderDate", time);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void AddActivationCode(int orderItemId, string activationCode) //Updates the activation codes for a product in the ActivationCode table based on the provided productId and orderId.
        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                string sql = @"
                       INSERT INTO ActivationCodes (OrderItemID, ActivationCode)
                       VALUES(@OrderItemID, @ActivationCode)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderItemID", orderItemId);
                    cmd.Parameters.AddWithValue("@ActivationCode", activationCode);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Order> GetAllOrders() //Retrieves all orders and their associated order items from the Orders and OrderItems tables, and returns a list of Order objects containing their information.
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
                                Price = (float)(double)(reader["UnitPrice"])
                            };

                            order.OrderItems.Add(orderItem);
                        }
                    }
                }

                return orders;
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

    }
}




