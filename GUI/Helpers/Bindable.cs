// <copyright file="Bindable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GUI.Helpers
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Bindable class.
    /// </summary>
    public class Bindable : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChange method.
        /// </summary>
        /// <param name="s">string param.</param>
        protected void OnPropertyChange([CallerMemberName]string s = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }
    }
}
