using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleLearn.TicTacToe.Tests.Game
{
    [TestClass]
    public class GameStateSymmetryTests
    {
        [TestMethod]
        public void GameState_MatchesSymmetricalAfterRotate_Success()
        {
            var game1 = new TicTacToe.Game();
            game1.CurrentBoard.Add(UserType.EndUser, 1, 1)
                .Add(UserType.Machine, 2, 2)
                .Add(UserType.EndUser, 1, 2)
                .Add(UserType.Machine, 0, 1);


            var game2 = new TicTacToe.Game();
            game2.CurrentBoard.Add(UserType.EndUser, 1, 1)
                .Add(UserType.Machine, 0, 2)
                .Add(UserType.EndUser, 1, 2)
                .Add(UserType.Machine, 0, 1);

            Assert.AreNotEqual(game1.State, game2.State);
            Assert.AreNotEqual(game1.State, game2.CurrentBoard.RotateClockwise().GetHashCode());
        }


        [TestMethod]
        public void GameState_MatchesSymmetricalAfterRotateBackAndForth_Success()
        {
            var game1 = new TicTacToe.Game();
            game1.CurrentBoard.Add(UserType.EndUser, 1, 1)
                .Add(UserType.Machine, 2, 2)
                .Add(UserType.EndUser, 1, 2)
                .Add(UserType.Machine, 0, 1);

            var rotated = game1.CurrentBoard.RotateClockwise();
            Assert.AreNotEqual(game1.State, rotated.GetHashCode());
            
            rotated = rotated.RotateCounterClockwise();
            Assert.AreEqual(game1.State, rotated.GetHashCode());
        }

        [TestMethod]
        public void GameState_MatchesSymmetricalAfterFullRotation_Success()
        {
            var game1 = new TicTacToe.Game();
            game1.CurrentBoard.Add(UserType.EndUser, 1, 1)
                .Add(UserType.Machine, 2, 2)
                .Add(UserType.EndUser, 1, 2)
                .Add(UserType.Machine, 0, 1);

            var rotated = game1.CurrentBoard.RotateClockwise();
            Assert.AreNotEqual(game1.State, rotated.GetHashCode());

            rotated = rotated.RotateClockwise();
            Assert.AreNotEqual(game1.State, rotated.GetHashCode());

            rotated = rotated.RotateClockwise();
            Assert.AreNotEqual(game1.State, rotated.GetHashCode());

            rotated = rotated.RotateClockwise();
            Assert.AreEqual(game1.State, rotated.GetHashCode());
        }
    }
}
