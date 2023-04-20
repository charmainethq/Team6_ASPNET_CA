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
            
        


        


        public IActionResult CreateReview(string? submitReviewButton, int ratingStars, string? reviewDescription, int OrderItemId) 
        {
            
            //when "submit" button page is clicked on the create review page
            if (submitReviewButton == null)
            {
                return View();
            }
            else
            {
                //test sample update
                OrderItemId = 20081;
                ProductData.submitReview(ratingStars, reviewDescription, OrderItemId);

                // Redirect back to Order History after review has been submitted
                // return RedirectToAction("Index", "OrderHistory");

                // test redirect to 
                return RedirectToAction("ProductReview", "Home");
            }
        }

    }
}
