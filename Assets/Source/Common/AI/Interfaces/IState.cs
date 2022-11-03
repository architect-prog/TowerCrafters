using System.Collections;

namespace Source.Common.AI.Interfaces
{
    public interface IState
    {
        void Enter();
        IEnumerator Update();
        void Exit();
    }
}