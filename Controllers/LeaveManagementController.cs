using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Models;
using System.Collections.Generic;
using LMS.Data;

namespace LMS.Controllers
{
    public class LeaveManagementController : Controller
    {
      private ApplicationDBContext _context;
      public LeaveManagementController(ApplicationDBContext context)
      {
         _context = context;
      }
          [CustomAuthorizeFilter]  
    //To Apply Leave
    [HttpGet]
    public IActionResult ToApply()
    {
        return View();
    }
    [CustomAuthorizeFilter]
    [HttpPost]
    public async Task<IActionResult> ToApply(Leave leave)
    {
        if(ModelState.IsValid)
        {
            try
            {
                _context.Add(leave);
                await _context.SaveChangesAsync();
                return RedirectToAction("ToViewE");
            }
            catch(Exception exception)
            {
                ModelState.AddModelError(string.Empty,$"Something went wrong { exception.Message}");
            }
        }
        ModelState.AddModelError(string.Empty,$"Something went wrong invalid model");
        return View(leave);
    }
   //To View the list of Leave Details by Admin
    public async Task<IActionResult> ToViewA()
     {
      var leave = await _context.LeaveDetails.ToListAsync(); 
      return View(leave); 
     }
      [CustomAuthorizeFilter]
       public IActionResult ToViewE()
      {  
       string logindetails =  HttpContext.Session.GetString("logindetail");
       List<Leave> leave = _context.LeaveDetails.Where(u=>u.Name==logindetails).ToList();
       return View("ToViewE",leave);   
      }
   //To Approve or Reject by Admin
    [HttpGet]
    public async Task<IActionResult> Approve( int id)
    {
      var exist = await _context.LeaveDetails.Where(x => x.EmployeeID==id).FirstOrDefaultAsync();
      return View(exist);
    }
    [HttpPost]
    public async Task<IActionResult> Approve(Leave leave)
    {   
       try
            {
              var exist =  _context.LeaveDetails.Where(x => x.EmployeeID==leave.EmployeeID).FirstOrDefault();
              if(exist != null)
              {
               
                exist.Name=leave.Name;
                exist.LeaveType=leave.LeaveType;
                exist.StartDate=leave.StartDate;
                exist.EndDate=leave.EndDate;
                exist.Description=leave.Description;
                exist.AcceptOrReject=leave.AcceptOrReject;
              }
              await _context.SaveChangesAsync();
              return RedirectToAction("ToViewA");
            }
            catch(Exception exception)
            { 
                ModelState.AddModelError(string.Empty,$"Something went wrong {exception.Message}");
            }
        
        return View(leave);
    }
    

    public  ActionResult Delete(int id)//To Remove the Leave details
    {
        if(ModelState.IsValid)
        {
            try
            {
              var exist =  _context.LeaveDetails.FirstOrDefault(m => m.EmployeeID==id);
              if(exist != null)
              {
                _context.LeaveDetails.Remove(exist);
                _context.SaveChanges();
                return RedirectToAction("ToViewA");
               }
            }
            catch(Exception exception)
            {
                ModelState.AddModelError(string.Empty,$"Something went wrong {exception.Message}");
            }

        }
     ModelState.AddModelError(string.Empty,$"Something went wrong invalid model");
     return RedirectToAction("ToViewA");
    }
 }
}



    
