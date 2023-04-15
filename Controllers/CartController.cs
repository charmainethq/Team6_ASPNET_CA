using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;
using Team6.Extension;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Team6.Controllers
{
    public class CartController : Controller
    {
        //TODO: View all items in cart
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");

            return View(cart);
        }


        //TODO: Add to cart - linked to button input from gallery
        public IActionResult Details(int productId, int quantity)
        {
            Product product = CartData.GetProductById(productId);

            if(productId == null)
            {
                return View();
            }
            else
            {
                //Get current cart
                var cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");
                if (cart == null)
                {
                    cart = new List<OrderItem>();
                }

                //check if item already in cart
                var cartItem = cart.FirstOrDefault(ci => ci.ProductID == productId);
                if (cartItem == null)
                {
                    cartItem = new OrderItem
                    {
                        ProductID = product.ProductId,
                        ProductName = product.Name,
                        Quantity = quantity,
                        ProductDescription = product.Description,
                        Price = product.Price,
                    };
                    cart.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += quantity;
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return RedirectToAction("Index","Cart");
        }
       

        //TODO: Remove from cart - linked to buttons from cart view


        //TODO: Checkout Cart. Create Order with OrderItems

        //TODO: Calculator function for product total (2x Office 365 is $500)

        //TODO: Calculator function for cart total


    }
}
