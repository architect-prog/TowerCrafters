using System;
using Source.Core.Components.Units.Enemies;

namespace Source.Core.Components.Towers.Interfaces
{
    public interface ITargetSelector : IDisposable
    {
        Enemy GetTarget(Enemy[] targets, Enemy selectedTarget);
    }
}