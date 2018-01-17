using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleLearn.TicTacToe.Tests.Game
{
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void GameState_Matches_Success()
        {
            var game1 = new TicTacToe.Game();
            game1.CurrentBoard.Add(UserType.EndUser, 1, 1)
                        .Add(UserType.Machine, 2, 2);


            var game2 = new TicTacToe.Game();
            game2.CurrentBoard.Add(UserType.EndUser, 1, 1)
                        .Add(UserType.Machine, 2, 2);

            Assert.AreEqual(game1.State, game2.State);
        }

        [TestMethod]
        public void GameState_MisMatchesPosition_Success()
        {
            var game1 = new TicTacToe.Game();
            game1.CurrentBoard.Add(UserType.EndUser, 1, 1)
                        .Add(UserType.Machine, 2, 2);


            var game2 = new TicTacToe.Game();
            game2.CurrentBoard.Add(UserType.EndUser, 1, 1)
                        .Add(UserType.Machine, 1, 2);

            Assert.AreNotEqual(game1.State, game2.State);
        }

        [TestMethod]
        public void GameState_MisMatchesUser_Success()
        {
            var game1 = new TicTacToe.Game();
            game1.CurrentBoard.Add(UserType.EndUser, 1, 1)
                .Add(UserType.Machine, 2, 2);


            var game2 = new TicTacToe.Game();
            game2.CurrentBoard.Add(UserType.Machine, 1, 1)
                .Add(UserType.EndUser, 2, 2);

            Assert.AreNotEqual(game1.State, game2.State);
        }

        [TestMethod]
        public void GameState_MisMatchesOrder_Success()
        {
            var game1 = new TicTacToe.Game();
            game1.CurrentBoard.Add(UserType.EndUser, 1, 1)
                .Add(UserType.Machine, 2, 2);


            var game2 = new TicTacToe.Game();
            game2.CurrentBoard.Add(UserType.Machine, 2, 2)
                .Add(UserType.EndUser, 1, 1);

            Assert.AreEqual(game1.State, game2.State);
        }

        [TestMethod]
        public void GameState_FullBoard_Success()
        {
            var game = new TicTacToe.Game();
            game.CurrentBoard.Add(UserType.EndUser, 0, 0)
                .Add(UserType.Machine, 0, 1)
                .Add(UserType.EndUser, 0, 2)

                .Add(UserType.Machine, 1, 0)
                .Add(UserType.EndUser, 1, 1)
                .Add(UserType.Machine, 1, 2)

                .Add(UserType.EndUser, 2, 0)
                .Add(UserType.Machine, 2, 1)
                .Add(UserType.EndUser, 2, 2);

            Assert.AreNotEqual(null, game.State);
            Assert.AreNotEqual(null, game.State);
        }
    }
}
