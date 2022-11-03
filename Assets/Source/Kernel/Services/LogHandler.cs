using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Kernel.Services
{
    public class LogHandler : ILogHandler
    {
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            Debug.unityLogger.logHandler.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, Object context)
        {
            Debug.unityLogger.LogException(exception, context);
        }
    }
}