using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;
using Team6.Extension;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Castle.Core.Resource;

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
            if (productId == null)
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
                        ProductImage = product.ProductImage,
                        Quantity = quantity,
                        ProductDescription = product.Description,
                        UnitPrice = product.UnitPrice,
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
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");

            if (cart != null)
            {
                var cartItem = cart.FirstOrDefault(ci => ci.ProductID == productId);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    HttpContext.Session.SetObjectAsJson("cart", cart);
                }
            }

            return RedirectToAction("Index", "Cart");
        }

        //TODO: Checkout Cart. Create Order with OrderItems 
        //same as my purchases?
        [HttpGet]
        public IActionResult Checkout(int customerId)
        {
            // Check if the customer is logged in
            int? currentCustomerId = HttpContext.Session.GetInt32("CustomerId");

            if (currentCustomerId == null)
            {
                return RedirectToAction("Login", "Home");
            }

            // Create new order


            // Add cart items as order items to the new order



            // Get all orders for current customer
            List<Order> pastOrders = OrderData.GetOrdersByCustomer(customerId);

            // Display past orders to user
            return View(pastOrders);
        }

    }
}
