using System;
using System.Collections.Generic;

namespace HyperCasual.Data
{
    /// <summary>
    /// Responsible for managing and calling registered handlers.
    /// </summary>
    [Serializable]
    public class Event
    {
        public int HandlerCount { get { return _callbacks.Count; } }

        public Event Send(object sender)
        {
            for (var i = _callbacks.Count - 1; i >= 0; --i)
            {
                var callback = _callbacks[i];
                if (callback.SingleUse || callback.Target == null)
                    _callbacks.RemoveAt(i);

                if (callback.Target != null)
                    callback.Target(sender, EventArgs.Empty);
            }

            return this;
        }

        public Event CallOnce(EventHandler handler)
        {
            _callbacks.Insert(0, EventCallback.Repeated(handler));
            return this;
        }

        public Event Call(EventHandler handler)
        {
            _callbacks.Insert(0, EventCallback.Repeated(handler));
            return this;
        }

        public Event()
        {
            _callbacks = new List<EventCallback>();
        }

        private readonly List<EventCallback> _callbacks;
    }
}
