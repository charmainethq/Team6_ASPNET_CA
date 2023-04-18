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
            Debug.WriteLine("Product Image: " + product.ProductImage);

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
                var itemToRemove = cart.FirstOrDefault(item => item.ProductID == productId);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return RedirectToAction("Index");
        }


        //TODO: Checkout Cart. Create Order with OrderItems 
        //same as my purchases?

        public IActionResult Checkout()
        {
            int? customerId = HttpContext.Session.GetInt32("customerId");

            if (!customerId.HasValue)
            {
                RedirectToAction("Index", "Login");
            }

            else
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");
                foreach (OrderItem cartItem in cart)
                {
                    int guid1 = Convert.ToInt32(Guid.NewGuid().ToString().Replace("-", ""));

                    cartItem.OrderID = guid1;
                    cartItem.OrderItemId = Convert.ToInt32(Guid.NewGuid().ToString().Replace("-", ""));

                    //insert into Orders table
                    CartData.CreateOrder(cartItem, customerId, DateTime.Now);
                    //insert into OrderItem table
                    CartData.CreateOrderItem(cartItem);
                    //insert into ActivationCode table

                }

                // Create new order

                // Add cart items as order items to the new order


                // Get all orders for current customer
                //List<Order> pastOrders = CartData.GetOrdersByCustomer(customerId);

                // Display past orders to user
                return View("Index");
            }

            return View("Index");
        }
    }
}
