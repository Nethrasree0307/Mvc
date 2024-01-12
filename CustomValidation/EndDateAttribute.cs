using System.ComponentModel.DataAnnotations;
namespace LMS.CustomValidation
{
    public  class EndDateAttribute:ValidationAttribute
{
   private readonly string _endDate;

    public EndDateAttribute(string endDateProperty)
    {
        _endDate = endDateProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var startDate = (DateTime)value;
        var endDate = (DateTime)validationContext.ObjectType.GetProperty(_endDate).GetValue(validationContext.ObjectInstance);
        if (startDate < endDate)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Start Date must be less than end date.");
        }
    }
    }
}
   
