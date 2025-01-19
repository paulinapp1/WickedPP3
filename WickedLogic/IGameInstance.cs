using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WickedLogic
{
    public interface IGameInstance
    {
        void InitializeInterrupts();
        void StartGame(Levels difficulty);
        bool IsGameOver();
        void Move(Direction direction);
        int Score { get; }
    }

}
