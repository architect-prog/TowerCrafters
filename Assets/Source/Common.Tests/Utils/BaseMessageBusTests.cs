using System;
using NUnit.Framework;
using Source.Common.Exceptions;
using Source.Common.Messaging;

namespace Source.Common.Tests.Utils
{
    [TestFixture]
    public class BaseMessageBusTests
    {
        private const string EventType1 = "event1";
        private const string EventType2 = "event2";

        private Action testAction1;
        private Action testAction2;
        private Action testAction3;
        private Action<int> testAction4;
        private Action<string> testAction5;

        [SetUp]
        public void SetUp()
        {
            testAction1 = () => { };
            testAction2 = () => { };
            testAction3 = () => { };
            testAction4 = _ => { };
            testAction5 = _ => { };
        }

        [Test]
        public void BaseMessageBus_When_Add_Null_Listener_Should_Throw_ArgumentNullException()
        {
            var act = new TestDelegate(() => { BaseMessageBus.AddListener(EventType1, null); });

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void BaseMessageBus_When_Add_Listeners_Of_Different_Types_Should_Throw_ListenerException()
        {
            var act = new TestDelegate(() =>
            {
                BaseMessageBus.AddListener(EventType1, testAction1);
                BaseMessageBus.AddListener(EventType1, testAction4);
            });

            Assert.Throws<ListenerException>(act);
        }

        [Test]
        public void BaseMessageBus_When_Add_Listeners_Should_Add_Listeners_Correctly()
        {
            BaseMessageBus.AddListener(EventType1, testAction1);
            BaseMessageBus.AddListener(EventType1, testAction2);
            BaseMessageBus.AddListener(EventType1, testAction3);
            BaseMessageBus.AddListener(EventType2, testAction5);

            var result = BaseMessageBus.Listeners;

            Assert.That(result, Contains.Key(EventType1));
            Assert.That(result, Contains.Key(EventType2));
            Assert.That(result, Contains.Value(Delegate.Combine(testAction1, testAction2, testAction3)));
            Assert.That(result, Contains.Value(testAction5));
        }

        [Test]
        public void BaseMessageBus_When_Remove_Null_Should_Throw_ArgumentNullException()
        {
            var act = new TestDelegate(() => { BaseMessageBus.RemoveListener(EventType1, null); });

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void BaseMessageBus_When_Remove_Incorrect_Type_Should_Throw_ListenerNullException()
        {
            var act = new TestDelegate(() =>
            {
                BaseMessageBus.AddListener(EventType1, testAction1);
                BaseMessageBus.RemoveListener(EventType1, testAction4);
            });

            Assert.Throws<ListenerException>(act);
        }

        [Test]
        public void BaseMessageBus_When_Remove_Not_Existing_Listener_Should_Leave_Bus_As_It_Is()
        {
            BaseMessageBus.RemoveListener(EventType1, testAction2);

            var result = BaseMessageBus.Listeners;

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BaseMessageBus_When_Remove_Should_Remove_Listener()
        {
            BaseMessageBus.AddListener(EventType1, testAction1);
            BaseMessageBus.AddListener(EventType1, testAction2);
            BaseMessageBus.AddListener(EventType1, testAction3);

            BaseMessageBus.RemoveListener(EventType1, testAction1);
            BaseMessageBus.RemoveListener(EventType1, testAction2);

            var result = BaseMessageBus.Listeners;

            Assert.That(result, Contains.Key(EventType1));
            Assert.That(result, Contains.Value(testAction3));
        }

        [Test]
        public void BaseMessageBus_When_Remove_All_Listeners_Should_Remove_Event()
        {
            BaseMessageBus.AddListener(EventType1, testAction1);

            BaseMessageBus.RemoveListener(EventType1, testAction1);

            var result = BaseMessageBus.Listeners;

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BaseMessageBus_GetInvocationList_Should_Return_Expected_Value()
        {
            BaseMessageBus.AddListener(EventType1, testAction1);
            BaseMessageBus.AddListener(EventType1, testAction2);
            BaseMessageBus.AddListener(EventType1, testAction3);

            var result = BaseMessageBus.GetInvocationList<Action>(EventType1);

            Assert.That(result, Is.EqualTo(new[] {testAction1, testAction2, testAction3}));
        }

        [Test]
        public void BaseMessageBus_GetInvocationList_When_Listeners_Empty_Should_Return_Empty_Collection()
        {
            var result = BaseMessageBus.GetInvocationList<Action>(EventType1);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BaseMessageBus_GetInvocationList_When_Listener_Type_Incorrect_Should_Throw_InvalidCastException()
        {
            BaseMessageBus.AddListener(EventType1, testAction1);

            var act = new TestDelegate(() => { BaseMessageBus.GetInvocationList<Action<int>>(EventType1); });

            Assert.Throws<InvalidCastException>(act);
        }

        [TearDown]
        public void TearDown()
        {
            BaseMessageBus.Clear();
        }
    }
}