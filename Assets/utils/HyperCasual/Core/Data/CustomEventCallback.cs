using System;

namespace HyperCasual.Data
{
    public class EventCallback<T> where T : EventArgs
    {
        public bool SingleUse { get; private set; }
        public EventHandler<T> Target { get; private set; }

        public static EventCallback<T> Single(EventHandler<T> handler)
        {
            return new EventCallback<T>(true, handler);
        }

        public static EventCallback<T> Repeated(EventHandler<T> handler)
        {
            return new EventCallback<T>(false, handler);
        }

        private EventCallback(bool single_use, EventHandler<T> handler)
        {
            SingleUse = single_use;
            Target = handler;
        }
    }
}
