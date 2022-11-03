using UnityEngine;

namespace Source.Core.Components.Scenarios
{
    public class Scenario : MonoBehaviour
    {
        [SerializeField] private Wave[] waves;

        public Wave[] Waves => waves;
    }
}