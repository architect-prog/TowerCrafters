using System;
using System.Runtime.Serialization;

namespace Source.Common.Exceptions
{
    [Serializable]
    public class ListenerException : Exception
    {
        public ListenerException(string message) : base(message)
        {
        }

        protected ListenerException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}