// <copyright file="ScoresWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GUI
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using GUI.ViewModels;
    using Repository;

    /// <summary>
    /// Interaction logic for Scores.xaml.
    /// </summary>
    public partial class ScoresWindow : Window
    {
        private ScoreViewModel VM;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoresWindow"/> class.
        /// </summary>
        public ScoresWindow()
        {
            this.InitializeComponent();
            this.VM = new ScoreViewModel();
            this.DataContext = this.VM;
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowDialog();
            this.Close();
        }
    }
}
