using System;
using Source.Kernel.Contracts;

namespace Source.Kernel.Constants
{
    public static class Aggregators
    {
        public static readonly Func<float, float, float> FloatSum = (x, y) => x + y;
        public static readonly Func<DamageAmount, DamageAmount, DamageAmount> DamageAmountSum = (x, y) => x + y;
        public static readonly Func<ResistAmount, ResistAmount, ResistAmount> ResistAmountSum = (x, y) => x + y;
    }
}