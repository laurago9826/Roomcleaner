// <copyright file="IGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// IGameModel interface.
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Gets or sets x coordinate.
        /// </summary>
        int X_coordinate { get; set; }

        /// <summary>
        /// Gets or sets y coordinate.
        /// </summary>
        int Y_coordinate { get; set; }

        /// <summary>
        /// Gets or sets object height.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Gets or sets object width.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Gets object area.
        /// </summary>
        Rect GetObjectArea { get; }

        /// <summary>
        /// Gets rect for view.
        /// </summary>
        Rect RectForView { get; }
    }
}
