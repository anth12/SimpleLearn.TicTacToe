
using System;

namespace SimpleLearn.TicTacToe
{
    public struct BoardPosition
    {
        public BoardPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 0 Index X position
        /// </summary>
        public int X { get; set; }

        
        /// <summary>
        /// 0 Index Y position
        /// </summary>
        public int Y { get; set; }

        public int Index => ToIndex();

        /// <summary>
        /// 012
        /// 345
        /// 678
        /// </summary>
        /// <returns></returns>
        public int ToIndex()
        {
            return (Y * 3) + X;
        }

        /// <summary>
        // 00 01 02
        // 10 11 12
        // 20 21 22
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static BoardPosition ParseIndex(int index)
        {
            return new BoardPosition(index % 3, (int)Math.Floor(index / 3m));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BoardPosition))
            {
                return false;
            }

            var position = (BoardPosition)obj;
            return X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
