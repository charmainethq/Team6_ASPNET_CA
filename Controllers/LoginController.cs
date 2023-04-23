using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Team6.Data;
using Team6.Extension;
using Team6.Models;

namespace Team6.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index(string username, string password)
        {
            
            // user is already inside a session, do nothing and redirect
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            else if (username == null)
                return View();          


            else if (AuthenticateUser(username, password) == true)
            {
                Customer user = CustomerData.GetCustomerByUsername(username);

                HttpContext.Session.SetInt32("customerId", user.CustomerID);
                HttpContext.Session.SetString("fullName", user.FirstName + " " + user.LastName);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //authentication method including error messages
        public bool AuthenticateUser(string username, string password)
        {
            bool authenticateFlag = false;

            Customer userCredentials = CustomerData.GetCustomerByUsername(username);

			if (userCredentials == null)
                ViewData["error"] = "Username is incorrect";

            else if (password != userCredentials.Password)
				ViewData["error"] = "Wrong password";

            else if (password == userCredentials.Password)
                authenticateFlag = true;

            return authenticateFlag;

        }


        public IActionResult Logout()
        {
            int? custId = HttpContext.Session.GetInt32("customerId");
            HttpContext.Session.Clear();        //clear session cookies

            return RedirectToAction("Index", "Home");
        }
    }
}
