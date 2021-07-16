// <copyright file="MainCharacterTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LogicTests
{
    using System.Collections.Generic;
    using BusinessLogic;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// MainCharacter tests.
    /// </summary>
    [TestFixture]
    public class MainCharacterTest
    {
        /// <summary>
        /// test for ming left.
        /// </summary>
        [Test]
        public void MovingDirectionMoveLeftTest()
        {
            List<IGameModel> lista = new List<IGameModel>
            {
                new StaticObject(0, 0, 10, 10, ObjectType.Wall),
            };
            MainCharacter testchar = new MainCharacter(10, 0, 10, 10, 1920, 1080);

            testchar.MoveDirection(Direction.Left, lista);
            Assert.AreEqual(testchar.TryMoveLeft(), true);
        }

        /// <summary>
        /// test for ming right.
        /// </summary>
        [Test]
        public void MovingDirectionMoveRightTest()
        {
            List<IGameModel> lista = new List<IGameModel>
            {
                new StaticObject(10, 0, 10, 10, ObjectType.Wall),
            };
            MainCharacter testchar = new MainCharacter(0, 0, 10, 10, 1920, 1080);

            testchar.MoveDirection(Direction.Left, lista);
            Assert.AreEqual(testchar.TryMoveRight(), true);
        }

        /// <summary>
        /// test for ming up.
        /// </summary>
        [Test]
        public void MovingDirectionMoveUpTest()
        {
            List<IGameModel> lista = new List<IGameModel>
            {
                new StaticObject(0, 0, 10, 10, ObjectType.Wall),
            };
            MainCharacter testchar = new MainCharacter(0, 10, 10, 10, 1920, 1080);

            testchar.MoveDirection(Direction.Left, lista);
            Assert.AreEqual(testchar.TryMoveUp(), true);
        }

        /// <summary>
        /// test for ming down.
        /// </summary>
        [Test]
        public void MovingDirectionMoveDownTest()
        {
            List<IGameModel> lista = new List<IGameModel>
            {
                new StaticObject(0, 10, 10, 10, ObjectType.Wall),
            };
            MainCharacter testchar = new MainCharacter(0, 0, 10, 10, 1920, 1080);

            testchar.MoveDirection(Direction.Left, lista);
            Assert.AreEqual(testchar.TryMoveDown(), true);
        }

        /// <summary>
        /// Testign collision direction.
        /// </summary>
        [Test]
        public void TestForCollideingFromDirection()
        {
            List<IGameModel> lista = new List<IGameModel>
            {
                new StaticObject(10, 10, 10, 10, ObjectType.Wall),
            };
            MainCharacter testchar = new MainCharacter(0, 0, 10, 10, 1920, 1080);

            Assert.AreEqual(testchar.CollidesWithOtherItemFromWhichDirection(lista), Direction.None);
        }
    }
}
