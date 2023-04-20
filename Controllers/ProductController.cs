using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers
{
    public class ProductController : Controller
    {
        private readonly List<Product> _products = new List<Product>
        {  
            //new Product { Id ="P1000" , Name= "Product 1", Description = "Description 1", Price = 10.99m, ImageUrl = "/images/product1.jpg" },
            //new Product { Id ="P1002", Name = "Product 2", Description = "Description 2", Price = 19.99m, ImageUrl = "/images/product2.jpg" },
            //new Product { Id= "P1003", Name = "Product 3", Description = "Description 3", Price = 25.99m, ImageUrl = "/images/product3.jpg" },
};


        public IActionResult Product(int Id)

        {
            Product product = ProductData.GetProductById(Id);
            int averageRating = ProductData.AverageRating(Id);
            int ratingCounts = ProductData.CountRating(Id);
            List<ProductReview> reviewDetails = ProductData.ReviewDetails(Id);
            ViewBag.product = product;
            ViewData["avgRtg"] = averageRating;
            ViewData["rtgCnts"] = ratingCounts;
            ViewData["rvDts"] = reviewDetails;
            return View();
        }
        
        
        //return view of the individual product based on product Id or name
            
        
        //checks if customer has review before and redirect accordingly
        public IActionResult CheckCustomerReview(int OrderItemId)
        {
            List<ProductReview> checkCustomerReview = ProductData.RetrieveCustomerReview(OrderItemId);
            foreach (ProductReview reviewDetails in checkCustomerReview)
            {
                if (reviewDetails.Rating == null && reviewDetails.ReviewText == null)
                {
                    return RedirectToAction("CreateReview");
                }
                else
                {
                    return RedirectToAction("EditReview");
                }
            }
            return View();
        }

        //create review (for first time reviews)
        public IActionResult CreateReview(string? submitReviewButton, int ratingStars, string? reviewDescription, int OrderItemId, string customerName) 
        {
            //when "submit" button page is clicked on the create review page
            if (submitReviewButton == null)
            {
                return View();
            }
            else
            {
                ProductData.submitReview(ratingStars, reviewDescription, OrderItemId);

                // Redirect back to Order History after review has been submitted
                return RedirectToAction("Index", "OrderHistory");
            }
        }

        //edit review (for subsequent reviews)
        public IActionResult EditReview(string? submitReviewButton, int ratingStars, string? reviewDescription, int OrderItemId, string customerName)
        {
            //when "submit" button page is clicked on the create review page
            if (submitReviewButton == null)
            {
                List<ProductReview> retrieveReviews = ProductData.RetrieveCustomerReview(OrderItemId);
                ViewData["rtvRv"] = retrieveReviews;
                return View();
            }
            else
            {
                ProductData.submitReview(ratingStars, reviewDescription, OrderItemId);

                // Redirect back to Order History after review has been submitted
                return RedirectToAction("Index", "OrderHistory");
            }
        }
    }
}
