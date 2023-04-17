using Microsoft.Data.SqlClient;
using Team6.Models;

namespace Team6.Data
{
    public class ProductData
    {
        public static List<Product> GetAllProducts()
        {

            List<Product> products = new List<Product>();

            string connectionString = ConnectString.connectionString;
            ;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT * FROM products";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = (int)reader["ProductID"],
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        UnitPrice = (float)(double)reader["UnitPrice"],

                    };
                    products.Add(product);
                    
                }
            }

            return products;
        }
        public static Product GetProductById(string Id)
        {

            string connectionString = ConnectString.connectionString;
            ;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT *
                               FROM [Products]
                               WHERE ProductID='" + Id + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {


                        ProductID=(int)reader["ProductID"],
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        UnitPrice = (double)reader["UnitPrice"],
                        
            
                    };
                    return product;
                }
            }

            return null;
        }


    }
}
