using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Surveys.Core
{
    class TeamColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var team = (string)value;
            var color = Color.Transparent;

            switch (team)
            {
                case "Nacional":
                case "Cali":
                    color = Color.Green;
                    break;
                case "America":
                case "Santa fe":
                    color = Color.Red;
                    break;
                case "Medellin":
                    color = Color.BlueViolet;
                    break;              
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
