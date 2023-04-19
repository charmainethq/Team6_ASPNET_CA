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
        public static List<OrderHistory> OrderList(int customerID)
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
								ANd O.CustomerID =" + customerID;

                List<OrderHistory> allOrdersByCustomer = new List<OrderHistory>();
                using (var cmd = new SqlCommand(sql, conn))
                {
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
	

