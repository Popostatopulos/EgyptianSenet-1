using NUnit.Framework;

namespace Game.GameLogic.Tests
{
    [TestFixture]
    public class TestsStep
    {
        [Test]
        public void FillingMap_Should()
        {
            var game = new Game();
            var map = game.Map;
            for (var i = 1; i <= 14; i++)
            {
                if (i % 2 > 0)
                    Assert.IsTrue(map[i].State.Type == ChipsType.Cone);
                else
                    Assert.IsTrue(map[i].State.Type == ChipsType.Coil);
            }

            for (var i = 15; i < map.Length; i++)
            {
                Assert.AreEqual(map[i].State, null);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]

        public void Mini_MakeStep_Should(int stepCount)
        {
            var map = new Cell[7];
            var coil = new Figure(1, 1, ChipsType.Coil);
            map[1] = new Cell(coil);
            for (var i = 2; i < map.Length; i++)
                map[i] = new Cell(null);
            Game.MakeStep(stepCount, map, map[1].State);
            Assert.AreEqual(coil, map[1 + stepCount].State);
            Assert.IsNull(map[1].State);
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 4)]
        [TestCase(4, 5)]
        [TestCase(5, 6)]

        public void Mini_MakeStepWithCut_Should(int stepCount, int cutLocation)
        {
            var map = new Cell[7];
            var cone = new Figure(cutLocation, 1, ChipsType.Cone);
            var coil = new Figure(1, 1, ChipsType.Coil);
            map[1] = new Cell(coil);
            for (var i = 2; i < map.Length; i++)
            {
                if (i == cutLocation)
                    map[i] = new Cell(cone);
                else
                    map[i] = new Cell(null);
            }
            Game.MakeStep(stepCount, map, map[1].State);
            Assert.AreEqual(map[1].State, cone);
            Assert.AreEqual(map[cutLocation].State, coil);
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 4)]
        [TestCase(4, 5)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public void Mini_MakeStepWithNoCut_Should(int stepCount, int cutLocation)
        {
            var map = new Cell[7];
            var cone = new Figure(cutLocation, 1, ChipsType.Cone);
            var guard = new Figure(cutLocation + 1, 2, ChipsType.Cone);
            var coil = new Figure(1, 1, ChipsType.Coil);
            map[1] = new Cell(coil);
            for (var i = 2; i < map.Length; i++)
            {
                if (i == cutLocation)
                    map[i] = new Cell(cone);
                else if (i == cutLocation + 1)
                    map[i] = new Cell(guard);
                else
                    map[i] = new Cell(null);
            }
            Assert.IsFalse(Game.MakeStep(stepCount, map, map[1].State));
        }
        
        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 4)]
        [TestCase(4, 5)]
        public void Mini_MakeStepWithNoCutTeammates_Should(int stepCount, int cutLocation)
        {
            var map = new Cell[7];
            var coil1 = new Figure(1, 1, ChipsType.Coil);
            var coil2 = new Figure(cutLocation, 2, ChipsType.Coil);
            map[1] = new Cell(coil1);
            for (var i = 2; i < map.Length; i++)
            {
                if (i == cutLocation)
                    map[i] = new Cell(coil2);
                else
                    map[i] = new Cell(null);
            }
            Assert.IsFalse(Game.MakeStep(stepCount, map, map[1].State));
        }

        [Test]
        public void MakeStepToSecondRow_Should()
        {
            var game = new Game();
            Game.MakeStep(1, game.Map, game.Map[9].State);
            Assert.IsFalse(Game.MakeStep(2, game.Map, game.Map[9].State));
        }

        [Test]
        public void WinCondition()
        {
            var game = new Game();
            for (var i = 1; i < 15; i++)
            {
                var figure = game.Map[i].State;
                Game.MakeStep(26 - i, game.Map, figure);
                Game.MakeStep(5, game.Map, figure);
            }

            Assert.IsTrue(game.PlayerSecond.OwnFigures.Count == 0);
        }
    }
}