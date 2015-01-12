using System;
using System.Windows.Data;

namespace YesCommander.Converter
{
    /// <summary>
    /// A value converter to determine whether the ImageComboBoxItem HighlightColor property is set or not.
    /// </summary>
    internal class IsImageComboBoxItemHighlightColorSetConverter : IValueConverter
    {
        /// <summary>
        /// Do convertion from nullable brush value to bool. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>return true if the HighlightColor is not null; otherwise, return false.</returns>
        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            if ( value != null )
                return true;
            else
                return false;
        }

        /// <summary>
        /// Not used for convert back.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
