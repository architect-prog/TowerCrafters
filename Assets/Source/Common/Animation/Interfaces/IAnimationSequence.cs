using System;

namespace Source.Common.Animation.Interfaces
{
    public interface IAnimationSequence : IDisposable
    {
        IAnimationSequence AddAnimation(IAnimationEffect effect);
        public void Stop();
        void Execute();
    }
}