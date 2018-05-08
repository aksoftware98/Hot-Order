using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OneClickDelivery.DeliveryOrderSystem.Windows.Validations
{
    public class NumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isValid = AKSoftware.Text.Validation.IsNumbers(value.ToString());

            if (isValid)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, null); 
        }
    }
}
