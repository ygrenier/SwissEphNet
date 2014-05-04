using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SweWPF.Converters
{

    /// <summary>
    /// Convert double to Degrees format
    /// </summary>
    public class DoubleToDegreesFormatConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            double val = System.Convert.ToDouble(value);
            string fmt = parameter != null ? parameter.ToString() : null;
            return SwissEphNet.SwissEph.FormatToDegreeMinuteSecond(val, fmt);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

    }

}
