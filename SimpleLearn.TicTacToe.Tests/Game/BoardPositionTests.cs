using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleLearn.TicTacToe.Tests.Game
{
    [TestClass]
    public class BoardPositionTests
    {
        [TestMethod]
        public void BoardPosition_ToIndex_Success()
        {
            var zeroZeroIndex = new BoardPosition(0, 0).ToIndex();
            Assert.AreEqual(0, zeroZeroIndex);

            var zeroOneIndex = new BoardPosition(1, 0).ToIndex();
            Assert.AreEqual(1, zeroOneIndex);

            var zeroTwoIndex = new BoardPosition(2, 0).ToIndex();
            Assert.AreEqual(2, zeroTwoIndex);


            var oneZeroIndex = new BoardPosition(0, 1).ToIndex();
            Assert.AreEqual(3, oneZeroIndex);

            var oneOneIndex = new BoardPosition(1, 1).ToIndex();
            Assert.AreEqual(4, oneOneIndex);

            var oneTwoIndex = new BoardPosition(2, 1).ToIndex();
            Assert.AreEqual(5, oneTwoIndex);


            var twoZeroIndex = new BoardPosition(0, 2).ToIndex();
            Assert.AreEqual(6, twoZeroIndex);

            var twoOneIndex = new BoardPosition(1, 2).ToIndex();
            Assert.AreEqual(7, twoOneIndex);

            var twoTwoIndex = new BoardPosition(2, 2).ToIndex();
            Assert.AreEqual(8, twoTwoIndex);
        }


        [TestMethod]
        public void BoardPosition_ParseIndex_Success()
        {
            var zeroZero = BoardPosition.ParseIndex(0);
            Assert.AreEqual(new BoardPosition(0, 0), zeroZero);

            var zeroOne = BoardPosition.ParseIndex(1);
            Assert.AreEqual(new BoardPosition(1, 0), zeroOne);

            var zeroTwo = BoardPosition.ParseIndex(2);
            Assert.AreEqual(new BoardPosition(2, 0), zeroTwo);


            var oneZero = BoardPosition.ParseIndex(3);
            Assert.AreEqual(new BoardPosition(0, 1), oneZero);

            var oneOne = BoardPosition.ParseIndex(4);
            Assert.AreEqual(new BoardPosition(1, 1), oneOne);

            var oneTwo = BoardPosition.ParseIndex(5);
            Assert.AreEqual(new BoardPosition(2, 1), oneTwo);


            var twoZero = BoardPosition.ParseIndex(6);
            Assert.AreEqual(new BoardPosition(0, 2), twoZero);

            var twoOne = BoardPosition.ParseIndex(7);
            Assert.AreEqual(new BoardPosition(1, 2), twoOne);

            var twoTwo = BoardPosition.ParseIndex(8);
            Assert.AreEqual(new BoardPosition(2, 2), twoTwo);
        }

    }
}
