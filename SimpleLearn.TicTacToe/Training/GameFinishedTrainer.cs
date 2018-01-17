using SimpleLearn.TicTacToe.Memory;

namespace SimpleLearn.TicTacToe.Training
{
    public class GameFinishedTrainer : ITrainer
    {
        private readonly IMemory memory;
        private readonly FrontalLobe frontalLobe;

        public GameFinishedTrainer(IMemory memory, FrontalLobe frontalLobe)
        {
            this.memory = memory;
            this.frontalLobe = frontalLobe;
        }

        public void Feedback(Game game, GameResult result)
        {

            foreach (var machineMove in game.States)
            {
                var experience = frontalLobe.GetExperience(machineMove.BoardState);
                var moveKey = machineMove.Position.GetHashCode();

                if (experience == null)
                    experience = new Experience();

                switch (result)
                {
                    case GameResult.MachineWon:
                        // Reward
                        if (!experience.Positives.ContainsKey(moveKey))
                            experience.Positives.Add(moveKey, 0);

                        experience.Positives[moveKey]++;

                        break;
                    case GameResult.Draw:
                        // Punish
                        if (!experience.Neutrals.ContainsKey(moveKey))
                            experience.Neutrals.Add(moveKey, 0);

                        experience.Neutrals[moveKey]++;

                        break;
                    case GameResult.HumanWon:
                        // Minor reward
                        if (!experience.Negatives.ContainsKey(moveKey))
                            experience.Negatives.Add(moveKey, 0);

                        experience.Negatives[moveKey]++;

                        break;
                }

                memory.SetState(machineMove.BoardState.GetHashCode(), experience);
            }

        }

    }
}
