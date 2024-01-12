
using System.ComponentModel.DataAnnotations;
namespace LMS.CustomValidation
{
    public  class DateAttribute: ValidationAttribute
{
    public DateAttribute() : base("{0} Date sholud be greater than current date") //to set the default error message using the base constructor.
    {

    }
    public override bool IsValid(object value)//overrides the logic
    {
    DateTime datevalue = Convert.ToDateTime(value);
    if(datevalue >= DateTime.Now)
    return true;
    else
    return false;
    }
 }
}