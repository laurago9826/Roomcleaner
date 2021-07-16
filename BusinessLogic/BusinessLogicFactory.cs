// <copyright file="BusinessLogicFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    /// <summary>
    /// Bisiness logic factory class.
    /// </summary>
    public class BusinessLogicFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicFactory"/> class.
        /// </summary>
        /// <param name="width">window width.</param>
        /// <param name="height">window height.</param>
        /// <param name="uname">username.</param>
        public BusinessLogicFactory(double width, double height, string uname)
        {
            this.GL = new GameLogic(width, height, uname);
        }

        /// <summary>
        /// Gets or sets gamelogic.
        /// </summary>
        public GameLogic GL { get; set; }
    }
}
