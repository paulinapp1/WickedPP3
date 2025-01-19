using WickedLogic;

namespace WickedGame.Services
{
    public interface IGameService
    {
            void StartGame(Levels difficulty);
            bool IsGameOver();
            void Move(Direction direction);
            int Score { get; }
    }
}
