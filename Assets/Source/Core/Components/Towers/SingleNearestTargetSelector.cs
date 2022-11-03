using System.Linq;
using Source.Common.Extensions;
using Source.Core.Components.Towers.Interfaces;
using Source.Core.Components.Units.Enemies;
using UnityEngine;

namespace Source.Core.Components.Towers
{
    public class SingleNearestTargetSelector : ITargetSelector
    {
        private MonoBehaviour owner;

        public SingleNearestTargetSelector(MonoBehaviour owner)
        {
            this.owner = owner;
        }

        public Enemy GetTarget(Enemy[] targets, Enemy selectedTarget)
        {  
            if (targets.Contains(selectedTarget))
                return selectedTarget;

            var result = targets
                .Where(x => x)
                .GetNearestFor(owner);

            return result;
        }

        public void Dispose()
        {
            owner = null;
        }
    }
}