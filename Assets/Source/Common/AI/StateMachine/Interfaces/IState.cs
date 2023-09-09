using System.Collections;

namespace Source.Common.AI.StateMachine.Interfaces
{
    public interface IState
    {
        void Enter();
        IEnumerator Update();
        void Exit();
    }
}