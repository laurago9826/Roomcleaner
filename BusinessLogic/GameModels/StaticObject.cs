// <copyright file="StaticObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System.Windows;

    /// <summary>
    /// Objecttype enum.
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// Armchair
        /// </summary>
        Armchair,

        /// <summary>
        /// blue bed
        /// </summary>
        Bluebed,

        /// <summary>
        /// bookcase
        /// </summary>
        Bookcase,

        /// <summary>
        /// cabinet
        /// </summary>
        ChinaCabinet,

        /// <summary>
        /// dersser
        /// </summary>
        Dresser,

        /// <summary>
        /// flowers
        /// </summary>
        Flowers,

        /// <summary>
        /// lamp
        /// </summary>
        Lamp,

        /// <summary>
        /// red flower
        /// </summary>
        Redflower,

        /// <summary>
        /// rose bed
        /// </summary>
        Rosebed,

        /// <summary>
        /// sofa with flower and table
        /// </summary>
        SofaFlowerTable,

        /// <summary>
        /// table
        /// </summary>
        Table,

        /// <summary>
        /// wall
        /// </summary>
        Wall,

        /// <summary>
        /// pile of clothes
        /// </summary>
        PileOfClothes,

        /// <summary>
        /// Arrow
        /// </summary>
        Arrow,

        /// <summary>
        /// cloth
        /// </summary>
        Cloth,

        /// <summary>
        /// none
        /// </summary>
        None,
    }

    /// <summary>
    /// Static object class, inherated from IGameModel.
    /// </summary>
    public class StaticObject : IGameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticObject"/> class.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="width">object width.</param>
        /// <param name="height">object height.</param>
        /// <param name="type">objetc type.</param>
        public StaticObject(int x, int y, int width, int height, ObjectType type)
        {
            this.X_coordinate = x;
            this.Y_coordinate = y;
            this.Width = width;
            this.Height = height;
            this.Type = type;
            this.RectForView = this.GetObjectArea;
        }

        /// <summary>
        /// Gets or sets x coordinate.
        /// </summary>
        public int X_coordinate { get; set; }

        /// <summary>
        /// Gets or sets y coordinate.
        /// </summary>
        public int Y_coordinate { get; set; }

        /// <summary>
        /// Gets or sets height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets object type.
        /// </summary>
        public ObjectType Type { get; set; }

        /// <summary>
        /// Gets object area.
        /// </summary>
        public Rect GetObjectArea
        {
            get { return new Rect(this.X_coordinate, this.Y_coordinate, this.Width, this.Height); }
        }

        /// <summary>
        /// Gets or sets rect for view.
        /// </summary>
        public Rect RectForView { get; set; }
    }
}
