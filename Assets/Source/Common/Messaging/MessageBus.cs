using System;

namespace Source.Common.Messaging
{
    public static class MessageBus
    {
        public static void AddListener(string eventType, Action handler)
        {
            BaseMessageBus.AddListener(eventType, handler);
        }

        public static void RemoveListener(string eventType, Action handler)
        {
            BaseMessageBus.RemoveListener(eventType, handler);
        }

        public static void Broadcast(string eventType)
        {
            var invocationList = BaseMessageBus.GetInvocationList<Action>(eventType);
            foreach (var callback in invocationList)
            {
                callback.Invoke();
            }
        }
    }

    public static class MessageBus<T>
    {
        public static void AddListener(string eventType, Action<T> handler)
        {
            BaseMessageBus.AddListener(eventType, handler);
        }

        public static void RemoveListener(string eventType, Action<T> handler)
        {
            BaseMessageBus.RemoveListener(eventType, handler);
        }

        public static void Broadcast(string eventType, T arg0)
        {
            var invocationList = BaseMessageBus.GetInvocationList<Action<T>>(eventType);
            foreach (var callback in invocationList)
            {
                callback.Invoke(arg0);
            }
        }
    }
}