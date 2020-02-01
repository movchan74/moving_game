using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace HyperCasual.Extensions
{
    /// <summary>
    /// Extends the generic component class with log operations.
    /// </summary>
    public static class ComponentLogExtensions
    {
        [Conditional("DEBUG")]
        public static void LogMessage<T>(this T component, string format, params object[] args) where T : Component
        {
            Debug.LogFormat("{0}::{1}", component.GetType().Name, string.Format(format, args));
        }

        [Conditional("DEBUG")]
        public static void LogWarning<T>(this T component, string format, params object[] args) where T : Component
        {
            Debug.LogWarningFormat("{0}::{1}", component.GetType().Name, string.Format(format, args));
        }

        [Conditional("DEBUG")]
        public static void LogError<T>(this T component, string format, params object[] args) where T : Component
        {
            Debug.LogErrorFormat("{0}::{1}", component.GetType().Name, string.Format(format, args));
        }

    }
}
