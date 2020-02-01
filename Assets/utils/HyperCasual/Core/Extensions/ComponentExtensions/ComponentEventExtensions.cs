using System;
using HyperCasual.Data;
using UnityEngine;
using Event = HyperCasual.Data.Event;

namespace HyperCasual.Extensions
{
    /// <summary>
    /// Extends the generic component class with common event operations.
    /// </summary>
    public static class ComponentEventExtensions
    {
        public static void Raise<T>(this Component componet, Event<T> target_event, T args) where T : EventArgs
        {
            target_event.Send(componet, args);
        }

        public static void Raise(this Component component, Event target_event)
        {
            target_event.Send(component);
        }
    }
}
