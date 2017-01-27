using System;
using System.Collections.Generic;
using System.Linq;

namespace Umbraco.Core.Events
{
    /// <summary>
    /// This event manager supports event cancellation and will raise the events as soon as they are tracked, it does not store tracked events
    /// </summary>
    internal class PassThroughEventDispatcher : DisposableObject, IEventDispatcher
    {
        public void QueueEvent(EventHandler e, object sender, EventArgs args, string eventName = null)
        {
            if (e == null) return;
            e(sender, args);
        }

        public void QueueEvent<TEventArgs>(EventHandler<TEventArgs> e, object sender, TEventArgs args, string eventName = null)
        {
            if (e == null) return;
            e(sender, args);
        }

        public void QueueEvent<TSender, TEventArgs>(TypedEventHandler<TSender, TEventArgs> e, TSender sender, TEventArgs args, string eventName = null)
        {
            if (e == null) return;
            e(sender, args);
        }

        public bool DispatchCancelable(EventHandler eventHandler, object sender, CancellableEventArgs args)
        {
            if (eventHandler == null) return args.Cancel;
            eventHandler(sender, args);
            return args.Cancel;
        }

        public bool DispatchCancelable<TArgs>(EventHandler<TArgs> eventHandler, object sender, TArgs args)
            where TArgs : CancellableEventArgs
        {
            if (eventHandler == null) return args.Cancel;
            eventHandler(sender, args);
            return args.Cancel;
        }

        public bool DispatchCancelable<TSender, TArgs>(TypedEventHandler<TSender, TArgs> eventHandler, TSender sender, TArgs args)
            where TArgs : CancellableEventArgs
        {
            if (eventHandler == null) return args.Cancel;
            eventHandler(sender, args);
            return args.Cancel;
        }

        public void Dispatch(EventHandler eventHandler, object sender, EventArgs args)
        {
            if (eventHandler == null) return;
            eventHandler(sender, args);
        }

        public void Dispatch<TArgs>(EventHandler<TArgs> eventHandler, object sender, TArgs args)
        {
            if (eventHandler == null) return;
            eventHandler(sender, args);
        }

        public void Dispatch<TSender, TArgs>(TypedEventHandler<TSender, TArgs> eventHandler, TSender sender, TArgs args)
        {
            if (eventHandler == null) return;
            eventHandler(sender, args);
        }

        public IEnumerable<IEventDefinition> GetEvents()
        {
            return Enumerable.Empty<IEventDefinition>();
        }

        public bool SupportsEventCancellation
        {
            get { return true; }
        }

        protected override void DisposeResources()
        {
            //noop
        }
    }
}