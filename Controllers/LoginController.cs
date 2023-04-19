using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Team6.Data;
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

            //TODO?: Generate error message for empty username
            else if (username == null)
                return View();          


            else if (AuthenticateUser(username, password) == true)
            {
                Customer user = GetCustomer(username);

                //create session object
                Session session = new Session() 
                {
                    SessionID = Guid.NewGuid().ToString(),
                    CustomerID = user.CustomerID
                };


                // set session cookies
                HttpContext.Session.SetString("SessionId", session.SessionID);
                HttpContext.Session.SetInt32("customerId", user.CustomerID);
                HttpContext.Session.SetString("fullName", user.FirstName + " " + user.LastName);

                TempData["fullName"] = user.FirstName + " " + user.LastName;
                TempData["sessionId"] = HttpContext.Session.GetString("SessionId");


                //add or update user's session in database
                if (SessionData.GetSessionByCustomerId(user.CustomerID) == null)
                {
                    SessionData.AddSession(session);
                }
                else 
                {
                    SessionData.DeleteSession(user.CustomerID);
                    SessionData.AddSession(session);
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public Customer GetCustomer(string username)
        {
            return CustomerData.GetCustomerByUsername(username);
        }

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
            SessionData.DeleteSession(custId);  //delete from database
            HttpContext.Session.Clear();        //clear session cookies

            return RedirectToAction("Index", "Home");
        }
    }
}
