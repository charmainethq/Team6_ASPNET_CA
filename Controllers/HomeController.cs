using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index(string? searchStr)
    {

        // return gallery of products, including search https://www.w3schools.com/howto/howto_js_filter_lists.asp
        if (String.IsNullOrWhiteSpace(searchStr)) { }
        else { }
        //clicking on a product bring to Product page?

        return View();
    }


    public IActionResult AddData()
    {
        return View("Index");
    }

    public IActionResult ProductReview()
    {
        
        // sample product review (average, counts, summary of details)
        int ProductID = 1000; // sample test: 1000
        int averageRating = ProductData.AverageRating(ProductID);
        int ratingCounts = ProductData.CountRating(ProductID);
        List<ProductReview> reviewDetails = ProductData.ReviewDetails(ProductID);

        ViewData["avgRtg"] = averageRating;
        ViewData["rtgCnts"] = ratingCounts;
        ViewData["rvDts"] = reviewDetails;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

