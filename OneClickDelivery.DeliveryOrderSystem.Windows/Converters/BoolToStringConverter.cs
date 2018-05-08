using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OneClickDelivery.DeliveryOrderSystem.Windows.Converters
{
    public class BoolToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool target = (bool)value;

                if (target)
                    return "Yes";
                else
                    return "No";
            }

            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string target = value.ToString();

            if (target != "Yes")
            {
                return true;
            }
            else
            { return false; }
        }
    }
}
