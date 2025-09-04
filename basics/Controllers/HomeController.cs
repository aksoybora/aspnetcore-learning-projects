using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using basics.Models;

namespace basics.Controllers;

//localhost
//localhost/home
//localhost/home/index

public class HomeController : Controller
{
    //home
    //home/index
    public IActionResult Index()
    {
        return View();
    }

    //home/contact
    public IActionResult Contact()
    {
        return View();
    }
}
