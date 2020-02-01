using System;
using System.Collections.Generic;

namespace HyperCasual.Data
{
    [Serializable]
    public class Event<T> where T : EventArgs
    {
        public int RegisteredHandlers { get { return _callbacks.Count; } }

        public Event<T> Send(object sender, T args)
        {
            for (var i = _callbacks.Count - 1; i >= 0; --i)
            {
                var callback = _callbacks[i];
                if (callback.SingleUse || callback.Target == null)
                {
                    _callbacks.RemoveAt(i);
                    continue;
                }

                callback.Target(sender, args);
            }

            return this;
        }

        public Event<T> CallOnce(EventHandler<T> handler)
        {
            _callbacks.Insert(0, EventCallback<T>.Single(handler));
            return this;
        }

        public Event<T> Call(EventHandler<T> handler)
        {
            _callbacks.Insert(0, EventCallback<T>.Repeated(handler));
            return this;
        }

        public Event()
        {
            _callbacks = new List<EventCallback<T>>();
        }

        private readonly List<EventCallback<T>> _callbacks;
    }
}
