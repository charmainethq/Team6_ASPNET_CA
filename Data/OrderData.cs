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
				string sql = @"SELECT OI.OrderItemId, OI.ProductID AS ProductID, 
								O.OrderDate AS OrderDate,oi.Quantity AS OrderQty,
								A.ActivationCode As Code,
								P.Name as ProductName,P.Description as ProductDescription,
								P.ProductImage as ProductImage
								FROM Orders O, OrderItems OI, ActivationCodes A,Products P
								where O.OrderID=OI.OrderID 
								AND OI.OrderItemId = A.OrderItemID
								And p.ProductID=oi.ProductID
								ANd O.CustomerID =" + customerID+"Order by O.OrderDate";
				
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
								Qty = (int)reader["OrderQty"],
								Activation_Code = (string)reader["Code"],
								Name= (string)reader["ProductName"],
								Description= (string)reader["ProductDescription"],
								ProductImage = (string)reader["ProductImage"]

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
	

