using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

public class CustomAuthorizeFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var users = context.HttpContext.Session.GetObjectFromJson<Logindetail>("logusers");
            if (users == null)
            {
                Console.WriteLine("Error: Trying to enter without logging in...please login");
                context.Result = new RedirectToActionResult("", "Error", null);
                return;
            }
        Console.WriteLine($"users: {JsonConvert.SerializeObject(users.Name)}");
    }
}