using System.Collections.Generic;

namespace SimpleLearn.TicTacToe
{
    public class Game
    {
        public BoardState CurrentBoard { get; set; } = new BoardState();
        
        public List<Move> States { get; set; }


        public int State => CurrentBoard.GetHashCode();
    }
}
