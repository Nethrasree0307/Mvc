using Microsoft.AspNetCore.Mvc;
public class ErrorController:Controller
{
[Route("Error/{statusCode}")]
public IActionResult Error(int statusCode) 
{    
    switch(statusCode) 
    {
        case 404:
        ViewBag.Message = "Not Found : Sorry, Page trying to enter do not exist";
        break;
        case 500:
        ViewBag.Message = "Server Error : Unexpected error on the server side while processing the request";
        break;
        case 245:
        ViewBag.Message = "DatabaseError(SqlServer):The string value cannot be parsed into a number.";
        break;
        case 400:
        ViewBag.Message = "Bad Request : The server cannot process the request due to an issue from the client.";   //due to file may be too large
        break;
        case 200:
        ViewBag.Message = "No Content but response has been succesfully delivered";
        break;
        default:
        ViewBag.Message = "Please Run Web Api Application ";
        break;
    }
    return View ("ErrorPage");
    } 
}