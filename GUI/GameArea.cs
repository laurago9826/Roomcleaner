// <copyright file="GameArea.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using BusinessLogic;

    /// <summary>
    /// GameArea class.
    /// </summary>
    public class GameArea : FrameworkElement
    {
        private static readonly Dictionary<char, string> NumberToImage = new Dictionary<char, string>
        {
            { '0', @"../../Images/numbers/0.png" },
            { '1', @"../../Images/numbers/1.png" },
            { '2', @"../../Images/numbers/2.png" },
            { '3', @"../../Images/numbers/3.png" },
            { '4', @"../../Images/numbers/4.png" },
            { '5', @"../../Images/numbers/5.png" },
            { '6', @"../../Images/numbers/6.png" },
            { '7', @"../../Images/numbers/7.png" },
            { '8', @"../../Images/numbers/8.png" },
            { '9', @"../../Images/numbers/9.png" },
            { ':', @"../../Images/numbers/doubledotttt.png" },
            { '/', @"../../Images/numbers/slash.png" },
        };

        private static readonly Dictionary<Direction, string> ToArrowUri = new Dictionary<Direction, string>()
        {
            { Direction.Down, @"../../Images/FoldingGame/Down" },
            { Direction.Left, @"../../Images/FoldingGame/Left" },
            { Direction.Right, @"../../Images/FoldingGame/Right" },
            { Direction.Up, @"../../Images/FoldingGame/Up" },
        };

        private GameLogic gL;

        /// <summary>
        /// Gets the gamelogic.
        /// </summary>
        public GameLogic GL { get => this.gL; }

        /// <summary>
        /// Sets the GameLogic parameter.
        /// </summary>
        /// <param name="gL">GameLogic parameter.</param>
        public void SetupLogic(GameLogic gL)
        {
            this.gL = gL;
        }

        /// <summary>
        /// Draws a moving character.
        /// </summary>
        /// <param name="drawingContext">drawing context.</param>
        /// <param name="item">moving item.</param>
        public void PlaceMovingCharacter(DrawingContext drawingContext, MovingObject item)
        {
            string prefix = string.Empty;
            string dir = string.Empty;
            if (item is MainCharacter)
            {
                dir = "mainCharacter";
                if ((item as MainCharacter).IsHoldingFoldedClothes)
                {
                    prefix = "sg";
                }
            }
            else
            {
                dir = "roommate_" + (((item as Roommate).RoommateID % 3) + 1);
            }

            this.DrawMovingObjectAccordingToPosition(drawingContext, item, dir, prefix);
        }

        /// <summary>
        /// Draws a moving character according to their position.
        /// </summary>
        /// <param name="drawingContext">drawing context.</param>
        /// <param name="item">moving item.</param>
        /// <param name="dir">this directory of the images.</param>
        /// <param name="prefix">the prefix of the image name.</param>
        public void DrawMovingObjectAccordingToPosition(DrawingContext drawingContext, MovingObject item, string dir, string prefix)
        {
            int r;
            if (item.PositionCounter < 4)
            {
                r = 0;
            }
            else
            {
                r = 1;
            }

            switch (item.Movement)
            {
                case Direction.Left:
                    this.Draw(drawingContext, item.RectForView, @"../../Images/" + dir + "/" + prefix + "Left" + (r + 1) + ".png");
                    break;
                case Direction.Right:
                    this.Draw(drawingContext, item.RectForView, @"../../Images/" + dir + "/" + prefix + "Right" + (r + 1) + ".png");
                    break;
                case Direction.Up:
                    this.Draw(drawingContext, item.RectForView, @"../../Images/" + dir + "/" + prefix + "Up" + (r + 1) + ".png");
                    break;
                case Direction.Down:
                    this.Draw(drawingContext, item.RectForView, @"../../Images/" + dir + "/" + prefix + "Down" + (r + 1) + ".png");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// On render method.
        /// </summary>
        /// <param name="drawingContext">drawing context.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(@"../../Images/furniture/final_base1.png", UriKind.Relative))), null, new Rect(0, 0, this.ActualWidth, (int)((double)this.ActualWidth / 2328 * 1084)));
            drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(@"../../Images/furniture/barr.png", UriKind.Relative))), null, new Rect(0, (int)((double)this.ActualWidth / 2328 * 1084), this.ActualWidth, this.ActualHeight - (int)((double)this.ActualWidth / 2328 * 1084)));

            if (this.GL != null)
            {
                foreach (var e in this.GL.ItemsForView)
                {
                    if (e is MovingObject)
                    {
                        // drawingContext.DrawRectangle(new SolidColorBrush(Color.FromRgb((byte)125, (byte)255, (byte)255)), null, e.GetObjectArea);
                        this.PlaceMovingCharacter(drawingContext, e as MovingObject);
                    }
                    else
                    {
                        // drawingContext.DrawRectangle(new SolidColorBrush(Color.FromRgb((byte)125, (byte)255, (byte)125)), null, e.GetObjectArea);
                        this.PlaceStaticObject(drawingContext, e as StaticObject);
                    }
                }

                this.DrawCharacterFromString(drawingContext, new Rect(0.025 * this.ActualWidth, 0.9 * this.ActualHeight, 0.078 * this.ActualWidth, 0.037 * this.ActualHeight), this.GL.StringRemainingTime);
                this.DrawCharacterFromString(drawingContext, new Rect(0.85 * this.ActualWidth, 0.93 * this.ActualHeight, 0.078 * this.ActualWidth, 0.037 * this.ActualHeight), this.GL.Score.ToString());
                this.Draw(drawingContext, new Rect(0.815 * this.ActualWidth, 0.85 * this.ActualHeight, 0.15 * this.ActualWidth, 0.041 * this.ActualWidth), @"../../Images/MenuItems/score.png");

                if (this.GL.Minigame)
                {
                    this.DrawFoldingMinigame(drawingContext);
                }
                else
                {
                    this.Draw(drawingContext, new Rect(0.3 * this.ActualWidth, (int)((double)this.ActualWidth / 2328 * 1110), this.ActualWidth * 0.4, this.ActualHeight - (int)((double)this.ActualWidth / 2328 * 1150)), @"../../Images/GUI/text.png");
                }

                if (this.GL.OutOfTime)
                {
                    this.Draw(drawingContext, new Rect(0.2 * this.ActualWidth, 0.4 * this.ActualHeight, 0.6 * this.ActualWidth, 0.2 * this.ActualHeight), @"../../Images/GUI/GAMEOVER.png");
                }
                else if (this.GL.Finished)
                {
                    this.Draw(drawingContext, new Rect(0.2 * this.ActualWidth, 0.4 * this.ActualHeight, 0.6 * this.ActualWidth, 0.15 * this.ActualHeight), @"../../Images/GUI/CONGRATULATIONS.png");
                }
            }
        }

        private void Draw(DrawingContext drawingContext, Rect item, string uri)
        {
            drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(uri, UriKind.Relative))), null, item);
        }

        private void PlaceStaticObject(DrawingContext drawingContext, StaticObject e)
        {
            if (e.Type != ObjectType.Wall)
            {
                if (e.Type == ObjectType.Table)
                {
                    this.Draw(drawingContext, e.RectForView, @"../../Images/furniture/Table.png");
                }
                else if (e.Type == ObjectType.SofaFlowerTable)
                {
                    this.Draw(drawingContext, e.RectForView, @"../../Images/furniture/SofaTable.png");
                }
                else if (e.Type == ObjectType.Armchair)
                {
                    this.Draw(drawingContext, e.RectForView, @"../../Images/furniture/Armchair.png");
                }
                else if (e.Type == ObjectType.PileOfClothes)
                {
                    this.Draw(drawingContext, e.RectForView, @"../../Images/furniture/tt_burnedee.png");
                }
            }
        }

        private void DrawCharacterFromString(DrawingContext drawingContext, Rect position, string draw)
        {
            Rect rects = new Rect(position.X, position.Y, 30, 30);
            this.Draw(drawingContext, rects, NumberToImage[draw[0]]);
            for (int i = 1; i < draw.Length; i++)
            {
                rects = new Rect(rects.X + rects.Width, rects.Y, rects.Width, rects.Height);
                this.Draw(drawingContext, rects, NumberToImage[draw[i]]);
            }
        }

        private void DrawFoldingMinigame(DrawingContext drawingContext)
        {
            Rect clothesArea = new Rect(0.38 * this.ActualWidth, (int)((double)this.ActualHeight / 2.5), this.ActualWidth * 0.24, this.ActualWidth * 0.245);
            this.DrawFoldedClothesState(drawingContext, clothesArea);
            Rect sequenceBarArea = new Rect(0.3 * this.ActualWidth, (int)((double)this.ActualWidth / 2328 * 1110), this.ActualWidth * 0.4, this.ActualHeight - (int)((double)this.ActualWidth / 2328 * 1120));

            this.DrawSequenceBar(drawingContext, sequenceBarArea);
            Rect foldednumber = new Rect(0.18 * this.ActualWidth, (int)((double)this.ActualWidth / 2328 * 1150), this.ActualWidth * 0.1, this.ActualHeight - (int)((double)this.ActualWidth / 2328 * 1150));
            this.DrawCharacterFromString(drawingContext, foldednumber, this.GL.StringNumberOFoldedClothes);
        }

        private void DrawFoldedClothesState(DrawingContext drawingContext, Rect drawingArea)
        {
            switch (this.GL.FoldingMiniGame.State)
            {
                case FoldingState.Initial:
                    this.Draw(drawingContext, drawingArea, @"../../Images/FoldingGame/folded0.png");
                    break;
                case FoldingState.OneSleeve:
                    this.Draw(drawingContext, drawingArea, @"../../Images/FoldingGame/folded1.png");
                    break;
                case FoldingState.TwoSleeves:
                    this.Draw(drawingContext, drawingArea, @"../../Images/FoldingGame/folded2.png");
                    break;
                case FoldingState.Folded:
                    this.Draw(drawingContext, drawingArea, @"../../Images/FoldingGame/folded3.png");
                    break;
                default:
                    break;
            }
        }

        private void DrawSequenceBar(DrawingContext drawingContext, Rect drawingArea)
        {
            string suffix = string.Empty;
            Rect rects = new Rect(drawingArea.X, drawingArea.Y, drawingArea.Width / 6, drawingArea.Height);
            for (int i = 0; i < this.GL.FoldingMiniGame.KeySequence.Count(); i++)
            {
                if (i <= this.GL.FoldingMiniGame.Index)
                {
                    suffix = "Green";
                }

                this.Draw(drawingContext, rects, ToArrowUri[this.GL.FoldingMiniGame.KeySequence[i]] + suffix + ".png");
                rects = new Rect(rects.X + rects.Width, rects.Y, rects.Width, rects.Height);
                suffix = string.Empty;
            }
        }
    }
}
