using Microsoft.Data.SqlClient;
using Team6.Models;

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



    }
}
