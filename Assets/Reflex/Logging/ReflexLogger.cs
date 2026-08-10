using System;
using Reflex.Configuration;
using UnityEngine;

namespace Reflex.Logging
{
    internal static class ReflexLogger
    {
        private static LogLevel _logLevel;

        static ReflexLogger()
        {
            ReflexSettings.TryGetInstance(out var reflexSettings);
            Initialize(reflexSettings);
        }

        internal static void Initialize(ReflexSettings reflexSettings)
        {
            _logLevel = reflexSettings != null
                ? reflexSettings.LogLevel
                : LogLevel.Info;
            Log($"Reflex LogLevel set to {_logLevel}", LogLevel.Info);
        }

        public static void UpdateLogLevel(LogLevel logLevel)
        {
            if (logLevel != _logLevel)
            {
                _logLevel = logLevel;
                Log($"Reflex LogLevel set to {_logLevel}", LogLevel.Info);
            }
        }
        
        public static void Log(object message, LogLevel logLevel, UnityEngine.Object context = null)
        {
            if (logLevel < _logLevel)
            {
                return;
            }
            
            switch (logLevel)
            {
                case LogLevel.Development: Debug.Log(message, context); break;
                case LogLevel.Info: Debug.Log(message, context); break;
                case LogLevel.Warning: Debug.LogWarning(message, context); break;
                case LogLevel.Error: Debug.LogError(message, context); break;
                default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}