// <copyright file="RegisterWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GUI
{
    using System.Linq;
    using System.Windows;
    using GUI.ViewModels;

    /// <summary>
    /// Interaction logic for RegisterWindow.xaml.
    /// </summary>
    public partial class RegisterWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterWindow"/> class.
        /// </summary>
        public RegisterWindow()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.Helper = new ScoreViewModel();
        }

        private string Username { get; set; }

        private ScoreViewModel Helper { get; set; }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            this.Helper = new ScoreViewModel();
            if (this.UserName.Text == string.Empty)
            {
                MessageBox.Show("You will need a name...");
            }
            else if (this.Helper.Highscores.Count(x => x.Nev == this.UserName.Text) >= 1)
            {
                MessageBox.Show("This name is already taken...");
            }
            else
            {
                this.Close();
                GameWindow gw = new GameWindow(this.UserName.Text);
                gw.ShowDialog();
            }
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
