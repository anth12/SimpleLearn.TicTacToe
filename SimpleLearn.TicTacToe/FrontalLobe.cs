using System;
using System.Collections.Generic;
using SimpleLearn.TicTacToe.Memory;
using System.Linq;

namespace SimpleLearn.TicTacToe
{
    public class FrontalLobe
    {
        private readonly IMemory memory;
        private readonly Random random;
        private static readonly double[] DefaultScore = { 4, 3, 2, 1, 1 };

        public FrontalLobe(IMemory memory)
        {
            this.memory = memory;
            this.random = new Random();
        }

        public Move NextMove(Game game)
        {
            var experience = GetExperience(game.CurrentBoard);
            
            var moveNumber = game.CurrentBoard.IndexedValue.Count(c => c == 'X');

            var moveOptions = new List<int>();

            for (int i = 0; i <= 8; i++)
            {
                // Check for occupied cells
                var cell = game.CurrentBoard.IndexedValue[i];
                if(cell == 'X' || cell == 'O')
                    continue;

                var position = BoardPosition.ParseIndex(i);
                var positionScore = DefaultScore[moveNumber];
                var moveKey = position.GetHashCode();

                if (experience.Positives.ContainsKey(moveKey))
                {
                    positionScore += experience.Positives[moveKey] * 3;
                }

                if (experience.Neutrals.ContainsKey(moveKey))
                {
                    // Neutral's have minor boost
                    positionScore += experience.Neutrals[moveKey];
                }

                if (experience.Negatives.ContainsKey(moveKey))
                    positionScore -= experience.Negatives[moveKey];

                for (int moveIndex = 0; moveIndex < positionScore; moveIndex++)
                {
                    moveOptions.Add(i);
                }
            }

            return new Move
            {
                BoardState = game.CurrentBoard,
                Position = BoardPosition.ParseIndex(
                        moveOptions[random.Next(0, moveOptions.Count - 1)]
                    )
            };
        }

        private static List<Func<BoardState, BoardState>> IdenticalStates = new List<Func<BoardState, BoardState>>
        {
            (b) => b,

            // Rotations
            (b) => b.RotateClockwise(),
            (b) => b.RotateClockwise().RotateClockwise(),
            (b) => b.RotateClockwise().RotateClockwise().RotateClockwise(),

            // Symetry
            (b) => b.RotateClockwise().RotateClockwise(),

        };

        public Experience GetExperience(BoardState board)
        {
            Experience experience = null;
            foreach (var stateModifyer in IdenticalStates)
            {
                var identicalBoard = stateModifyer(board);
                experience = memory.GetState(identicalBoard.GetHashCode());

                if (!experience.IsEmpty)
                    break;
            }

            return experience;
        }
        
    }
}
