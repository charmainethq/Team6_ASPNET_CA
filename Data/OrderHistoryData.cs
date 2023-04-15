using Microsoft.Data.SqlClient;
using Team6.Models;

namespace Team6.Data
{
	public class OrderHistoryData
	{
		public static List<Tuple<Order, OrderItem>> PurchaseHistory()
		{
			List<Tuple<Order, OrderItem>> orders = new List<Tuple<Order, OrderItem>>();
			string con = @"Server=LEWIS-PC;Database=adoExample;Integrated Security=true;encrypt=false";
			using (SqlConnection conn = new SqlConnection(con))
			{
				conn.Open();
				string sql = @"SELECT O.OrderDate,OI.Quantity,OI.ActivationCode
                               FROM Order O, OrderItem OI,
                                WHERE O.OrderID=OI.OrderID";
				SqlCommand cmd = new SqlCommand(sql, conn);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					Order myOrder = new Order()
					{
						OrderDate = (DateTime)reader["Purchase On"],

					};
					OrderItem myOrderItem = new OrderItem()
					{
						Quantity = (int)reader["Qty"],
						ActivationCodes = (List<ActivationCode>)reader["Activation Code"],
					};
					orders.Add(Tuple.Create(myOrder, myOrderItem));
				}
			}
			return orders;
		}
	}
}
