using System;

namespace HyperCasual.Data
{
    public class EventCallback
    {
        public bool SingleUse { get; private set; }
        public EventHandler Target { get; private set; }

        public static EventCallback Single(EventHandler handler)
        {
            return new EventCallback(true, handler);
        }

        public static EventCallback Repeated(EventHandler handler)
        {
            return new EventCallback(false, handler);
        }

        private EventCallback(bool single_use, EventHandler handler)
        {
            SingleUse = single_use;
            Target = handler;
        }
    }
}
