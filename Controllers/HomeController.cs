using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers;

public class HomeController : Controller
{
    private readonly ShopContext db;

    public HomeController(ShopContext db)
    {
        this.db = db;
    }

    public IActionResult Index()
    {
        // return gallery of products, including search https://www.w3schools.com/howto/howto_js_filter_lists.asp

        //clicking on a product bring to Product page?

        return View();
    }
    //This is CS's comment.



   







    public IActionResult AddData()
    {
        SampleData data = new SampleData(db);
        data.AddSampleData();

        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

