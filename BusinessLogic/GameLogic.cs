// <copyright file="GameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using Repository;

    /// <summary>
    /// GameLogic: Business Logic of the game.
    /// </summary>
    public class GameLogic : IGameLogic
    {
        /// <summary>
        /// duration of game in minutes.
        /// </summary>
        private const int Durationofgame = 3;
        private const int DresserScore = 1000;
        private const int FoldingScore = 2500;
        private const int MinusScoreRoommate = 40;
        private const int MinusScoreWrongArrow = 400;
        private readonly double baseImageWidth;
        private readonly double baseImageHeight;
        private int score = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="window_width">window width.</param>
        /// <param name="window_heigth">window height.</param>
        /// <param name="username">username.</param>
        /// <param name="Repo">repository.</param>
        public GameLogic(double window_width, double window_heigth, string username)
        {
            this.ScoreDb = new ScoreContext();
            this.FoldingMiniGame = FoldingGame.Instance;
            this.GameTime = new Stopwatch();
            this.GameTime.Start();
            this.PlayerName = username;
            this.baseImageWidth = window_width;
            this.baseImageHeight = window_width * 1084 / 2328;
            this.Items = new List<IGameModel>();

            this.GenerateGameModels();
        }

        /// <summary>
        /// Gets or sets iGameModel items.
        /// </summary>
        public List<IGameModel> Items { get; set; }

        /// <summary>
        /// Gets items for view.
        /// </summary>
        public List<IGameModel> ItemsForView
        {
            get { return this.Items.OrderBy(x => x.Y_coordinate).ToList(); }
        }

        /// <summary>
        /// Gets or sets the database where highscores are stored.
        /// </summary>
        public ScoreContext ScoreDb { get; set; }

        /// <summary>
        /// Gets or sets maincharacter.
        /// </summary>
        public MainCharacter MainCharacter { get; set; }

        /// <summary>
        /// Gets or sets player name.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Gets or sets foldigminigame.
        /// </summary>
        public FoldingGame FoldingMiniGame { get; set; }

        /// <summary>
        /// Gets or sets gaemtime.
        /// </summary>
        public Stopwatch GameTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player finished the game.
        /// </summary>
        public bool Finished { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the window can be closed.
        /// </summary>
        public bool FinishedCanClose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player finished the game.
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if the player is in minigame.
        /// </summary>
        public bool Minigame { get; set; }

        /// <summary>
        /// Gets the score calculated from the remaining time and regular score.
        /// </summary>
        public int GetFinalScore
        {
            get
            {
                return (int)(((this.RemainingMinutes * 6000) + (this.RemainingSeconds * 100)) + this.Score);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is out of time.
        /// </summary>
        public bool OutOfTime
        {
            get
            {
                if (this.RemainingMinutes == 0 && this.RemainingSeconds == 0)
                {
                    Thread t = new Thread(() =>
                    {
                        Thread.Sleep(2000);
                        this.GameOver = true;
                    });
                    t.Start();
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets score based on collision and interacting.
        /// </summary>
        public int Score
        {
            get
            {
                return this.GetScoreHelper();
            }
        }

        /// <summary>
        /// Gets remaining minutes.
        /// </summary>
        public int RemainingMinutes
        {
            get
            {
                if ((Durationofgame - 1 - this.GameTime.Elapsed.Minutes) <= 0)
                {
                    return 0;
                }

                return Durationofgame - 1 - this.GameTime.Elapsed.Minutes;
            }
        }

        /// <summary>
        /// Gets remaining seconds.
        /// </summary>
        public int RemainingSeconds
        {
            get
            {
                if (this.RemainingMinutes <= 0 && (59 - this.GameTime.Elapsed.Seconds) <= 0)
                {
                    this.GameTime.Stop();
                    return 0;
                }

                return 59 - this.GameTime.Elapsed.Seconds;
            }
        }

        /// <summary>
        /// Gets remaining time in string.
        /// </summary>
        public string StringRemainingTime
        {
            get
            {
                string min = this.RemainingMinutes.ToString();
                string sec = this.RemainingSeconds.ToString();
                if (min.Length == 1)
                {
                    min = "0" + min;
                }

                if (sec.Length == 1)
                {
                    sec = "0" + sec;
                }

                return min + ":" + sec;
            }
        }

        /// <summary>
        /// Gets numer of minigames in string.
        /// </summary>
        public string StringNumberOFoldedClothes
        {
            get { return FoldingGame.NumberOfFinishedMiniGames + "/" + FoldingGame.NumberOfShirtsInOnePile; }
        }

        /// <summary>
        /// Decrements the score.
        /// </summary>
        /// <returns>returns the score.</returns>
        public int GetScoreHelper()
        {
            int possibleScore = this.score - (this.MainCharacter.NumberOfNewCollisionsWithRoommate * MinusScoreRoommate) - (this.FoldingMiniGame.NumberOfFailedFoldings * MinusScoreWrongArrow);
            if (possibleScore < 0)
            {
                this.MainCharacter.NumberOfNewCollisionsWithRoommate = 0;
                this.FoldingMiniGame.NumberOfFailedFoldings = 0;
                this.score = 0;
                return 0;
            }

            this.MainCharacter.NumberOfNewCollisionsWithRoommate = 0;
            this.FoldingMiniGame.NumberOfFailedFoldings = 0;
            this.score = possibleScore;
            return this.score;
        }

        /// <summary>
        /// Generates the field.
        /// </summary>
        public void GenerateGameModels()
        {
            this.Items.Add(new StaticObject(0, 0, (int)this.baseImageWidth, (int)(this.baseImageHeight * 317 / 1084), ObjectType.Wall));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 1305 / 2321), (int)(this.baseImageHeight * 706 / 1084), (int)(this.baseImageWidth * 202 / 2321), (int)(this.baseImageHeight * 90 / 1084), ObjectType.Armchair)
            { RectForView = new Rect((int)(this.baseImageWidth * 1305 / 2321), (int)(this.baseImageHeight * 597 / 1084), (int)(this.baseImageWidth * 202 / 2321), (int)(this.baseImageHeight * 205 / 1084)) });
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 1955 / 2321), (int)(this.baseImageHeight * 706 / 1084), (int)(this.baseImageWidth * 202 / 2321), (int)(this.baseImageHeight * 90 / 1084), ObjectType.Armchair)
            { RectForView = new Rect((int)(this.baseImageWidth * 1955 / 2321), (int)(this.baseImageHeight * 597 / 1084), (int)(this.baseImageWidth * 202 / 2321), (int)(this.baseImageHeight * 205 / 1084)) });
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 551 / 2321), (int)(this.baseImageHeight * 235 / 1084), (int)(this.baseImageWidth * 329 / 2321), (int)(this.baseImageHeight * 386 / 1084), ObjectType.Bluebed));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 25 / 2321), (int)(this.baseImageHeight * 235 / 1084), (int)(this.baseImageWidth * 338 / 2321), (int)(this.baseImageHeight * 386 / 1084), ObjectType.Rosebed));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 411 / 2321), (int)(this.baseImageHeight * 163 / 1084), (int)(this.baseImageWidth * 102 / 2321), (int)(this.baseImageHeight * 277 / 1084), ObjectType.Lamp));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 976 / 2321), (int)(this.baseImageHeight * 179 / 1084), (int)(this.baseImageWidth * 124 / 2321), (int)(this.baseImageHeight * 200 / 1084), ObjectType.Redflower));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 1177 / 2321), (int)(this.baseImageHeight * 41 / 1084), (int)(this.baseImageWidth * 332 / 2321), (int)(this.baseImageHeight * 385 / 1084), ObjectType.ChinaCabinet));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 1593 / 2321), (int)(this.baseImageHeight * 129 / 1084), (int)(this.baseImageWidth * 301 / 2321), (int)(this.baseImageHeight * 272 / 1084), ObjectType.Dresser));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 1989 / 2321), (int)(this.baseImageHeight * 212 / 1084), (int)(this.baseImageWidth * 101 / 2321), (int)(this.baseImageHeight * 200 / 1084), ObjectType.Flowers));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 2174 / 2321), (int)(this.baseImageHeight * 212 / 1084), (int)(this.baseImageWidth * 97 / 2321), (int)(this.baseImageHeight * 200 / 1084), ObjectType.Flowers));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 397 / 2321), (int)(this.baseImageHeight * 780 / 1084), (int)(this.baseImageWidth * 176 / 2321), (int)(this.baseImageHeight * 130 / 1084), ObjectType.Table)
            { RectForView = new Rect((int)(this.baseImageWidth * 397 / 2321), (int)(this.baseImageHeight * 576 / 1084), (int)(this.baseImageWidth * 176 / 2321), (int)(this.baseImageHeight * 335 / 1084)) });
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 1576 / 2321), (int)(this.baseImageHeight * 650 / 1084), (int)(this.baseImageWidth * 281 / 2321), (int)(this.baseImageHeight * 290 / 1084), ObjectType.SofaFlowerTable)
            { RectForView = new Rect((int)(this.baseImageWidth * 1576 / 2321), (int)(this.baseImageHeight * 519 / 1084), (int)(this.baseImageWidth * 281 / 2321), (int)(this.baseImageHeight * 423 / 1084)) });

            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 47 / 800), (int)(this.baseImageHeight * 305 / 365), (int)(this.baseImageWidth * 41 / 800), (int)(this.baseImageHeight * 38 / 365), ObjectType.PileOfClothes));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 8 / 800), (int)(this.baseImageHeight * 219 / 365), (int)(this.baseImageWidth * 41 / 800), (int)(this.baseImageHeight * 38 / 365), ObjectType.PileOfClothes));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 137 / 800), (int)(this.baseImageHeight * 157 / 365), (int)(this.baseImageWidth * 41 / 800), (int)(this.baseImageHeight * 38 / 365), ObjectType.PileOfClothes));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 263 / 800), (int)(this.baseImageHeight * 207 / 365), (int)(this.baseImageWidth * 41 / 800), (int)(this.baseImageHeight * 38 / 365), ObjectType.PileOfClothes));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 315 / 800), (int)(this.baseImageHeight * 138 / 365), (int)(this.baseImageWidth * 41 / 800), (int)(this.baseImageHeight * 38 / 365), ObjectType.PileOfClothes));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 720 / 800), (int)(this.baseImageHeight * 138 / 365), (int)(this.baseImageWidth * 41 / 800), (int)(this.baseImageHeight * 38 / 365), ObjectType.PileOfClothes));
            this.Items.Add(new StaticObject((int)(this.baseImageWidth * 745 / 800), (int)(this.baseImageHeight * 319 / 365), (int)(this.baseImageWidth * 41 / 800), (int)(this.baseImageHeight * 38 / 365), ObjectType.PileOfClothes));
            this.MainCharacter = new MainCharacter((int)(this.baseImageWidth / 2), (int)(this.baseImageHeight * 3 / 4), (int)(this.baseImageHeight / 6), (int)(this.baseImageWidth / 20.3), this.baseImageWidth, this.baseImageHeight);
            this.Items.Add(this.MainCharacter);

            // this.Items.Add(new Roommate((int)(this.baseImageWidth / 1.77), (int)(this.baseImageHeight * 0.76), (int)(this.baseImageHeight / 6), (int)(this.baseImageWidth / 20.3), this.baseImageWidth, this.baseImageHeight));
            this.Items.Add(new Roommate((int)(this.baseImageWidth / 2.4), (int)(this.baseImageHeight * 0.76), (int)(this.baseImageHeight / 6), (int)(this.baseImageWidth / 20.3), this.baseImageWidth, this.baseImageHeight));
            this.Items.Add(new Roommate((int)(this.baseImageWidth * 0.1), (int)(this.baseImageHeight * 0.76), (int)(this.baseImageHeight / 6), (int)(this.baseImageWidth / 20.3), this.baseImageWidth, this.baseImageHeight));
            this.Items.Add(new Roommate((int)(this.baseImageWidth * 0.7), (int)(this.baseImageHeight * 0.3), (int)(this.baseImageHeight / 6), (int)(this.baseImageWidth / 20.3), this.baseImageWidth, this.baseImageHeight));

            // this.Items.Add(new Roommate((int)(this.baseImageWidth / 14.17), (int)(this.baseImageHeight / 1.65), (int)(this.baseImageHeight / 6), (int)(this.baseImageWidth / 20.3), this.baseImageWidth, this.baseImageHeight));
            this.Items.Add(new Roommate((int)(this.baseImageWidth / 7.1), (int)(this.baseImageHeight / 1.1), (int)(this.baseImageHeight / 6), (int)(this.baseImageWidth / 20.3), this.baseImageWidth, this.baseImageHeight));
            this.Items.Add(new Roommate((int)(this.baseImageWidth / 2), (int)(this.baseImageHeight / 1.8), (int)(this.baseImageHeight / 6), (int)(this.baseImageWidth / 20.3), this.baseImageWidth, this.baseImageHeight));
            this.Items.Add(new Roommate((int)(this.baseImageWidth / 1.23), (int)(this.baseImageHeight / 1.8), (int)(this.baseImageHeight / 6), (int)(this.baseImageWidth / 20.3), this.baseImageWidth, this.baseImageHeight));
        }

        /// <summary>
        /// Navigates or checks if can interract with.
        /// </summary>
        /// <param name="direction">input direction.</param>
        /// <returns>return objecttype.</returns>
        public ObjectType NavigateMovementOrInteraction(Direction direction)
        {
            if (direction != Direction.None && !this.Minigame && !this.Finished && !this.OutOfTime)
            {
                this.MainCharacter.MoveDirection(direction, this.Items);
            }
            else
            {
                return this.CheckIfCanInteract(); // valamit vissza kell adnia, hogy tudjon jelezni a code behind-nak
            }

            return ObjectType.None;
        }

        /// <summary>
        /// Check if can interract with.
        /// </summary>
        /// <returns>Returns object if can.</returns>
        public ObjectType CheckIfCanInteract()
        {
            StaticObject interactingObject = this.MainCharacter.ReturnInteractingObjectsWhichCollided(this.Items);
            if (interactingObject != null)
            {
                return this.ReturnTypeOfInteractingObjectInCaseOfInteraction(interactingObject);
            }

            return ObjectType.None;
        }

        /// <summary>
        /// Moves roommates.
        /// </summary>
        public void MoveRoommates()
        {
            if (!this.Minigame && !this.Finished && !this.OutOfTime)
            {
                foreach (var item in this.Items)
                {
                    if (item is Roommate)
                    {
                        (item as Roommate).MoveDirectionIfCollided(this.Items);
                    }
                }
            }
        }

        /// <summary>
        /// Controlles minigame.
        /// </summary>
        /// <param name="keypressed">pressed key.</param>
        public void ControllMinigame(Direction keypressed)
        {
            this.Minigame = this.FoldingMiniGame.CheckIfKeyIsCorrect(keypressed) && !this.FoldingMiniGame.AllFolded;
        }

        /// <summary>
        /// Checks if the main character interacts with a closet or with pile of clothes and start the minigame.
        /// </summary>
        /// <param name="interactingObject">the interacting object.</param>
        /// <returns>returns the txpe of interacting object.</returns>
        public ObjectType ReturnTypeOfInteractingObjectInCaseOfInteraction(StaticObject interactingObject)
        {
            if (this.MainCharacter.CollidesWithOtherItemFromWhichDirection(new List<IGameModel>() { interactingObject }) == this.MainCharacter.Movement)
            {
                if (!this.MainCharacter.IsHoldingFoldedClothes && interactingObject.Type == ObjectType.PileOfClothes)
                {
                    this.StartFoldingGame(interactingObject);
                    return ObjectType.PileOfClothes;
                }
                else if (interactingObject.Type == ObjectType.Dresser)
                {
                    if (this.MainCharacter.IsHoldingFoldedClothes)
                    {
                        this.PutClothesInCloset(interactingObject);
                    }

                    return ObjectType.Dresser;
                }
            }

            return ObjectType.None;
        }

        /// <summary>
        /// Puts clothes in closet an check if there are piles left.
        /// </summary>
        /// <param name="interactingObject">interacting object.</param>
        public void PutClothesInCloset(IGameModel interactingObject)
        {
            this.MainCharacter.IsHoldingFoldedClothes = false;
            this.score += DresserScore;
            if (!this.CheckIfUnfoldedClothesLeft())
            {
                this.FinishedTheGame();
            }
        }

        /// <summary>
        /// Starts the folding minigame.
        /// </summary>
        /// <param name="interactingObject">the interacting object.</param>
        public void StartFoldingGame(IGameModel interactingObject)
        {
            this.Minigame = true;
            this.FoldingMiniGame = FoldingGame.Instance;
            this.Items.Remove(interactingObject);
            this.score += FoldingScore;
            this.MainCharacter.IsHoldingFoldedClothes = true;
            FoldingGame.NumberOfFinishedMiniGames = 0;
        }

        /// <summary>
        /// If the player finishes the game it starts a new thread which writes to the database, because the main thread would be blocked otherwise and the GUI will freeze for a few seconds.
        /// </summary>
        public void FinishedTheGame()
        {
            Thread t = new Thread(() =>
            {
                this.ScoreDb.AddNewEntry(this.PlayerName, this.RemainingMinutes, this.RemainingSeconds, this.Score, this.GetFinalScore);
                Thread.Sleep(1000);
                this.FinishedCanClose = true;
            });
            t.Start();
            this.Finished = true;
        }

        /// <summary>
        /// Checks if there are unfolded clothes left.
        /// </summary>
        /// <returns>returns false if there are no piles of clothes left.</returns>
        public bool CheckIfUnfoldedClothesLeft()
        {
            if (this.Items.Where(x => x is StaticObject).Count(x => (x as StaticObject).Type == ObjectType.PileOfClothes) == 0 && this.FoldingMiniGame.AllFolded)
            {
                this.GameTime.Stop();
                return false;
            }

            return true;
        }
    }
}
