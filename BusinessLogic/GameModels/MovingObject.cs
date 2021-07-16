// <copyright file="MovingObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Enum for direction.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Left.
        /// </summary>
        Left,

        /// <summary>
        /// Right.
        /// </summary>
        Right,

        /// <summary>
        /// Up.
        /// </summary>
        Up,

        /// <summary>
        /// Down.
        /// </summary>
        Down,

        /// <summary>
        /// None.
        /// </summary>
        None,
    }

    /// <summary>
    /// MovingObject class.
    /// </summary>
    public abstract class MovingObject : IGameModel
    {
        /// <summary>
        /// Dictionary of directions.
        /// </summary>
        public static Dictionary<int, Direction> Directions = new Dictionary<int, Direction>()
        {
            { 0, Direction.Left },
            { 1, Direction.Right },
            { 2, Direction.Up },
            { 3, Direction.Down },
        };

        /// <summary>
        /// Window width.
        /// </summary>
        protected double windowWidth;

        /// <summary>
        /// Window height.
        /// </summary>
        protected double windowHeight;

        /// <summary>
        /// Move pixel units.
        /// </summary>
        protected int movePixelsUnit = 6;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovingObject"/> class.
        /// Constructor of MovingObject.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="height">object height.</param>
        /// <param name="width">object width.</param>
        /// <param name="window_width">window width.</param>
        /// <param name="window_height">window height.</param>
        public MovingObject(int x, int y, int height, int width, double window_width, double window_height)
        {
            this.windowWidth = window_width;
            this.windowHeight = window_height;

            this.X_coordinate = x;
            this.Y_coordinate = y + (height / 3);
            this.Height = height / 3;
            this.Width = width;
        }

        /// <summary>
        /// Gets or sets x coordinate.
        /// </summary>
        public int X_coordinate { get; set; }

        /// <summary>
        /// Gets or sets y coordinate.
        /// </summary>
        public int Y_coordinate { get; set; }

        /// <summary>
        /// Gets or sets object height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets object width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets position counter.
        /// </summary>
        public int PositionCounter { get; set; }

        /// <summary>
        /// Gets rect for view.
        /// </summary>
        public Rect RectForView
        {
            get { return new Rect(this.X_coordinate, this.Y_coordinate - (this.Height * 2), this.Width, this.Height * 3); }
        }

        /// <summary>
        /// Gets or sets movement direction.
        /// </summary>
        public Direction Movement { get; set; }

        /// <summary>
        /// Gets object area.
        /// </summary>
        public Rect GetObjectArea
        {
            get { return new Rect(this.X_coordinate, this.Y_coordinate, this.Width, this.Height); }
        }

        /// <summary>
        /// Tryes to move left.
        /// </summary>
        /// <returns>returns false if cant.</returns>
        public bool TryMoveLeft()
        {
            if (!this.CollidesWithLeftWall())
            {
                this.Movement = Direction.Left;
                this.X_coordinate -= this.movePixelsUnit;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tryes to move right.
        /// </summary>
        /// <returns>returns false if cant.</returns>
        public bool TryMoveRight()
        {
            if (!this.CollidesWithRightWall())
            {
                this.Movement = Direction.Right;
                this.X_coordinate += this.movePixelsUnit;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tryes to move up.
        /// </summary>
        /// <returns>returns false if cant.</returns>
        public bool TryMoveUp()
        {
            if (!this.CollidesWithTopWall())
            {
                this.Movement = Direction.Up;
                this.Y_coordinate -= this.movePixelsUnit;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tryes to move down.
        /// </summary>
        /// <returns>returns false if cant.</returns>
        public bool TryMoveDown()
        {
            if (!this.CollidesWithBottomWall())
            {
                this.Movement = Direction.Down;
                this.Y_coordinate += this.movePixelsUnit;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines opposite direction.
        /// </summary>
        /// <param name="direction">current direction.</param>
        /// <returns>opposite direction.</returns>
        public Direction DetermineOppositeDirection(Direction direction)
        {
            var directionID = Directions.Where(x => x.Value == direction).Select(x => x.Key).FirstOrDefault();
            if (directionID % 2 == 0)
            {
                return Directions[++directionID];
            }
            else
            {
                return Directions[--directionID];
            }
        }

        /// <summary>
        /// Returns the size of intersection area from left.
        /// </summary>
        /// <param name="main">main area.</param>
        /// <param name="comparedRect">compared area.</param>
        /// <returns>size of intersection.</returns>
        public double LeftIntersectionArea(Rect main, Rect comparedRect)
        {
            Rect mainLeft = new Rect(main.X - main.Width, main.Y, main.Width, main.Height);
            Rect intersectionRect = Rect.Intersect(comparedRect, mainLeft);
            if (double.IsInfinity(intersectionRect.Width * intersectionRect.Height))
            {
                return 0;
            }

            return intersectionRect.Width * intersectionRect.Height;
        }

        /// <summary>
        /// Returns the size of intersection area from right.
        /// </summary>
        /// <param name="main">main area.</param>
        /// <param name="comparedRect">compared area.</param>
        /// <returns>size of intersection.</returns>
        public double RightIntersectionArea(Rect main, Rect comparedRect)
        {
            Rect mainLeft = new Rect(main.X + main.Width, main.Y, main.Width, main.Height);
            Rect intersectionRect = Rect.Intersect(comparedRect, mainLeft);
            if (double.IsInfinity(intersectionRect.Width * intersectionRect.Height))
            {
                return 0;
            }

            return intersectionRect.Width * intersectionRect.Height;
        }

        /// <summary>
        /// Returns the size of intersection area from above.
        /// </summary>
        /// <param name="main">main area.</param>
        /// <param name="comparedRect">compared area.</param>
        /// <returns>size of intersection.</returns>
        public double AboveIntersectionArea(Rect main, Rect comparedRect)
        {
            Rect mainLeft = new Rect(main.X, main.Y - main.Height, main.Width, main.Height);
            Rect intersectionRect = Rect.Intersect(comparedRect, mainLeft);
            if (double.IsInfinity(intersectionRect.Width * intersectionRect.Height))
            {
                return 0;
            }

            return intersectionRect.Width * intersectionRect.Height;
        }

        /// <summary>
        /// Returns the size of intersection area from below.
        /// </summary>
        /// <param name="main">main area.</param>
        /// <param name="comparedRect">compared area.</param>
        /// <returns>size of intersection.</returns>
        public double BelowIntersectionArea(Rect main, Rect comparedRect)
        {
            Rect mainLeft = new Rect(main.X, main.Y + main.Height, main.Width, main.Height);
            Rect intersectionRect = Rect.Intersect(comparedRect, mainLeft);
            if (double.IsInfinity(intersectionRect.Width * intersectionRect.Height))
            {
                return 0;
            }

            return intersectionRect.Width * intersectionRect.Height;
        }

        /// <summary>
        /// Determines the direction of collision.
        /// </summary>
        /// <param name="comparedItem">compared item.</param>
        /// <param name="mainItem">observed item.</param>
        /// <returns>direction of collosion.</returns>
        public Direction DetermineCollidingDirection(IGameModel comparedItem, IGameModel mainItem)
        {
            double left = this.LeftIntersectionArea(mainItem.GetObjectArea, comparedItem.GetObjectArea);
            double right = this.RightIntersectionArea(mainItem.GetObjectArea, comparedItem.GetObjectArea);
            double up = this.AboveIntersectionArea(mainItem.GetObjectArea, comparedItem.GetObjectArea);
            double down = this.BelowIntersectionArea(mainItem.GetObjectArea, comparedItem.GetObjectArea);

            double[] intersectionAreas = { left, right, up, down };

            int index = this.GetMaximumArea(intersectionAreas);
            if (index >= 0)
            {
                return Directions[index];
            }

            return Direction.None;
        }

        /// <summary>
        /// Gets the maximum from the intersection areas.
        /// </summary>
        /// <param name="intersectionAreas">intersection areas.</param>
        /// <returns>returns the maximum.</returns>
        public int GetMaximumArea(double[] intersectionAreas)
        {
            int indexOfMaxArea = 0;
            for (int i = 1; i < intersectionAreas.Length; i++)
            {
                if (intersectionAreas[i] > intersectionAreas[indexOfMaxArea])
                {
                    indexOfMaxArea = i;
                }
            }

            if (intersectionAreas[indexOfMaxArea] > 0)
            {
                return indexOfMaxArea;
            }

            return -1;
        }

        /// <summary>
        /// Determines if object collides with left wall.
        /// </summary>
        /// <returns>returns bool.</returns>
        public bool CollidesWithLeftWall()
        {
            return this.X_coordinate <= this.movePixelsUnit;
        }

        /// <summary>
        /// Determines if object collides with right wall.
        /// </summary>
        /// <returns>returns bool.</returns>
        public bool CollidesWithRightWall()
        {
            return this.X_coordinate >= this.windowWidth - this.movePixelsUnit - this.Width;
        }

        /// <summary>
        /// Determines if object collides with top wall.
        /// </summary>
        /// <returns>returns bool.</returns>
        public bool CollidesWithTopWall()
        {
            return this.Y_coordinate <= this.movePixelsUnit;
        }

        /// <summary>
        /// Determines if object collides with bottom wall.
        /// </summary>
        /// <returns>returns bool.</returns>
        public bool CollidesWithBottomWall()
        {
            return this.Y_coordinate >= this.windowHeight - this.movePixelsUnit - this.Height;
        }
    }
}
