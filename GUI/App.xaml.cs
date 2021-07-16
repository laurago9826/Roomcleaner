// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1652 // Enable XML documentation output

namespace GUI
#pragma warning restore SA1652 // Enable XML documentation output
#pragma warning restore SA1652 // Enable XML documentation output
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainMenu window = new MainMenu();
            window.Title = "RoomCleaner";
            window.ShowDialog();
        }
    }
}
