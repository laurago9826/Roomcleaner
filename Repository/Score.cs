// <copyright file="Score.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Repository
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Score class.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [Key]
        public string Nev { get; set; }

        /// <summary>
        /// Gets or sets remaining minutes.
        /// </summary>
        public int RemainingMinutes { get; set; }

        /// <summary>
        /// Gets or sets remaining seconds.
        /// </summary>
        public int RemainingSeconds { get; set; }

        /// <summary>
        /// Gets or sets scorepoint.
        /// </summary>
        public int ScorePoint { get; set; }

        /// <summary>
        /// Gets or sets final score.
        /// </summary>
        public int FinalScore { get; set; }

        /// <summary>
        /// Gets remaining time.
        /// </summary>
        public string RemainingTime
        {
            get
            {
                return this.RemainingMinutes + ":" + this.RemainingSeconds;
            }
        }
    }
}
