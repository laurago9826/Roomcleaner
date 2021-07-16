// <copyright file="ScoreViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GUI.ViewModels
{
    using System.Collections.ObjectModel;
    using GUI.Helpers;
    using Repository;

    /// <summary>
    /// ScoreViewModel.
    /// </summary>
    public class ScoreViewModel : Bindable
    {
        private readonly ScoreContext sc;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreViewModel"/> class.
        /// </summary>
        public ScoreViewModel()
        {
            this.sc = new ScoreContext();
        }

        /// <summary>
        /// Gets the highscores from the database.
        /// </summary>
        public ObservableCollection<Score> Highscores
        {
            get
            {
                ObservableCollection<Score> scores = new ObservableCollection<Score>();
                foreach (var h in this.sc.GetHighscores)
                {
                    scores.Add(h);
                }

                return scores;
            }
        }
    }
}
