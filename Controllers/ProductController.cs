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


        public IActionResult Product(string Id)

        {
            Product product = ProductData.GetProductById(Id);
            ViewBag.product = product;
            return View();
        }
        
        
        //return view of the individual product based on product Id or name
            
        


        


        public IActionResult CreateReview()
        {
            //take in rating and message(optional) to create a Review object. Display should be at the bottom of the product page
            //View: have 5 buttons, each represented by a star without fill. On clicking one button, the value (eg. 4 ) should be recorded
            //and the icons for buttons 1-4 should switch to filled 
            return View();
        }
    }
}
