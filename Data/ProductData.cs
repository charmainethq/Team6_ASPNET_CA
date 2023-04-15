using Microsoft.Data.SqlClient;
using Team6.Models;

namespace Team6.Data
{
    public class ProductData
    {

        public static double GetAverageRating(string ProductID)
        {
            ProductID = "'P1000'"; // sample test 'P1000'
            double productAverageRatings = 0;

            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                string sql = @"SELECT AVG(Rating) AS 'Ratings'
                             FROM OrderItems
                             WHERE ProductID = " + ProductID;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productAverageRatings = (double)reader["Ratings"];
                }
            }
            return productAverageRatings;
        }
    }
}
