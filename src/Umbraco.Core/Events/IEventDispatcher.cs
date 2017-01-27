﻿using System;
using System.Collections.Generic;

namespace Umbraco.Core.Events
{
    public interface IEventDispatcher : IDisposable
    {
        // fixme - refactor it all!

        // fixme - what's about eventName?!
        //bool DispatchCancellable(EventHandler e, object sender, EventArgs args);
        //void Dispatch(EventHandler e, object sender, EventArgs args);
        //
        // and we DON'T want
        // SupportsEventCancellation - wtf?
        // GetEvents - should be all managed by the IEventDispatcher implementation!
        // no need for it to be disposable

        bool DispatchCancelable(EventHandler eventHandler, object sender, CancellableEventArgs args);
        bool DispatchCancelable<TArgs>(EventHandler<TArgs> eventHandler, object sender, TArgs args)
            where TArgs : CancellableEventArgs;
        bool DispatchCancelable<TSender, TArgs>(TypedEventHandler<TSender, TArgs> eventHandler, TSender sender, TArgs args)
            where TArgs : CancellableEventArgs;
        void Dispatch(EventHandler eventHandler, object sender, EventArgs args);
        void Dispatch<TArgs>(EventHandler<TArgs> eventHandler, object sender, TArgs args);
        void Dispatch<TSender, TArgs>(TypedEventHandler<TSender, TArgs> eventHandler, TSender sender, TArgs args);

        IEnumerable<IEventDefinition> GetEvents();
        void QueueEvent(EventHandler e, object sender, EventArgs args, string eventName = null);
        void QueueEvent<TEventArgs>(EventHandler<TEventArgs> e, object sender, TEventArgs args, string eventName = null);
        void QueueEvent<TSender, TEventArgs>(TypedEventHandler<TSender, TEventArgs> e, TSender sender, TEventArgs args, string eventName = null);

        /// <summary>
        /// True or false depending on if the event manager supports event cancellation
        /// </summary>
        bool SupportsEventCancellation { get; }
    }
}