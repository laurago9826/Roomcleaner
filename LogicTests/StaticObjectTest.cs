// <copyright file="StaticObjectTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LogicTests
{
    using System.Windows;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Static object test.
    /// </summary>
    internal class StaticObjectTest
    {
        /// <summary>
        /// tök felesleges erre is tesztet irni kek.
        /// </summary>
        [Test]
        public void TestingStaticObjectConstructor()
        {
            Rect rekttest = new Rect(0, 0, 200, 200);
            BusinessLogic.StaticObject statictest = new BusinessLogic.StaticObject(0, 0, 200, 200, BusinessLogic.ObjectType.Bluebed);

            Assert.That(statictest.X_coordinate, Is.EqualTo(0));
            Assert.That(statictest.RectForView, Is.EqualTo(rekttest));
        }
    }
}
