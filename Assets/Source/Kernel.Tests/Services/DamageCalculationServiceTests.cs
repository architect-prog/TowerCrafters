using System;
using System.Collections.Generic;
using NUnit.Framework;
using Source.Kernel.Contracts;
using Source.Kernel.Enums;
using Source.Kernel.Services;

namespace Source.Kernel.Tests.Services
{
    [TestFixture]
    public class DamageCalculationServiceTests
    {
        private readonly DamageCalculationService damageCalculationService = new();

        [Test]
        public void CalculateResultDamage_When_Damage_Is_Null_Should_Throw_ArgumentNullException()
        {
            var act = new TestDelegate(() => damageCalculationService.CalculateResultDamage(null, new ResistAmount()));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void CalculateResultDamage_When_Resist_Is_Null_Should_Throw_ArgumentNullException()
        {
            var act = new TestDelegate(() => damageCalculationService.CalculateResultDamage(new DamageAmount(), null));

            Assert.Throws<ArgumentNullException>(act);
        }

        [TestCaseSource(nameof(GetTestAmounts))]
        public void CalculateResultDamage_Should_Return_Expected_Result(
            DamageAmount damage,
            ResistAmount resist,
            float expected)
        {
            var result = damageCalculationService.CalculateResultDamage(damage, resist);

            Assert.AreEqual(expected, result);
        }

        private static IEnumerable<TestCaseData> GetTestAmounts()
        {
            var damage1 = new Damage(10f, DamageType.Physical);
            var damage2 = new Damage(15f, DamageType.Fire);
            var damage3 = new Damage(8f, DamageType.Magic);
            
            var resist1 = new Resist(0.8f, DamageType.Physical);
            var resist2 = new Resist(1f, DamageType.Fire);
            var resist3 = new Resist(0.5f, DamageType.Lightning);
            var resist4 = new Resist(5, DamageType.Physical);
            
            yield return new TestCaseData(new DamageAmount(), new ResistAmount(), 0f);
            yield return new TestCaseData(new DamageAmount(damage1), new ResistAmount(), 10f);
            yield return new TestCaseData(new DamageAmount(damage1, damage2), new ResistAmount(), 25f);
            yield return new TestCaseData(new DamageAmount(damage1, damage2), new ResistAmount(resist1), 17f);
            yield return new TestCaseData(new DamageAmount(damage1), new ResistAmount(resist2), 10f);
            yield return new TestCaseData(new DamageAmount(damage2), new ResistAmount(resist2), 0);
            yield return new TestCaseData(new DamageAmount(), new ResistAmount(resist2), 0);
            yield return new TestCaseData(new DamageAmount(damage1, damage3), new ResistAmount(resist1, resist3), 10f);
            yield return new TestCaseData(new DamageAmount(damage1), new ResistAmount(resist4), 0f);
        }
    }
}