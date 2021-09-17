using System;
using System.Collections.Generic;
using FasterGames.Whiskey.Boxes;
using UnityEngine;

namespace FasterGames.Whiskey.Events
{
    /// <summary>
    /// The base of all Whiskey events.
    /// </summary>
    public abstract class EventBase : ScriptableObject
    {
        /// <summary>
        /// The number of currently listening listeners.
        /// </summary>
        public abstract int ListenerCount { get; }
        
        /// <summary>
        /// The argument types of the event.
        /// </summary>
        public abstract List<Type> EventTypes { get; }
        
        /// <summary>
        /// An unsafe (can fail at runtime) hook to raise events. Do not use.
        /// </summary>
        /// <remarks>
        /// This exists to expose a hook for generic code (e.g. editors) that may want to invoke events knowing only the base type.
        /// </remarks>
        /// <param name="args">event arguments</param>
        public abstract void UnsafeRaise(params object[] args);
    }

    /// <summary>
    /// An argument-less event.
    /// </summary>
    public abstract class Event : EventBase
    {
        /// <inheritdoc />
        public override int ListenerCount => Listeners.Count;
        
        /// <inheritdoc />
        public override List<Type> EventTypes => new List<Type>();
        
        /// <summary>
        /// Storage for the bound listeners.
        /// </summary>
        protected readonly List<Action> Listeners = new List<Action>();

        /// <summary>
        /// Raise the event, invoking all listeners.
        /// </summary>
        public void Raise()
        {
            foreach (var listener in Listeners)
            {
                listener.Invoke();
            }
        }

        /// <inheritdoc />
        public override void UnsafeRaise(params object[] args)
        {
            Raise();
        }

        /// <summary>
        /// Add a listener to be invoked when the event is raised with <see cref="Raise"/>.
        /// </summary>
        /// <param name="listener">listener</param>
        public void AddListener(Action listener)
        {
            Listeners.Add(listener);
        }

        /// <summary>
        /// Remove a listener, so it will no longer be invoked when the event is raised with <see cref="Raise"/>
        /// </summary>
        /// <param name="listener">listener</param>
        public void RemoveListener(Action listener)
        {
            Listeners.Remove(listener);
        }
    }

    /// <summary>
    /// A single argument event.
    /// </summary>
    /// <typeparam name="TArg0">Argument type</typeparam>
    public abstract class Event<TArg0> : EventBase
    {
        /// <inheritdoc />
        public override int ListenerCount => Listeners.Count;
        /// <inheritdoc />
        public override List<Type> EventTypes => new List<Type>() {typeof(TArg0)};

        /// <summary>
        /// Storage for the bound listeners.
        /// </summary>
        protected readonly List<Action<TArg0>> Listeners = new List<Action<TArg0>>();

        /// <summary>
        /// Raise the event, invoking all listeners.
        /// </summary>
        /// <param name="arg0">first argument</param>
        public void Raise(TArg0 arg0)
        {
            foreach (var listener in Listeners)
            {
                listener.Invoke(arg0);
            }
        }

        /// <summary>
        /// Raise the event with a box, invoking all listeners.
        /// </summary>
        /// <remarks>
        /// The value inside the box will be used to raise the event.
        /// </remarks>
        /// <param name="arg0">first argument, boxed</param>
        public void Raise(Box<TArg0> arg0)
        {
            Raise(arg0.Read());
        }
        
        /// <inheritdoc />
        public override void UnsafeRaise(params object[] args)
        {
            Raise((TArg0)args[0]);
        }

        /// <summary>
        /// Add a listener to be invoked when the event is raised with <see cref="Raise(TArg0)"/>.
        /// </summary>
        /// <param name="listener">listener</param>
        public void AddListener(Action<TArg0> listener)
        {
            Listeners.Add(listener);
        }

        /// <summary>
        /// Remove a listener, so it will no longer be invoked when the event is raised with <see cref="Raise(TArg0)"/>
        /// </summary>
        /// <param name="listener">listener</param>
        public void RemoveListener(Action<TArg0> listener)
        {
            Listeners.Remove(listener);
        }
    }
    
    /// <summary>
    /// A dual argument event.
    /// </summary>
    /// <typeparam name="TArg0">Argument type 1</typeparam>
    /// <typeparam name="TArg1">Argument type 2</typeparam>
    public abstract class Event<TArg0, TArg1> : EventBase
    {
        /// <inheritdoc />
        public override int ListenerCount => Listeners.Count;
        
        /// <inheritdoc />
        public override List<Type> EventTypes => new List<Type>() {typeof(TArg0), typeof(TArg1)};

        /// <summary>
        /// Storage for the bound listeners.
        /// </summary>
        protected readonly List<Action<TArg0, TArg1>> Listeners = new List<Action<TArg0, TArg1>>();

        /// <summary>
        /// Raise the event, invoking all listeners.
        /// </summary>
        /// <param name="arg0">first argument</param>
        /// <param name="arg1">second argument</param>
        public void Raise(TArg0 arg0, TArg1 arg1)
        {
            foreach (var listener in Listeners)
            {
                listener.Invoke(arg0, arg1);
            }
        }
        
        /// <summary>
        /// Raise the event with a box, invoking all listeners.
        /// </summary>
        /// <remarks>
        /// The value inside the box will be used to raise the event.
        /// </remarks>
        /// <param name="arg0">first argument, boxed</param>
        /// <param name="arg1">second argument, boxed</param>
        public void Raise(Box<TArg0> arg0, Box<TArg1> arg1)
        {
            Raise(arg0.Read(), arg1.Read());
        }
        
        /// <inheritdoc />
        public override void UnsafeRaise(params object[] args)
        {
            Raise((TArg0)args[0], (TArg1)args[1]);
        }

        /// <summary>
        /// Add a listener to be invoked when the event is raised with <see cref="Raise(TArg0,TArg1)"/>.
        /// </summary>
        /// <param name="listener">listener</param>
        public void AddListener(Action<TArg0, TArg1> listener)
        {
            Listeners.Add(listener);
        }

        /// <summary>
        /// Remove a listener, so it will no longer be invoked when the event is raised with <see cref="Raise(TArg0,TArg1)"/>
        /// </summary>
        /// <param name="listener">listener</param>
        public void RemoveListener(Action<TArg0, TArg1> listener)
        {
            Listeners.Remove(listener);
        }
    }
    
    /// <summary>
    /// A three argument event.
    /// </summary>
    /// <typeparam name="TArg0">Argument type 1</typeparam>
    /// <typeparam name="TArg1">Argument type 2</typeparam>
    /// <typeparam name="TArg2">Argument type 3</typeparam>
    public abstract class Event<TArg0, TArg1, TArg2> : EventBase
    {
        /// <inheritdoc />
        public override int ListenerCount => Listeners.Count;
        
        /// <inheritdoc />
        public override List<Type> EventTypes => new List<Type>() {typeof(TArg0), typeof(TArg1), typeof(TArg2)};

        /// <summary>
        /// Storage for the bound listeners.
        /// </summary>
        protected readonly List<Action<TArg0, TArg1, TArg2>> Listeners = new List<Action<TArg0, TArg1, TArg2>>();

        /// <summary>
        /// Raise the event, invoking all listeners.
        /// </summary>
        /// <param name="arg0">first argument</param>
        /// <param name="arg1">second argument</param>
        /// <param name="arg2">third argument</param>
        public void Raise(TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            foreach (var listener in Listeners)
            {
                listener.Invoke(arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Raise the event with a box, invoking all listeners.
        /// </summary>
        /// <remarks>
        /// The value inside the box will be used to raise the event.
        /// </remarks>
        /// <param name="arg0">first argument, boxed</param>
        /// <param name="arg1">second argument, boxed</param>
        /// <param name="arg2">third argument, boxed</param>
        public void Raise(Box<TArg0> arg0, Box<TArg1> arg1, Box<TArg2> arg2)
        {
            Raise(arg0.Read(), arg1.Read(), arg2.Read());
        }
        
        /// <inheritdoc />
        public override void UnsafeRaise(params object[] args)
        {
            Raise((TArg0)args[0], (TArg1)args[1], (TArg2)args[2]);
        }
        
        /// <summary>
        /// Add a listener to be invoked when the event is raised with <see cref="Raise(TArg0,TArg1,TArg2)"/>.
        /// </summary>
        /// <param name="listener">listener</param>
        public void AddListener(Action<TArg0, TArg1, TArg2> listener)
        {
            Listeners.Add(listener);
        }

        /// <summary>
        /// Remove a listener, so it will no longer be invoked when the event is raised with <see cref="Raise(TArg0,TArg1,TArg2)"/>
        /// </summary>
        /// <param name="listener">listener</param>
        public void RemoveListener(Action<TArg0, TArg1, TArg2> listener)
        {
            Listeners.Remove(listener);
        }
    }
    
    /// <summary>
    /// A four argument event.
    /// </summary>
    /// <typeparam name="TArg0">Argument type 1</typeparam>
    /// <typeparam name="TArg1">Argument type 2</typeparam>
    /// <typeparam name="TArg2">Argument type 3</typeparam>
    /// <typeparam name="TArg3">Argument type 4</typeparam>
    public abstract class Event<TArg0, TArg1, TArg2, TArg3> : EventBase
    {
        /// <inheritdoc />
        public override int ListenerCount => Listeners.Count;

        /// <inheritdoc />
        public override List<Type> EventTypes => new List<Type>()
            {typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3)};
        
        /// <summary>
        /// Storage for the bound listeners.
        /// </summary>
        protected readonly List<Action<TArg0, TArg1, TArg2, TArg3>> Listeners =
            new List<Action<TArg0, TArg1, TArg2, TArg3>>();

        /// <summary>
        /// Raise the event, invoking all listeners.
        /// </summary>
        /// <param name="arg0">first argument</param>
        /// <param name="arg1">second argument</param>
        /// <param name="arg2">third argument</param>
        /// <param name="arg3">fourth argument</param>
        public void Raise(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            foreach (var listener in Listeners)
            {
                listener.Invoke(arg0, arg1, arg2, arg3);
            }
        }
        
        /// <summary>
        /// Raise the event with a box, invoking all listeners.
        /// </summary>
        /// <remarks>
        /// The value inside the box will be used to raise the event.
        /// </remarks>
        /// <param name="arg0">first argument, boxed</param>
        /// <param name="arg1">second argument, boxed</param>
        /// <param name="arg2">third argument, boxed</param>
        /// <param name="arg3">fourth argument, boxed</param>
        public void Raise(Box<TArg0> arg0, Box<TArg1> arg1, Box<TArg2> arg2, Box<TArg3> arg3)
        {
            Raise(arg0.Read(), arg1.Read(), arg2.Read(), arg3.Read());
        }

        /// <inheritdoc />
        public override void UnsafeRaise(params object[] args)
        {
            Raise((TArg0)args[0], (TArg1)args[1], (TArg2)args[2], (TArg3)args[3]);
        }
        
        /// <summary>
        /// Add a listener to be invoked when the event is raised with <see cref="Raise(TArg0,TArg1,TArg2,TArg3)"/>.
        /// </summary>
        /// <param name="listener">listener</param>
        public void AddListener(Action<TArg0, TArg1, TArg2, TArg3> listener)
        {
            Listeners.Add(listener);
        }

        /// <summary>
        /// Remove a listener, so it will no longer be invoked when the event is raised with <see cref="Raise(TArg0,TArg1,TArg2,TArg3)"/>
        /// </summary>
        /// <param name="listener">listener</param>
        public void RemoveListener(Action<TArg0, TArg1, TArg2, TArg3> listener)
        {
            Listeners.Remove(listener);
        }
    }
}