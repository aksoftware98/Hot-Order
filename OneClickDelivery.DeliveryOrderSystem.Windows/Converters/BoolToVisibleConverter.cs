using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OneClickDelivery.DeliveryOrderSystem.Windows.Converters
{
    public class BoolToVisibileConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Bool to Visibile Convert
            if (value != null)
            {
                bool isVisibile = (bool)value;

                bool IsInverse = parameter != null ? System.Convert.ToBoolean(parameter) : true;

                if (IsInverse)
                {
                    if (isVisibile)
                        return Visibility.Visible;
                    else
                        return Visibility.Hidden;
                }
                else
                {
                    if (!isVisibile)
                        return Visibility.Visible;
                    else
                        return Visibility.Hidden;
                }


            }

            return Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Visible to Bool Convert

            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return true;
            else
                return false;
        }
    }
}
