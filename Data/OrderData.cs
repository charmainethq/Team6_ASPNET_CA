using Azure.Core;
using Azure;
using Microsoft.Data.SqlClient;
using Team6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Storage;
using Castle.Components.DictionaryAdapter.Xml;


namespace Team6.Data
{

    public class OrderData
    {
        public static Order GetOrderById(int orderId) //Retrieves an order by its ID from the Orders table.
        {
            Order order = null;

            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                string sql = "SELECT * FROM Orders WHERE OrderId = @orderId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        order = new Order
                        {
                            OrderID = Convert.ToInt32(reader["OrderId"]),
                            CustomerID = Convert.ToInt32(reader["CustomerId"]),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"])
                        };
                    }
                }
            }

            return order;
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

        public static List<OrderHistory> OrderList(int? customerID)
        {

            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                string sql = @"SELECT  P.Name AS Name,
								P.ProductImage AS Image,
								P.Description AS Description,
								OI.ProductID AS ProductID,
								OI.Quantity AS Quantity,
								OI.OrderItemId AS OrderItemID,
								O.OrderId AS OrderID,
								O.OrderDate AS OrderDate
							FROM 
								Orders O, OrderItems OI, Products P
							WHERE 
								O.OrderID=OI.OrderID 
								AND OI.ProductID = P.ProductId
								ANd O.CustomerID ='" + customerID+"'";

                List<OrderHistory> allOrdersByCustomer = new List<OrderHistory>();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    //SqlDataReader reader = cmd.ExecuteReader();
                     using (var reader = cmd.ExecuteReader())
                     {
                        while (reader.Read())
                        {
                            OrderHistory order = new OrderHistory()
                            {
                                OrderItemId = (int)reader["OrderItemId"],
                                ProductId = (int)reader["ProductID"],
                                ProductName = (string)reader["Name"],
                                ProductDescription = (string)reader["Description"],
                                ProductImage = (string)reader["Image"],
                                PurchaseOn = (DateTime)reader["OrderDate"],
                                Qty = (int)reader["Quantity"],                                

                            };
                            allOrdersByCustomer.Add(order);
                        }
                     }
                    return allOrdersByCustomer;
                }

            }
        }


        public static List<string> GetListOfCodes(int orderItemId)
        {

            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                string sql = @"select ActivationCode
                                from ActivationCodes
                                where OrderItemID =" + orderItemId;

                List<string> codesByOrderItemId = new List<string>();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string actCode = (string)reader["ActivationCode"];

                            codesByOrderItemId.Add(actCode);

                        }
                    }
                    return codesByOrderItemId;
                }

            }

        }



    }

}
	

