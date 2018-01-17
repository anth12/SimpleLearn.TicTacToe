using System.Collections.Generic;

namespace SimpleLearn.TicTacToe
{
    public class Experience
    {

        public bool IsEmpty => Positives.Count == 0 
                            && Neutrals.Count == 0 
                            && Negatives.Count == 0;

        public Dictionary<int, int> Positives { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> Neutrals { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> Negatives { get; set; } = new Dictionary<int, int>();

    }
}
