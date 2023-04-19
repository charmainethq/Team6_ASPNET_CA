using Microsoft.Data.SqlClient;
using Team6.Models;

namespace Team6.Data
{
    public class SessionData
    {
        public static Session GetSessionByCustomerId(int custId)
        {

            string connectionString = ConnectString.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT [CustomerID], [SessionID], [Timestamp]
                               FROM [ShoppingDB].[dbo].[Sessions]
                               WHERE CustomerID =" + custId;

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Session session = new Session()
                    {
                        SessionID = (string)reader["SessionID"],
                        CustomerID = (int)reader["CustomerID"],
                        Timestamp = (DateTime)reader["Timestamp"]
                    };
                    return session;
                }
            }

            return null;
        }


        public static void AddSession(Session session)
        {

            string connectionString = ConnectString.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Sessions (SessionID, CustomerID, Timestamp)
                               VALUES (@SessionID, @CustomerID, @Timestamp)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@SessionID", session.SessionID);
                cmd.Parameters.AddWithValue("@CustomerID", session.CustomerID);
                cmd.Parameters.AddWithValue("@Timestamp", session.Timestamp);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        public static void DeleteSession(int? custId)
        {
            if (custId != null)
            {
                string connectionString = ConnectString.connectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"DELETE FROM Sessions 
                              WHERE CustomerID=" + custId;

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }
    }

}
