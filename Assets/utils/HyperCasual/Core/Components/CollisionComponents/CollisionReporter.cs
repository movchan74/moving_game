using System;
using HyperCasual.Data;
using HyperCasual.Extensions;
using UnityEngine;

namespace HyperCasual
{
    using CollisionArgs = ValueArgs<Collision>;

    /// <summary>
    /// Responsible for reporting back using events on collisions.
    /// </summary>
    public class CollisionReporter
        : MonoBehaviour
    {
        public event EventHandler<CollisionArgs> CollisionEnter;
        public event EventHandler<CollisionArgs> CollisionExit;

        public void OnCollisionEnter(Collision data)
        {
            this.Raise(CollisionEnter, new CollisionArgs(data));
        }

        public void OnCollisionExit(Collision data)
        {
            this.Raise(CollisionExit, new CollisionArgs(data));
        }

        public CollisionReporter CallOnEnter(EventHandler<CollisionArgs> handler)
        {
            CollisionEnter += handler;
            return this;
        }

        public CollisionReporter CallOnExit(EventHandler<CollisionArgs> handler)
        {
            CollisionExit += handler;
            return this;
        }
    }
}
