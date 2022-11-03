namespace Source.Kernel.Contracts
{
    public class Effect<T>
    {
        public T Value { get; }

        public Effect(T value)
        {
            Value = value;
        }
    }
}