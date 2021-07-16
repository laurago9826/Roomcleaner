// <copyright file="FoldingGame.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// State of folding shirt.
    /// </summary>
    public enum FoldingState
    {
        /// <summary>
        /// base image
        /// </summary>
        Initial,

        /// <summary>
        /// one sleeve folded
        /// </summary>
        OneSleeve,

        /// <summary>
        /// two sleves folded
        /// </summary>
        TwoSleeves,

        /// <summary>
        /// fully folded
        /// </summary>
        Folded,
    }

    /// <summary>
    /// Foldig game class.
    /// </summary>
    public class FoldingGame
    {
        private static Random r = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="FoldingGame"/> class.
        /// </summary>
        private FoldingGame()
        {
            NumberOfShirtsInOnePile = 3;
            this.GenerateSequance();
        }

        /// <summary>
        /// Gets a new folding game for every interraction.
        /// </summary>
        public static FoldingGame Instance
        {
            get { return new FoldingGame(); }
        }

        /// <summary>
        /// Gets or sets number of times the sequence is generated for folding.
        /// </summary>
        public static int NumberOfShirtsInOnePile { get; set; }

        /// <summary>
        /// Gets or sets number of finished folding games.
        /// </summary>
        public static int NumberOfFinishedMiniGames { get; set; }

        /// <summary>
        /// Gets or sets the number of foldings failed.
        /// </summary>
        public int NumberOfFailedFoldings { get; set; }

        /// <summary>
        /// Gets a value indicating whether the number of foldings is done.
        /// </summary>
        public bool AllFolded
        {
            get { return NumberOfFinishedMiniGames == NumberOfShirtsInOnePile; }
        }

        /// <summary>
        /// Gets a value indicating whether the folding is finished.
        /// </summary>
        public bool FinishedFolding
        {
            get { return this.KeySequence.Count - 1 == this.Index; }
        }

        /// <summary>
        /// Gets or sets keysequence.
        /// </summary>
        public List<Direction> KeySequence { get; set; }

        /// <summary>
        /// Gets or sets state of folded shirt shown.
        /// </summary>
        public FoldingState State { get; set; }

        /// <summary>
        /// Gets or sets the pressed keys index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Chesk if the pressed key is correct.
        /// </summary>
        /// <param name="key">pressed key.</param>
        /// <returns>true if the pressed key is equal to the indexed key in the sequence.</returns>
        public bool CheckIfKeyIsCorrect(Direction key)
        {
            if (this.Index < 5)
            {
                if (key == this.KeySequence[this.Index + 1])
                {
                    this.Index++;
                }
                else
                {
                    this.NumberOfFailedFoldings++;
                    this.GenerateSequance();
                }

                if (this.Index < 1)
                {
                    this.State = FoldingState.Initial;
                }
                else if (this.Index < 3)
                {
                    this.State = FoldingState.OneSleeve;
                }
                else if (this.Index < 5)
                {
                    this.State = FoldingState.TwoSleeves;
                }
                else if (this.Index < 6)
                {
                    this.State = FoldingState.Folded;
                    NumberOfFinishedMiniGames++;
                    if (NumberOfFinishedMiniGames != NumberOfShirtsInOnePile)
                    {
                        Thread t = new Thread(() =>
                        {
                            Thread.Sleep(500);
                            this.GenerateSequance();
                        });
                        t.Start();
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void GenerateSequance()
        {
            this.Index = -1;
            this.State = FoldingState.Initial;
            this.KeySequence = new List<Direction>();
            r = new Random();
            for (int i = 0; i < 6; i++)
            {
                this.KeySequence.Add(MovingObject.Directions[r.Next(0, 4)]);
            }
        }
    }
}
