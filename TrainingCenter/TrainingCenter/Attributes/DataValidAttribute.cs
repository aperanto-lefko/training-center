using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class DataValidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                if (DateTime.Compare((DateTime)value, DateTime.Now) > 0)
                {
                    return new ValidationResult("Дата не может быть в будущем");
                }
            }
            else
            {
                return new ValidationResult("Неверный формат даты");
            }
            return ValidationResult.Success;
        }

    }
}
