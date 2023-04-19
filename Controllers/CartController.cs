﻿using Microsoft.AspNetCore.Mvc;
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
using System.Linq;

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
                var cartItem = cart.FirstOrDefault(ci => ci.ProductID == productId);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    HttpContext.Session.SetObjectAsJson("cart", cart);
                }
            }
            return RedirectToAction("Index", "Cart");
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

                foreach(KeyValuePair<int,int> OidQtyPair in qtyPerOid)
                {
                    for (int i = 0; i < OidQtyPair.Value; i++)
                    {
                        CartData.AddActivationCode(OidQtyPair.Key, Guid.NewGuid().ToString());
                    }
                }


                // Display past orders to user
                return RedirectToAction("Index", "OrderHistory");
            }
        }

        public int NewId()
        {
            string guidString = Guid.NewGuid().ToString().Replace("-", "");
            int guidNumber = int.Parse(guidString.Substring(0, 8), System.Globalization.NumberStyles.HexNumber);

            return guidNumber;
        }
    }
}
