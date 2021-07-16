// <copyright file="ScoreContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Repository
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// Score context for database.
    /// </summary>
    public class ScoreContext : DbContext
    {
        /// <summary>
        /// Gets or sets current player name.
        /// </summary>
        public static string Name { get; set; }

        /// <summary>
        /// Gets or sets highscores.
        /// </summary>
        public DbSet<Score> HighScores { get; set; }

        /// <summary>
        /// Gets high scores.
        /// </summary>
        public List<Score> GetHighscores
        {
            get { return this.HighScores.OrderByDescending(x => x.FinalScore).ToList(); }
        }

        /// <summary>
        /// Adds a new entry to database.
        /// </summary>
        /// <param name="name">player name.</param>
        /// <param name="remainingMinnutes">remaining minutes.</param>
        /// <param name="remainingSeconds">remaining seconds.</param>
        /// <param name="scorePoint">score.</param>
        /// <param name="finalScore">final score.</param>
        public void AddNewEntry(string name, int remainingMinnutes, int remainingSeconds, int scorePoint, int finalScore)
        {
            Name = name;
            var currentPlayer = new Score() { Nev = name, RemainingMinutes = remainingMinnutes, RemainingSeconds = remainingSeconds, ScorePoint = scorePoint, FinalScore = finalScore };
            this.HighScores.Add(currentPlayer);
            this.SaveChanges();
        }

        /// <summary>
        /// fix entity framework.
        /// </summary>
        public void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded.
            // Make sure the provider assembly is available to the running application.
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
