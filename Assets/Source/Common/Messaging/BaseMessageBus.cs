using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Source.Common.Constants;
using Source.Common.Exceptions;

namespace Source.Common.Messaging
{
    public static class BaseMessageBus
    {
        private static readonly ConcurrentDictionary<string, Delegate> listeners = new();

        public static IReadOnlyDictionary<string, Delegate> Listeners => listeners;

        public static void AddListener(string eventType, Delegate listener)
        {
            if (listener is null)
                throw new ArgumentNullException(nameof(listener));

            if (listeners.TryAdd(eventType, listener))
                return;

            var existingListener = listeners[eventType];
            if (existingListener.GetType() == listener.GetType())
            {
                listeners[eventType] = Delegate.Combine(existingListener, listener);
                return;
            }

            var message = string.Format(ExceptionConstants.FailedToAddListener,
                eventType, listener.GetType().Name, existingListener.GetType().Name);
            throw new ListenerException(message);
        }

        public static void RemoveListener(string eventType, Delegate listener)
        {
            if (listener is null)
                throw new ArgumentNullException(nameof(listener));

            if (!listeners.TryGetValue(eventType, out var existingListener))
                return;

            if (existingListener.GetType() == listener.GetType())
            {
                listeners[eventType] = Delegate.Remove(listeners[eventType], listener);
                if (listeners[eventType] is null)
                    listeners.Remove(eventType, out _);

                return;
            }

            var message = string.Format(ExceptionConstants.FailedToRemoveListener,
                eventType, existingListener.GetType().Name, listener.GetType().Name);
            throw new ListenerException(message);
        }

        public static T[] GetInvocationList<T>(string eventType)
        {
            if (!listeners.TryGetValue(eventType, out var eventListener))
            {
                return Array.Empty<T>();
            }

            var result = eventListener.GetInvocationList()
                .Cast<T>()
                .ToArray();

            return result;
        }

        public static void Clear()
        {
            listeners.Clear();
        }
    }
}