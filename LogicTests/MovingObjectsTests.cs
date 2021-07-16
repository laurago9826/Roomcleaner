// <copyright file="MovingObjectsTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LogicTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using BusinessLogic;
    using NUnit.Framework;

    /// <summary>
    /// Tests for moving objects.
    /// </summary>
    [TestFixture]
    public class MovingObjectsTests
    {
        private MovingObject movingObject = new MainCharacter(0, 0, 10, 10, 10, 10);

        /// <summary>
        /// Test for collission with left wall.
        /// </summary>
        [Test]
        public void CollidesWithLeftWall()
        {
            Assert.That(this.movingObject.CollidesWithLeftWall(), Is.EqualTo(true));
        }

        /// <summary>
        /// Test for collission with right wall.
        /// </summary>
        [Test]
        public void CollidesWithRightWall()
        {
            Assert.That(this.movingObject.CollidesWithRightWall(), Is.EqualTo(true));
        }

        /// <summary>
        /// Test for collission with top wall.
        /// </summary>
        [Test]
        public void CollidesWithTopWall()
        {
            Assert.That(this.movingObject.CollidesWithTopWall(), Is.EqualTo(true));
        }

        /// <summary>
        /// Test for collission with bottom wall.
        /// </summary>
        [Test]
        public void CollidesWithBottomWall()
        {
            Assert.That(this.movingObject.CollidesWithBottomWall(), Is.EqualTo(true));
        }

        /// <summary>
        /// Test GetMaximumArea.
        /// </summary>
        /// <param name="rects">array.</param>
        /// <param name="result">the maximum area.</param>
        [TestCase(new double[] { 1, 2, 3, 4 }, 3)]
        [TestCase(new double[] { 54, 95, 1, 58 }, 1)]
        [TestCase(new double[] { 23, 12, 54.4, 12.5 }, 2)]
        public void GetMaximumArea(double [] rects, int result)
        {
            Assert.That(this.movingObject.GetMaximumArea(rects), Is.EqualTo(result));
        }

        /// <summary>
        /// Test for left intersection.
        /// </summary>
        [Test]
        public void LeftIntersectionTest()
        {
            Rect rect = new Rect(0, 0, 10, 10);
            Rect rect2 = new Rect(10, 5, 10, 10);
            Assert.That(this.movingObject.LeftIntersectionArea(rect2, rect), Is.EqualTo(50));
        }

        /// <summary>
        /// Test for right intersection.
        /// </summary>
        [Test]
        public void RightIntersectionTest()
        {
            Rect rect = new Rect(0, 0, 10, 10);
            Rect rect2 = new Rect(10, 5, 10, 10);
            Assert.That(this.movingObject.RightIntersectionArea(rect, rect2), Is.EqualTo(50));
        }

        /// <summary>
        /// Test for top intersection.
        /// </summary>
        [Test]
        public void TopIntersectionTest()
        {
            Rect rect = new Rect(0, 10, 10, 10);
            Rect rect2 = new Rect(5, 0, 10, 10);
            Assert.That(this.movingObject.AboveIntersectionArea(rect, rect2), Is.EqualTo(50));
        }

        /// <summary>
        /// Test for bottom intersection.
        /// </summary>
        [Test]
        public void BottomIntersectionTest()
        {
            Rect rect = new Rect(0, 10, 10, 10);
            Rect rect2 = new Rect(5, 0, 10, 10);
            Assert.That(this.movingObject.BelowIntersectionArea(rect2, rect), Is.EqualTo(50));
        }

        /// <summary>
        /// Determine opposite direction method.
        /// </summary>
        /// <param name="dir1">one direction.</param>
        /// <param name="dir2">opposite direction.</param>
        [TestCase(Direction.Left, Direction.Right)]
        [TestCase(Direction.Right, Direction.Left)]
        [TestCase(Direction.Up, Direction.Down)]
        [TestCase(Direction.Down, Direction.Up)]
        public void DeterminaOppositeDirectionTest(Direction dir1, Direction dir2)
        {
            Assert.That(this.movingObject.DetermineOppositeDirection(dir1), Is.EqualTo(dir2));
        }

        /// <summary>
        /// Testing the movement.
        /// </summary>
        [Test]
        public void TryMoveTest()
        {
            MovingObject m = new MainCharacter(0, 0, 10, 10, 10, 10);
            Assert.That(m.TryMoveDown(), Is.EqualTo(false));
            Assert.That(m.TryMoveUp(), Is.EqualTo(false));
            Assert.That(m.TryMoveLeft(), Is.EqualTo(false));
            Assert.That(m.TryMoveRight(), Is.EqualTo(false));
        }

        /// <summary>
        /// Testing the movement.
        /// </summary>
        [Test]
        public void TryMoveTest_2()
        {
            MovingObject m = new MainCharacter(7, 7, 10, 10, 24, 24);
            Assert.That(m.TryMoveDown(), Is.EqualTo(true));
            Assert.That(m.TryMoveUp(), Is.EqualTo(true));
            Assert.That(m.TryMoveLeft(), Is.EqualTo(true));
            Assert.That(m.TryMoveRight(), Is.EqualTo(true));
        }
    }
}
