using System.Linq;

namespace SimpleLearn.TicTacToe
{
    public class BoardState
    {
        public BoardState()
        {
            Value = new[]
            {
                new[] {' ', ' ', ' '},
                new[] {' ', ' ', ' '},
                new[] {' ', ' ', ' '}
            };
        }

        public char[][] Value { get; set; }

        public char[] IndexedValue => Value.SelectMany(x => x).ToArray();

        public BoardState Add(UserType user, int x, int y)
        {
            Value[y][x] = "OX"[(int)user];
            return this;
        }

        public override int GetHashCode()
        {
            int hash = 0;

            hash ^= string.Join("", IndexedValue).GetHashCode();
            // TODO reflect the layout

            return hash;
        }

        public BoardState RotateClockwise()
        {
            return new BoardState
            {
                Value = new[]
                {
                    new[] {Value[2][0], Value[1][0], Value[0][0]},
                    new[] {Value[2][1], Value[1][1], Value[0][1]},
                    new[] {Value[2][2], Value[1][2], Value[0][2]},
                }
            };
        }

        public BoardState RotateCounterClockwise()
        {
            return new BoardState
            {
                Value = new[]
                {
                    new[] {Value[0][2], Value[1][2], Value[2][2]},
                    new[] {Value[0][1], Value[1][1], Value[2][1]},
                    new[] {Value[0][0], Value[1][0], Value[2][0]}
                }
            };
        }

        public BoardState ReflectX()
        {
            return new BoardState
            {
                Value = new[]
                {
                    new[] {Value[2][0], Value[2][1], Value[2][2]},
                    new[] {Value[1][0], Value[1][1], Value[1][2]},
                    new[] {Value[0][0], Value[0][1], Value[0][2]}
                }
            };
        }


        public BoardState ReflectY()
        {
            return new BoardState
            {
                Value = new[]
                {
                    new[] {Value[2][0], Value[2][1], Value[2][2]},
                    new[] {Value[1][0], Value[1][1], Value[1][2]},
                    new[] {Value[0][0], Value[0][1], Value[0][2]}
                }
            };
        }
    }
}
