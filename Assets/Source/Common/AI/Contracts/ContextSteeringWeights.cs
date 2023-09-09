using Source.Common.Extensions;

namespace Source.Common.AI.Contracts
{
    public class ContextSteeringWeights
    {
        public float[] Danger { get; }
        public float[] Interest { get; }
        public float[] Result { get; }

        public ContextSteeringWeights(int numOfDirections)
        {
            Danger = new float[numOfDirections];
            Interest = new float[numOfDirections];
            Result = new float[numOfDirections];
        }

        public void Clear()
        {
            Danger?.Clear();
            Interest?.Clear();
            Result?.Clear();
        }
    }
}