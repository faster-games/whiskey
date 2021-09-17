using System;
using FasterGames.Whiskey.PropertyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace FasterGames.Whiskey.Events
{
    /// <summary>
    /// The base of all Whiskey Event Listeners.
    /// </summary>
    public abstract class EventListenerBase : MonoBehaviour
    {
        /// <summary>
        /// The event to listen to.
        /// </summary>
        public abstract EventBase Event { get; }
        
        /// <summary>
        /// The reaction that occurs when the event is raised.
        /// </summary>
        /// <remarks>
        /// Only raised if the Game Object this is attached to is active.
        /// </remarks>
        public abstract UnityEventBase Reaction { get; }
    }
    
    /// <summary>
    /// Argument-less event listener.
    /// </summary>
    public abstract class EventListener : EventListenerBase
    {
        /// <summary>
        /// A reaction for an argument-less event.
        /// </summary>
        [Serializable]
        public class UnityReaction : UnityEvent
        {
        }

        /// <inheritdoc />
        public override EventBase Event => @event;
        
        /// <inheritdoc />
        public override UnityEventBase Reaction => reaction;

        /// <summary>
        /// The event to listen to.
        /// </summary>
        [Tooltip("The event to listen to")]
        [SerializeField]
        [GenericDrawer]
        protected Event @event;

        /// <summary>
        /// The reaction that occurs when the event is raised.
        /// </summary>
        /// <remarks>
        /// Only raised if the Game Object this is attached to is active.
        /// </remarks>
        [Tooltip("The reaction that occurs when the event is raised")]
        [SerializeField]
        protected UnityReaction reaction;

        /// <summary>
        /// Unity editor hook for when the game object is enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
            @event.AddListener(Handle);
        }

        /// <summary>
        /// Handler that is invoked when the event is raised
        /// </summary>
        protected virtual void Handle()
        {
            reaction?.Invoke();
        }

        /// <summary>
        /// Unity editor hook for when the game object is disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
            @event.RemoveListener(Handle);
        }
    }

    /// <summary>
    /// Single argument event listener.
    /// </summary>
    /// <typeparam name="TArg0">first argument type</typeparam>
    public abstract class EventListener<TArg0> : EventListenerBase
    {
        /// <summary>
        /// A reaction for a single argument event.
        /// </summary>
        [Serializable]
        public class UnityReaction : UnityEvent<TArg0>
        {
        }

        /// <inheritdoc />
        public override EventBase Event => @event;
        
        /// <inheritdoc />
        public override UnityEventBase Reaction => reaction;

        /// <summary>
        /// The event to listen to.
        /// </summary>
        [Tooltip("The event to listen to")]
        [SerializeField]
        [GenericDrawer]
        protected Event<TArg0> @event;

        /// <summary>
        /// The reaction that occurs when the event is raised.
        /// </summary>
        /// <remarks>
        /// Only raised if the Game Object this is attached to is active.
        /// </remarks>
        [Tooltip("The reaction that occurs when the event is raised")]
        [SerializeField]
        protected UnityReaction reaction;

        /// <summary>
        /// Unity editor hook for when the game object is enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
            @event.AddListener(Handle);
        }

        /// <summary>
        /// Handler that is invoked when the event is raised
        /// </summary>
        /// <param name="arg0">first argument</param>
        protected virtual void Handle(TArg0 arg0)
        {
            reaction?.Invoke(arg0);
        }

        /// <summary>
        /// Unity editor hook for when the game object is disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
            @event.RemoveListener(Handle);
        }
    }
    
    /// <summary>
    /// Two argument event listener.
    /// </summary>
    /// <typeparam name="TArg0">first argument type</typeparam>
    /// <typeparam name="TArg1">second argument type</typeparam>
    public abstract class EventListener<TArg0, TArg1> : EventListenerBase
    {
        /// <summary>
        /// A reaction for a dual argument event.
        /// </summary>
        [Serializable]
        public class UnityReaction : UnityEvent<TArg0, TArg1>
        {
        }

        /// <inheritdoc />
        public override EventBase Event => @event;
        
        /// <inheritdoc />
        public override UnityEventBase Reaction => reaction;

        /// <summary>
        /// The event to listen to.
        /// </summary>
        [Tooltip("The event to listen to")]
        [SerializeField]
        [GenericDrawer]
        protected Event<TArg0, TArg1> @event;

        /// <summary>
        /// The reaction that occurs when the event is raised.
        /// </summary>
        /// <remarks>
        /// Only raised if the Game Object this is attached to is active.
        /// </remarks>
        [Tooltip("The reaction that occurs when the event is raised")]
        [SerializeField]
        protected UnityReaction reaction;

        /// <summary>
        /// Unity editor hook for when the game object is enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
            @event.AddListener(Handle);
        }

        /// <summary>
        /// Handler that is invoked when the event is raised
        /// </summary>
        /// <param name="arg0">first argument</param>
        /// <param name="arg1">second argument</param>
        protected virtual void Handle(TArg0 arg0, TArg1 arg1)
        {
            reaction?.Invoke(arg0, arg1);
        }

        /// <summary>
        /// Unity editor hook for when the game object is disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
            @event.RemoveListener(Handle);
        }
    }
    
    /// <summary>
    /// Three argument event listener.
    /// </summary>
    /// <typeparam name="TArg0">first argument type</typeparam>
    /// <typeparam name="TArg1">second argument type</typeparam>
    /// <typeparam name="TArg2">third argument type</typeparam>
    public abstract class EventListener<TArg0, TArg1, TArg2> : EventListenerBase
    {
        /// <summary>
        /// A reaction for a three argument event.
        /// </summary>
        [Serializable]
        public class UnityReaction : UnityEvent<TArg0, TArg1, TArg2>
        {
        }

        /// <inheritdoc />
        public override EventBase Event => @event;
        
        /// <inheritdoc />
        public override UnityEventBase Reaction => reaction;

        /// <summary>
        /// The event to listen to.
        /// </summary>
        [Tooltip("The event to listen to")]
        [SerializeField]
        [GenericDrawer]
        protected Event<TArg0, TArg1, TArg2> @event;

        /// <summary>
        /// The reaction that occurs when the event is raised.
        /// </summary>
        /// <remarks>
        /// Only raised if the Game Object this is attached to is active.
        /// </remarks>
        [Tooltip("The reaction that occurs when the event is raised")]
        [SerializeField]
        protected UnityReaction reaction;

        /// <summary>
        /// Unity editor hook for when the game object is enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
            @event.AddListener(Handle);
        }

        /// <summary>
        /// Handler that is invoked when the event is raised
        /// </summary>
        /// <param name="arg0">first argument</param>
        /// <param name="arg1">second argument</param>
        /// <param name="arg2">third argument</param>
        protected virtual void Handle(TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            reaction?.Invoke(arg0, arg1, arg2);
        }

        /// <summary>
        /// Unity editor hook for when the game object is disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
            @event.RemoveListener(Handle);
        }
    }
    
    /// <summary>
    /// Four argument event listener.
    /// </summary>
    /// <typeparam name="TArg0">first argument type</typeparam>
    /// <typeparam name="TArg1">second argument type</typeparam>
    /// <typeparam name="TArg2">third argument type</typeparam>
    /// <typeparam name="TArg3">fourth argument type</typeparam>
    public abstract class EventListener<TArg0, TArg1, TArg2, TArg3> : EventListenerBase
    {
        /// <summary>
        /// A reaction for a four argument event.
        /// </summary>
        [Serializable]
        public class UnityReaction : UnityEvent<TArg0, TArg1, TArg2, TArg3>
        {
        }

        /// <inheritdoc />
        public override EventBase Event => @event;
        
        /// <inheritdoc />
        public override UnityEventBase Reaction => reaction;

        /// <summary>
        /// The event to listen to.
        /// </summary>
        [Tooltip("The event to listen to")]
        [SerializeField]
        [GenericDrawer]
        protected Event<TArg0, TArg1, TArg2, TArg3> @event;

        /// <summary>
        /// The reaction that occurs when the event is raised.
        /// </summary>
        /// <remarks>
        /// Only raised if the Game Object this is attached to is active.
        /// </remarks>
        [Tooltip("The reaction that occurs when the event is raised")]
        [SerializeField]
        protected UnityReaction reaction;

        /// <summary>
        /// Unity editor hook for when the game object is enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
            @event.AddListener(Handle);
        }

        /// <summary>
        /// Handler that is invoked when the event is raised
        /// </summary>
        /// <param name="arg0">first argument</param>
        /// <param name="arg1">second argument</param>
        /// <param name="arg2">third argument</param>
        /// <param name="arg3">fourth argument</param>
        protected virtual void Handle(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            reaction?.Invoke(arg0, arg1, arg2, arg3);
        }

        /// <summary>
        /// Unity editor hook for when the game object is disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
            @event.RemoveListener(Handle);
        }
    }
}