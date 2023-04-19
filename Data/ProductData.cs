
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT p.ProductID, p.Name, p.Description, p.UnitPrice, p.ProductImage, 
                               COALESCE(AVG(oi.Rating), 0) AS AverageRating, 
                               COALESCE(COUNT(oi.Rating), 0) AS ReviewCount
                               FROM Products p
                               LEFT JOIN OrderItems oi ON p.ProductID = oi.ProductID
                               GROUP BY p.ProductID, p.Name, p.Description, p.UnitPrice, p.ProductImage;";

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
                        ProductImage = (string)reader["ProductImage"],
                        AverageRating = (int)reader["AverageRating"],
                        ReviewCount = (int)reader["ReviewCount"],

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
                        ProductId=(int)reader["ProductID"],
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        UnitPrice = (float)(double)reader["UnitPrice"],
                        
            
                    };
                    return product;
                }
            }

            return null;
        }

    
    
    
        //calculates the average rating of the product
        public static int AverageRating(int ProductID)
        {
            int productAverageRatings = 0;

            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                string sql = @"SELECT AVG(Rating) AS 'Average'
                             FROM OrderItems
                             WHERE ProductID = " + ProductID;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productAverageRatings = (int)reader["Average"];
                }
            }
            return productAverageRatings;
        }
        //counts the number of ratings of the product
        public static int CountRating(int ProductID)
        {
            int ratingCounts = 0;

            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(Rating) AS 'RatingsCounted'
                             FROM OrderItems
                             WHERE ProductID = " + ProductID;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ratingCounts = (int)reader["RatingsCounted"];
                }
            }
            return ratingCounts;
        }
        //details of the product review (customer name, rating, description, date of review)
        public static List<ProductReview> ReviewDetails(int ProductID)
        {
            List<ProductReview> reviewDetails = new List<ProductReview>();
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                string sql = @"SELECT C.FirstName, C.LastName, O.OrderDate, OI.Rating, OI.Review
                               FROM Customers C, Orders O, OrderItems OI
                               WHERE C.CustomerID = O.CustomerID AND O.OrderID = OI.OrderID AND OI.ProductID = " + ProductID;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductReview reviewDetail = new ProductReview()
                    {
                        CustomerName = (string)reader["FirstName"] + " " + (string)reader["LastName"],
                        // add in date later (optional)
                        Rating = (int)reader["Rating"],
                        ReviewText = (string)reader["Review"]
                    };
                    reviewDetails.Add(reviewDetail);
                }
            }
            return reviewDetails;
        }
        //update product review into database
        public static void submitReview(int ratingStars, string reviewDescription, int OrderItemId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString.connectionString))
            {
                conn.Open();
                string sql = @"UPDATE OrderItems SET Rating = @ratingStars, Review = @reviewdescription WHERE OrderItemId = @OrderItemId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ratingStars", ratingStars);
                cmd.Parameters.AddWithValue("@reviewdescription", reviewDescription);
                cmd.Parameters.AddWithValue("@OrderItemId", OrderItemId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}


