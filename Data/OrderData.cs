using Azure.Core;
using Azure;
using Microsoft.Data.SqlClient;
using Team6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Storage;
namespace Team6.Data
{
	public class OrderData
	{
		public static List<OrderHistory> PurchaseHistory(int customerID)
		{

			using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
			{
				conn.Open();
				string sql = @"SELECT OI.ProductID AS ProductID, O.OrderDate AS OrderDate,OI.Quantity AS Quantity, A.ActivationCode AS Code
								FROM Orders O, OrderItems OI, ActivationCodes A
								WHERE O.OrderID=OI.OrderID 
								AND OI.OrderItemId = A.OrderItemID
								ANd O.CustomerID ="+customerID;
				
				List<OrderHistory> allOrdersByCustomer = new List<OrderHistory>();
				using (var cmd = new SqlCommand(sql, conn))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{

							OrderHistory order = new OrderHistory()
							{
								ProductId = (int)reader["ProductID"],
								PurchaseOn = (DateTime)reader["OrderDate"],
								Qty = (int)reader["Quantity"],
								Activation_Code = (string)reader["Code"]
							};

							allOrdersByCustomer.Add(order);

						}
					}
					return allOrdersByCustomer;
				}
				
			}
		}
	}
}
	

