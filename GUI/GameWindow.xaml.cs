// <copyright file="GameWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GUI
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;
    using BusinessLogic;

    /// <summary>
    /// Interaction logic for GameWindow.xaml.
    /// </summary>
    public partial class GameWindow : Window
    {
        private GameLogic gL;
        private DispatcherTimer dT;
        private string uname;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWindow"/> class.
        /// </summary>
        /// <param name="uname">username.</param>
        public GameWindow(string uname)
        {
            this.InitializeComponent();
            this.uname = uname;
        }

        /// <summary>
        /// Gets the Game Logic instance.
        /// </summary>
        public GameLogic GL { get => this.gL; }

        /// <summary>
        /// Gets the Dispatcher Timer instance.
        /// </summary>
        public DispatcherTimer DT { get => this.dT;  }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            bool validKey = true;
            Direction movingDirection = this.ReturnMovementByKey(e.Key, ref validKey);
            if (this.gL.Minigame && validKey)
            {
                this.gL.ControllMinigame(movingDirection);
            }

            if (validKey)
            {
                this.gL.NavigateMovementOrInteraction(movingDirection);
                this.GA.InvalidateVisual();
            }
            else if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private Direction ReturnMovementByKey(Key key, ref bool validKey)
        {
            if (key == Key.Left)
            {
                return Direction.Left;
            }
            else if (key == Key.Right)
            {
                return Direction.Right;
            }
            else if (key == Key.Up)
            {
                return Direction.Up;
            }
            else if (key == Key.Down)
            {
                return Direction.Down;
            }
            else if (key == Key.E)
            {
                return Direction.None;
            }

            validKey = false;
            return Direction.None;
        }

        private void DT_Tick(object sender, EventArgs e)
        {
            this.gL.MoveRoommates();
            this.Finished();
            this.GA.InvalidateVisual();
        }

        private void Finished()
        {
            if (this.GL.FinishedCanClose)
            {
                this.Close();
                ScoresWindow sw = new ScoresWindow();
                sw.ShowDialog();
            }
            else if (this.GL.GameOver)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.gL = new BusinessLogicFactory(this.GA.ActualWidth, this.GA.ActualHeight, this.uname).GL;
            this.GA.SetupLogic(this.GL);

            this.dT = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(30),
            };
            this.dT.Tick += this.DT_Tick;
            this.dT.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainMenu mm = new MainMenu();
        }
    }
}
