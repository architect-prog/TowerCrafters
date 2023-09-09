using UnityEngine;

namespace Source.Common.AI.ContextSteering
{
    public class ContextSteeringAI : MonoBehaviour
    {
        [SerializeField] private ContextSteering contextSteering;

        private Vector2 madeDecision;

        private void Update()
        {
            var currentPosition = (Vector2) transform.position;
            madeDecision = contextSteering.MakeDecision(currentPosition);

            transform.position = Vector2.MoveTowards(currentPosition, currentPosition + madeDecision, Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, madeDecision * 4);
        }
    }
}