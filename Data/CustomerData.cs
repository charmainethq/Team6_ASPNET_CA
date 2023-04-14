using Team6.Models;
using Microsoft.Data.SqlClient;

namespace Team6.Data
{
    public class CustomerData
    {
        public static Customer GetCustomerById(string custId)
        {

            string connectionString = ConnectString.connectionString;
;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT [CustomerID], [Username] ,[Password],[FirstName], [LastName]
                               FROM [ShoppingDB].[dbo].[Customers]
                               WHERE CustomerID='" + custId + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer()
                    {
                        CustomerID = (string)reader["CustomerID"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"]
                    };
                    return customer;
                }
            }

            return null;
        }

    }
}
