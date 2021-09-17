using System;
using FasterGames.Whiskey.Events;
using NUnit.Framework;
using UnityEngine;
using Event = FasterGames.Whiskey.Events.Event;
using UnityAction = UnityEngine.Events.UnityAction;

namespace FasterGames.Whiskey.Editor.Tests
{
    public class WhiskeyEventTests
    {
        public class VoidEvent : Event
        {
            
        }

        public class VoidEventListener : EventListener
        {
            public void Setup(VoidEvent evt, UnityAction action)
            {
                @event = evt;
                reaction = new UnityReaction();
                reaction.AddListener(action);
                
                OnEnable();
            }

            public void Teardown()
            {
                OnDisable();
            }
        }
        
        [Test]
        public void VoidEvent_Raises()
        {
            var evt = ScriptableObject.CreateInstance<VoidEvent>();

            var count = 0;
            Action handler = () =>
            {
                count++;
            };
            
            evt.AddListener(handler);
            
            evt.Raise();
            Assert.AreEqual(1, count);
            Assert.AreEqual(1, evt.ListenerCount);
            evt.Raise();
            Assert.AreEqual(2, count);
            
            evt.RemoveListener(handler);
            evt.Raise();
            Assert.AreEqual(0, evt.ListenerCount);
            Assert.AreEqual(2, count);
        }
        
        [Test]
        public void FloatEvent_Raises()
        {
            var evt = ScriptableObject.CreateInstance<FloatEvent>();

            var count = 0;
            Action<float> handler = (float f) =>
            {
                count++;
            };
            
            evt.AddListener(handler);
            
            evt.Raise(1f);
            Assert.AreEqual(1, count);
            Assert.AreEqual(1, evt.ListenerCount);
            evt.Raise(1f);
            Assert.AreEqual(2, count);
            
            evt.RemoveListener(handler);
            evt.Raise(1f);
            Assert.AreEqual(0, evt.ListenerCount);
            Assert.AreEqual(2, count);
        }

        [Test]
        public void EventListener_Success()
        {
            var evt = ScriptableObject.CreateInstance<VoidEvent>();

            var go = new GameObject();
            var listener = go.AddComponent<VoidEventListener>();
            
            // since listener is an editor construct, it isn't designed to be allocated via code
            // so we force it with a subclass that calls engine hooks manually.
            // in a production game, you'd want to just reference the event directly.

            var count = 0;
            listener.Setup(evt, () =>
            {
                count++;
            });
            
            evt.Raise();
            Assert.AreEqual(1, count);
            evt.Raise();
            Assert.AreEqual(2, count);
            listener.Teardown();
            evt.Raise();
            Assert.AreEqual(2, count);
        }
    }
}