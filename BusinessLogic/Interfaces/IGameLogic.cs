// <copyright file="IGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Repository;

    /// <summary>
    /// IGameLogic interface.
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// Gets or sets iGameModel items.
        /// </summary>
        List<IGameModel> Items { get; set; }

        /// <summary>
        /// Gets items for view.
        /// </summary>
        List<IGameModel> ItemsForView { get; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        ScoreContext ScoreDb { get; set; }

        /// <summary>
        /// Gets or sets maincharacter.
        /// </summary>
        MainCharacter MainCharacter { get; set; }

        /// <summary>
        /// Gets or sets player name.
        /// </summary>
        string PlayerName { get; set; }

        /// <summary>
        /// Gets or sets foldigminigame.
        /// </summary>
        FoldingGame FoldingMiniGame { get; set; }

        /// <summary>
        /// Gets or sets gaemtime.
        /// </summary>
        Stopwatch GameTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game is finished.
        /// </summary>
        bool Finished { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if the player is in minigame.
        /// </summary>
        bool Minigame { get; set; }

        /// <summary>
        /// Gets the final score.
        /// </summary>
        int GetFinalScore { get; }

        /// <summary>
        /// Gets a value indicating whether the player is out of time.
        /// </summary>
        bool OutOfTime { get; }

        /// <summary>
        /// Gets score based on collision and interacting.
        /// </summary>
        int Score { get; }

        /// <summary>
        /// Gets remaining minutes.
        /// </summary>
        int RemainingMinutes { get; }

        /// <summary>
        /// Gets remaining seconds.
        /// </summary>
        int RemainingSeconds { get; }

        /// <summary>
        /// Gets remaining time in string.
        /// </summary>
        string StringRemainingTime { get; }

        /// <summary>
        /// Gets numer of minigames in string.
        /// </summary>
        string StringNumberOFoldedClothes { get; }

        /// <summary>
        /// Calculates the score.
        /// </summary>
        /// <returns>returns the score.</returns>
        int GetScoreHelper();

        /// <summary>
        /// Generates the IGameModel items.
        /// </summary>
        void GenerateGameModels();

        /// <summary>
        /// Navigates or checks if can interract with.
        /// </summary>
        /// <param name="direction">input direction.</param>
        /// <returns>return objecttype.</returns>
        ObjectType NavigateMovementOrInteraction(Direction direction);

        /// <summary>
        /// Check if can interract with.
        /// </summary>
        /// <returns>Returns object if can.</returns>
        ObjectType CheckIfCanInteract();

        /// <summary>
        /// Moves roommates.
        /// </summary>
        void MoveRoommates();

        /// <summary>
        /// Controlles minigame.
        /// </summary>
        /// <param name="keypressed">pressed key.</param>
        void ControllMinigame(Direction keypressed);

        /// <summary>
        /// Returns the type of interacting objects.
        /// </summary>
        /// <param name="interactingObject">interacting object.</param>
        /// <returns>type.</returns>
        ObjectType ReturnTypeOfInteractingObjectInCaseOfInteraction(StaticObject interactingObject);

        /// <summary>
        /// Writes entry to database.
        /// </summary>
        void FinishedTheGame();

        /// <summary>
        /// Checks if there are unfolded clothes left.
        /// </summary>
        /// <returns>returns true if there are clothes left.</returns>
        bool CheckIfUnfoldedClothesLeft();

        /// <summary>
        /// Puts clothes in closet an check if there are piles left.
        /// </summary>
        /// <param name="interactingObject">interacting object.</param>
        void PutClothesInCloset(IGameModel interactingObject);

        /// <summary>
        /// Starts the folding minigame.
        /// </summary>
        /// <param name="interactingObject">the interacting object.</param>
        void StartFoldingGame(IGameModel interactingObject);
    }
}
