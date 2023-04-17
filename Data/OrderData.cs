namespace Team6.Data
{
	public class OrderData
	{


		/**public static List<Dictionary<Order, OrderItem>> PurchaseHistory()
		{
			List<Dictionary<Order, OrderItem>> orders = new List<Dictionary<Order, OrderItem>>();

			foreach (var order in orders)
			{
				var orderItems = OrderItems.Where(oi => oi.OrderID == order.OrderID).ToList();

				var myOrder = new Order()
				{
					OrderID = order.OrderID,
					OrderDate = order.OrderDate,
					CustomerID = order.CustomerID
				};

				var myOrderItem = new OrderItem()
				{
					OrderID = orderItems.FirstOrDefault().OrderID,
					ProductID = orderItems.FirstOrDefault().ProductID,
					Quantity = orderItems.FirstOrDefault().Quantity,
					Price = orderItems.FirstOrDefault().Price
				};

				orders.Add(new Dictionary<Order, OrderItem>() { { myOrder, myOrderItem } });
			}

			return orders;
		}**/
	}


	//List<Dictionary<Order, OrderItem>> orders = new List<Dictionary<Order, OrderItem>>();
	//using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
	//{
	//	conn.Open();
	//	string sql = @"SELECT O.OrderDate,OI.Quantity,OI.ActivationCode
	//                            FROM Order O, OrderItem OI,
	//                             WHERE O.OrderID=OI.OrderID";
	//	SqlCommand cmd = new SqlCommand(sql, conn);
	//	SqlDataReader reader = cmd.ExecuteReader();
	//	while (reader.Read())
	//	{
	//		var myOrder = new Order()
	//		{
	//			OrderDate = (DateTime)reader["Purchase On"],

	//		};
	//		var  myOrderItem = new OrderItem()
	//		{
	//			Quantity = (int)reader["Qty"],
	//			ActivationCodes = (List<ActivationCode>)reader["Activation Code"],
	//		};
	//		orders.Add(new Dictionary<Order, OrderItem>() { { myOrder, myOrderItem } } );
	//	}
	//}
	//return orders;
}
