// <copyright file="CurrentPlayerToColourConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GUI.Helpers
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Repository;

    /// <summary>
    /// For the current player the convert method returns "1" opacity.
    /// </summary>
    public class CurrentPlayerToColourConverter : IValueConverter
    {
        /// <summary>
        /// Converts the actual characters listbox item opacity.
        /// </summary>
        /// <param name="value">value.</param>
        /// <param name="targetType">targetType.</param>
        /// <param name="parameter">parameter.</param>
        /// <param name="culture">culture.</param>
        /// <returns>returns double.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string score = (string)value;
            if (score == ScoreContext.Name)
            {
                return 1;
            }

            return 0.6;
        }

        /// <summary>
        /// Not used method.
        /// </summary>
        /// <param name="value">value.</param>
        /// <param name="targetType">tagretType.</param>
        /// <param name="parameter">parameter.</param>
        /// <param name="culture">culture.</param>
        /// <returns>returns nothing.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
