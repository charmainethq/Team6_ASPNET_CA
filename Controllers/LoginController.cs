using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(string username, string password)
        {
            
            // user is already inside a session, so redirect them
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            else if (username == null)
                return View();


            else if (AuthenticateUser(username, password) == true)
            {
                Customer user = GetCustomer(username);

                //create session object
                Session session = new Session() 
                {
                    SessionId = Guid.NewGuid().ToString(),
                    CustomerID = user.CustomerID
                };


                // set session cookies
                HttpContext.Session.SetString("SessionId", session.SessionId);
                HttpContext.Session.SetInt32("customerId", user.CustomerID);
                HttpContext.Session.SetString("fullName", user.FirstName + " " + user.LastName);

                TempData["fullName"] = user.FirstName + " " + user.LastName;
                TempData["sessionId"] = HttpContext.Session.GetString("SessionId");

                return RedirectToAction("Index");
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
                ViewBag.wrongCred = true;

            else if (password != userCredentials.Password)
                ViewBag.wrongPassword = true;

            else if (password == userCredentials.Password)
                authenticateFlag = true;

            return authenticateFlag;

        }

    }
}
