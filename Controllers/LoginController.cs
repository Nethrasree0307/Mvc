using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace LMS.Controllers
{

    public class LoginController : Controller
 {
    private ApplicationDBContext _context;
    private  IHttpContextAccessor ContextAccessor;
    private readonly ILogger<LoginController> _logger;
        public LoginController(ApplicationDBContext context,IHttpContextAccessor httpContextAccessor,ILogger<LoginController> logger)
         {
            ContextAccessor = httpContextAccessor;
            _context = context;
            _logger = logger;
         }

       
             
        public IActionResult AdminIndex()
        {
          return View();
        }
        [HttpGet]
        public IActionResult Login()
         {
            return View();
         }
        [HttpPost]
        public IActionResult Login(Logindetail logindetail)
        {
          if(ModelState.IsValid)
                {      
                   var exist =  _context.Employees.Where(u => u.Name == logindetail.Name && u.password == logindetail.password).FirstOrDefault();
                    if(exist!=null)
                     {   
                            ContextAccessor.HttpContext.Session.SetString("Name",exist.Name);
                            bool isValid = (exist.Name == logindetail.Name && exist.password == logindetail.password);
                            if (isValid)
                                {
                                HttpContext.Session.SetString("logindetail",logindetail.Name);
                                HttpContext.Response.Cookies.Append("Cookie", exist.Name); 
                                Response.Cookies.Append("LastLogin", DateTime.Now.ToString()); 
                                HttpContext.Session.SetObjectAsJson("logusers",logindetail); 
                                var claims = new List<Claim>{
                                new Claim(ClaimTypes.Name, exist.Name)
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                            return RedirectToAction("EmployeeIndex","Login");
                            }
                            else
                            {
                               Console.WriteLine("Not used");
                               ViewBag.LoginError = "Invalid username or password.";
                               return View(logindetail);
                            }
                     }
                  else
                    {
                      string result = AdminLogin.adminlogin(logindetail);
                if(result == "success")
                {
                     HttpContext.Session.SetString("logindetail",logindetail.Name);

                     return View("AdminIndex");
                }
                else
                {
                     ModelState.AddModelError(string.Empty,$"correct password is required");
                     return View("adminlogin",logindetail);
                }
                         Console.WriteLine("Not used");
                         TempData["errorUsername"] = "Username not found!";
                         ViewBag.Name = "Invalid username.";
                         return View(logindetail);
                    }
                }
            else
            {
               ViewBag.LoginError = "Invalid username or password.";
               return View(logindetail);
            }
        }

        public IActionResult EmployeeIndex()
        {
            return View();
        }

      
          public IActionResult Logout()
            {
              HttpContext.Session.Clear();
               HttpContext.Session.Remove("logindetail");
               HttpContext.Session.Remove("logusers");

               // Clear cookies
               HttpContext.Response.Cookies.Delete("Cookie");
               HttpContext.Response.Cookies.Delete("LastLogin");

              HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
              return RedirectToAction("Index","Home");
            }
     }
        
}

