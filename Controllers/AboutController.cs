using Microsoft.AspNetCore.Mvc;
using LMS.Models;


namespace LMS.Controllers
{
   
public class AboutController : Controller
{
    public IActionResult Index()
    {
        ViewData["Msg"]="Leave Management System.";
        return View();
    }   
}
}
