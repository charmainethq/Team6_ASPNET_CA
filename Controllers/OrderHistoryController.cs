using Microsoft.AspNetCore.Mvc;

namespace Team6.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
