// <copyright file="MainCharacter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Class of MAin character, inhareted from MovingObject.
    /// </summary>
    public class MainCharacter : MovingObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainCharacter"/> class.
        /// Main caharcter constructor.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="height">height of character.</param>
        /// <param name="width">width of character.</param>
        /// <param name="window_width">window width.</param>
        /// <param name="window_height">window height.</param>
        /// <param name="username">username of current player.</param>
        public MainCharacter(int x, int y, int height, int width, double window_width, double window_height)
            : base(x, y, height, width, window_width, window_height)
        {
        }

        /// <summary>
        /// Gets or sets the number of collisions with roommate.
        /// </summary>
        public int NumberOfNewCollisionsWithRoommate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mainCharacter is holding folded clothes.
        /// </summary>
        public bool IsHoldingFoldedClothes { get; set; }

        /// <summary>
        /// Moves the MainCharacter in a direction.
        /// </summary>
        /// <param name="direction">input direction.</param>
        /// <param name="items">IGameModel items list.</param>
        public void MoveDirection(Direction direction, List<IGameModel> items)
        {
            if (direction == Direction.Left && this.CollidesWithOtherItemFromWhichDirection(items) != Direction.Left)
            {
                this.TryMoveLeft();
            }
            else if (direction == Direction.Right && this.CollidesWithOtherItemFromWhichDirection(items) != Direction.Right)
            {
                this.TryMoveRight();
            }
            else if (direction == Direction.Up && this.CollidesWithOtherItemFromWhichDirection(items) != Direction.Up)
            {
                this.TryMoveUp();
            }
            else if (direction == Direction.Down && this.CollidesWithOtherItemFromWhichDirection(items) != Direction.Down)
            {
                this.TryMoveDown();
            }

            this.PositionCounter = (this.PositionCounter + 1) % 6;
        }

        /// <summary>
        /// Returns the object that the MainCharacter collided with.
        /// </summary>
        /// <param name="items">IGameModle items list.</param>
        /// <returns>Collided object.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "mert")]
        public StaticObject ReturnInteractingObjectsWhichCollided(List<IGameModel> items)
        {
            Rect thisRect = new Rect(this.GetObjectArea.X - (this.movePixelsUnit * 6), this.GetObjectArea.Y - (6 * this.movePixelsUnit), this.GetObjectArea.Width + (12 * this.movePixelsUnit), this.GetObjectArea.Height + (12 * this.movePixelsUnit));
            List<IGameModel> interactingObjects = new List<IGameModel>();
            foreach (var i in items)
            {
                if ((i is StaticObject) && ((i as StaticObject).Type == ObjectType.Dresser || (i as StaticObject).Type == ObjectType.PileOfClothes))
                {
                    if (i.GetObjectArea.IntersectsWith(thisRect))
                    {
                        return i as StaticObject;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Determines the direction of colliding item.
        /// </summary>
        /// <param name="items">IGameModel items list.</param>
        /// <returns>Direction of collision.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "igen")]
        public Direction CollidesWithOtherItemFromWhichDirection(List<IGameModel> items)
        {
            Rect thisRect = new Rect(this.GetObjectArea.X - this.movePixelsUnit, this.GetObjectArea.Y - this.movePixelsUnit, this.GetObjectArea.Width + (2 * this.movePixelsUnit), this.GetObjectArea.Height + (2 * this.movePixelsUnit));
            foreach (var i in items)
            {
                if (i != this && i.GetObjectArea.IntersectsWith(thisRect))
                {
                    if (i is Roommate || !((i as StaticObject).Type != ObjectType.Dresser && (i as StaticObject).Type != ObjectType.PileOfClothes))
                    {
                        if (i is Roommate)
                        {
                            this.NumberOfNewCollisionsWithRoommate++;
                        }

                        return this.DetermineCollidingDirection(i, this);
                    }
                    else
                    {
                        this.PushOneUnitAway(i as StaticObject, this);
                    }
                }
            }

            return Direction.None;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "i", Justification = "asd")]
        private void PushOneUnitAway(StaticObject i, MainCharacter mainCharacter)
        {
            Direction original = this.Movement;
            Direction d = this.DetermineOppositeDirection(mainCharacter.Movement);
            if (d == Direction.Down)
            {
                this.TryMoveDown();
            }
            else if (d == Direction.Up)
            {
                this.TryMoveUp();
            }
            else if (d == Direction.Left)
            {
                this.TryMoveLeft();
            }
            else
            {
                this.TryMoveRight();
            }

            this.Movement = original;
        }
    }
}
