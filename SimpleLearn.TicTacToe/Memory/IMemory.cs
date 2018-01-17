
namespace SimpleLearn.TicTacToe.Memory
{
    public interface IMemory
    {
        Experience GetState(int stateHash);
        void SetState(int stateHash, Experience payload);
    }
}
