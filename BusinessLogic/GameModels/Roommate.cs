// <copyright file="Roommate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Roommate , inherated from movingobject.
    /// </summary>
    public class Roommate : MovingObject
    {
        private static Random rand = new Random();
        private static int roommateIDCounter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Roommate"/> class.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="height">object height.</param>
        /// <param name="width">object width.</param>
        /// <param name="window_width">window width.</param>
        /// <param name="window_height">window height.</param>
        public Roommate(int x, int y, int height, int width, double window_width, double window_height)
            : base(x, y, height, width, window_width, window_height)
        {
            this.movePixelsUnit = rand.Next(2, 5);
            this.Movement = Directions[rand.Next(0, 4)];
            this.MoveDirection();
            this.RoommateID = ++roommateIDCounter;
        }

        /// <summary>
        /// Gets or sets roommate id.
        /// </summary>
        public int RoommateID { get; set; }

        /// <summary>
        /// Moves the roommate in direction.
        /// </summary>
        public void MoveDirection()
        {
            if (this.Movement == Direction.Left)
            {
                if (!this.TryMoveLeft())
                {
                    this.Movement = this.DetermineOppositeDirection(this.Movement);
                }
            }
            else if (this.Movement == Direction.Right)
            {
                if (!this.TryMoveRight())
                {
                    this.Movement = this.DetermineOppositeDirection(this.Movement);
                }
            }
            else if (this.Movement == Direction.Up)
            {
                if (!this.TryMoveUp())
                {
                    this.Movement = this.DetermineOppositeDirection(this.Movement);
                }
            }
            else if (this.Movement == Direction.Down)
            {
                if (!this.TryMoveDown())
                {
                    this.Movement = this.DetermineOppositeDirection(this.Movement);
                }
            }

            this.PositionCounter = (this.PositionCounter + 1) % 6; // 3 pozícióváltáskor változik a kép
        }

        /// <summary>
        /// Moves the roommate in other direction if collides with something.
        /// </summary>
        /// <param name="items">IGameModel items list.</param>
        public void MoveDirectionIfCollided(List<IGameModel> items)
        {
            Rect thisRect = new Rect(this.GetObjectArea.X - (this.movePixelsUnit / 2), this.GetObjectArea.Y - (this.movePixelsUnit / 2), this.GetObjectArea.Width + this.movePixelsUnit, this.GetObjectArea.Height + this.movePixelsUnit);
            foreach (var e in items)
            {
                if (e != this && e.GetObjectArea.IntersectsWith(thisRect))
                {
                    this.Movement = this.DetermineOppositeDirection(this.DetermineCollidingDirection(e, this));
                    this.MoveDirection();
                }
            }

            this.RandomlyChangeDirection();
            this.MoveDirection();
        }

        private void RandomlyChangeDirection()
        {
            if (rand.Next(0, 50) == 0)
            {
                this.Movement = Directions[rand.Next(0, 4)];
            }
        }
    }
}
