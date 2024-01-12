using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LMS.Models;
using LMS.Data;
using System;

namespace LMS.Controllers;
public class EmployeeManagementController : Controller
    {
        private IHttpContextAccessor contextaccessor;
        private ILogger<EmployeeManagementController> _logger;
        private ApplicationDBContext _context;
        public EmployeeManagementController(ApplicationDBContext context,IHttpContextAccessor httpContextAccessor,ILogger<EmployeeManagementController> logger)
            {
                _context = context;
                _logger = logger;
                contextaccessor = httpContextAccessor;
            }

      
     public async Task<IActionResult> Index() //to view employee detail
     {
      try{
        
             _logger.LogInformation ("Data's are Displayed");//logger
             
             HttpClientHandler clientHandler = new HttpClientHandler();//Webapi integration
             clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
             { return true; };
             using (var client = new HttpClient(clientHandler))
                 {
                    client.BaseAddress = new Uri("https://localhost:7189/api/EmployeeManagement/");// address set to url of api
                    client.DefaultRequestHeaders.Accept.Clear(); //configured to accept JSON response
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("/api/EmployeeManagement/Get");
                         if (response.IsSuccessStatusCode)//success statuscode 200
                            {
                                var data = response.Content.ReadAsStringAsync().Result;
                                var employee = JsonConvert.DeserializeObject<List<Employee>>(data);
                                return View(employee);
                            }
                         else
                            {
                                _logger.LogError("Error! Please check");
                                return View("Error");
                            }
                }
    }  
     catch (Exception ex)
    {
      Console.WriteLine( "Please Run WEB API Application");
      return RedirectToAction("ErrorPage","Error");
    } 
    }
    //To Add Employee Details
    [HttpGet]
    
    public IActionResult Create()
    {
        return View();
    } 
    
    [HttpPost]
     
     public async Task<IActionResult> Create(Employee employee)
    {
      
        if(ModelState.IsValid)
        {
            try
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception exception)
            {
                ModelState.AddModelError(string.Empty,$"Error { exception.Message}");
            }
        }
        ModelState.AddModelError(string.Empty,$" invalid model");
        return View(employee);
    }
      
    [HttpGet]
    [Route("EmployeeManagement/{id:int}")]
    public async Task<IActionResult> Edit( int id) //To Edit Employee Details
    { 
          try
          {
          var exist = await _context.Employees.Where(x => x.EmployeeID==id).FirstOrDefaultAsync();
          return View(exist);
          }
          catch(Exception exception)
          {    
           ModelState.AddModelError(string.Empty,$"Something went wrong {exception.Message}");
          }
        
      return View();
    }
    [HttpPost]
    [Route("EmployeeManagement/{id:int}")]
 
    public async Task<IActionResult> Edit( Employee employee)
    {
       if(ModelState.IsValid)
        {
            try
            {
              var exist =  _context.Employees.Where(x => x.EmployeeID==employee.EmployeeID).FirstOrDefault();
              if(exist != null)
              {
                exist.Name=employee.Name;
                exist.Age=employee.Age;
                exist.EmailId=employee.EmailId;
                exist.Address=employee.Address;
                exist.password=employee.password;
                exist.contact=employee.contact;
              }
              await _context.SaveChangesAsync();
              return RedirectToAction("Index");
            }
            catch(Exception exception)
            {
                ModelState.AddModelError(string.Empty,$"Something went wrong {exception.Message}");
            }
        }
      ModelState.AddModelError(string.Empty,$"Something went wrong invalid model");
      return View(employee);
     }
  
    public  ActionResult Delete(int id)  //To  Delete Employee Details
    {
        if(ModelState.IsValid)
        {
            try
            {
              var exist =  _context.Employees.FirstOrDefault(m => m.EmployeeID==id);
              if(exist != null)
              {
                _context.Employees.Remove(exist);
                _context.SaveChanges();
                return RedirectToAction("Index");
               }
            }
            catch(Exception exception)
            {
                ModelState.AddModelError(string.Empty,$"Something went wrong {exception.Message}");
            }

        }
           ModelState.AddModelError(string.Empty,$"Something went wrong invalid model");
         return RedirectToAction("Index");
    }
}
   
