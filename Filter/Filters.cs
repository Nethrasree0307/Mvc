using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMS.Filter
{
    public class Filters :Attribute, IActionFilter,IExceptionFilter,IResultFilter
    {
     
        public void OnResultExecuted(ResultExecutedContext context)//operations after the result has been executed
        {
           Console.WriteLine("Result Filter :Method OnActionExecuted works here!")  ; 
        }
        public void OnResultExecuting(ResultExecutingContext context)//executed before the execution of an action method and result
        {
           Console.WriteLine("Result Filter :Method OnActionExecuting works here!")  ; 
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
          Console.WriteLine("Action Filter :Method OnActionExecuted")  ; 
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
          Console.WriteLine("Action Filter :Method OnActionExecuting")  ;     
        }
        
         public void OnException(ExceptionContext filtercontext)//handles unhandled exceptions
        {
             Exception error = filtercontext.Exception;
             Console.WriteLine("Error is occuring");
             filtercontext.ExceptionHandled=true;//indicates exceptions has  been handled
             filtercontext.Result = new ViewResult()
                {
                        ViewName = "login"//exception occurs application renders to view 
                };
        }
       
    }
}

      
    