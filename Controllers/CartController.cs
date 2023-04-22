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

using System.Linq;


namespace Team6.Controllers
{
    public class CartController : Controller
    {
        //View all items in cart
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");

            return View(cart);
        }


        //Add to cart - linked to button inputs
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
                    HttpContext.Session.SetInt32("cartCount", 0);
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
                    HttpContext.Session.SetInt32("cartCount", (int)HttpContext.Session.GetInt32("cartCount") + cartItem.Quantity);
                }
                else
                {
                    cartItem.Quantity += quantity;
                    HttpContext.Session.SetInt32("cartCount", (int)HttpContext.Session.GetInt32("cartCount") + quantity);
                }

                GetCartCount();
                HttpContext.Session.SetObjectAsJson("cart", cart);


            }
            return RedirectToAction("Index", "Cart");
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
                HttpContext.Session.SetInt32("cartCount", (int)HttpContext.Session.GetInt32("cartCount") - 1);
            }
            return RedirectToAction("Index");
        }



        public IActionResult Checkout()
        {

            int? customerId = HttpContext.Session.GetInt32("customerId");

            if (!customerId.HasValue)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");

                //dictionary of OrderItemId :  Quantity
                Dictionary<int, int> qtyPerOid = new Dictionary<int, int>();

                foreach (OrderItem cartItem in cart)
                {
                    cartItem.OrderID = NewId();
                    cartItem.OrderItemId = NewId();

                    //insert into Orders table
                    CartData.CreateOrder(cartItem, customerId, DateTime.Now);

                    //insert into OrderItem table
                    CartData.CreateOrderItem(cartItem);

                    qtyPerOid.Add(cartItem.OrderItemId, cartItem.Quantity);

                }

                //generate a list of activation codes for each order item based on quantity
                foreach (KeyValuePair<int, int> OidQtyPair in qtyPerOid)
                {
                    for (int i = 0; i < OidQtyPair.Value; i++)
                    {
                        CartData.AddActivationCode(OidQtyPair.Key, Guid.NewGuid().ToString());
                    }
                }

                //reset cart on checkout
                cart = new List<OrderItem>();
                HttpContext.Session.SetInt32("cartCount", 0);
                HttpContext.Session.SetObjectAsJson("cart", cart);

                // Display past orders to user
                return RedirectToAction("Index", "OrderHistory");
            }
        }
        
        //function for generating numerical GUIDs
        public int NewId()
        {
            string guidString = Guid.NewGuid().ToString().Replace("-", "");
            int guidNumber = Math.Abs(int.Parse(guidString.Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

            return guidNumber;
        }

        public JsonResult GetCartCount()
        {
            int cartCount = HttpContext.Session.GetInt32("cartCount") ?? 0;
            return Json(cartCount);
        }

        [HttpPost]
        public IActionResult UpdateCartItem(int cartItemId, int productId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");
            if (cart != null)
            {
                var cartItem = cart.FirstOrDefault(ci => ci.OrderItemId == cartItemId && ci.ProductID == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                    HttpContext.Session.SetObjectAsJson("cart", cart);
                    //HttpContext.Session.SetInt32("cartCount", (int)HttpContext.Session.GetInt32("cartCount") + quantity);
                }
            }

            return Ok();
        }

    }
}
