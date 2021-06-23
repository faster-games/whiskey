using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using FasterGames.Whiskey;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    /// <summary>
    /// Unit tests for Whiskey
    /// </summary>
    /// <remarks>
    /// Even though it's weird, we test internals (e.g. non public stuff) because the editor scripts rely on them
    /// </remarks>
    public class UnitTests
    {
        [Test]
        public void Derivation()
        {
            var mock = new GameObject("mock");
            var listener = mock.AddComponent<StringListener>();

            Assert.IsInstanceOf<MonoBehaviour>(listener);
            Assert.IsInstanceOf<ScriptableObject>(ScriptableObject.CreateInstance<StringEvent>());
        }

        [Test]
        public void Trigger_Success()
        {
            var mock = new GameObject("mock");
            var listener = mock.AddComponent<StringListener>();

            var evt = ScriptableObject.CreateInstance<StringEvent>();
            evt.Subscribe(listener);

            string expectedData = "hello world";
            string actualData = null;
            listener.Reaction.AddListener((string data) =>
            {
                actualData = data;
            });

            evt.Trigger(expectedData);

            Assert.AreEqual(expectedData, actualData);
        }

        [Test]
        public void AutoSubscribe_Success()
        {
            var mock = new GameObject("mock");
            var listener = mock.AddComponent<StringListener>();
            var evt = ScriptableObject.CreateInstance<StringEvent>();
            
            listener.Event = evt;

            // we reflect to get these, just like the UnityRuntime would for us.
            // a caller should never need to do this, but we want to force unity runtime hooks for testing.
            var OnEnable = typeof(StringListener).GetMethod("OnEnable", BindingFlags.Instance | BindingFlags.NonPublic);
            var OnDisable = typeof(StringListener).GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic);

            string expectedData = "hello world";
            List<string> actualData = new List<string>();
            listener.Reaction.AddListener((string data) =>
            {
                actualData.Add(data);
            });

            OnEnable.Invoke(listener, new object[0]);

            evt.Trigger(expectedData);

            OnDisable.Invoke(listener, new object[0]);

            evt.Trigger(expectedData);

            Assert.AreEqual(1, actualData.Count);
            Assert.AreEqual(expectedData, actualData[0]);
        }

        [Test]
        public void Object_ImmutableString_Success()
        {
            var immutableString = ScriptableObject.CreateInstance<ImmutableString>();
            
            var backingData = typeof(ImmutableString).GetField("value", BindingFlags.Instance | BindingFlags.NonPublic);
            
            Assert.AreEqual(default(string), immutableString.ReadOnlyValue);

            var expectedData = "hello, world";
            backingData.SetValue(immutableString, expectedData);
            
            Assert.AreEqual(expectedData, immutableString.ReadOnlyValue);
        }

        [Test]
        public void Object_MutableString_Success()
        {
            var mutableString = ScriptableObject.CreateInstance<MutableString>();
            
            Assert.AreEqual(default(string), mutableString.ReadOnlyValue);
            Assert.AreEqual(default(string), mutableString.Value);

            var expectedData = "hello, again";
            mutableString.Value = expectedData;
            
            Assert.AreEqual(expectedData, mutableString.ReadOnlyValue);
            Assert.AreEqual(expectedData, mutableString.Value);
        }

        [Test]
        public void Object_MutableStringTable_Success()
        {
            var immutableStringTable = ScriptableObject.CreateInstance<ImmutableStringTable>();
            
            Assert.AreEqual(default(string), immutableStringTable.ReadOnlyValue);
            
            immutableStringTable.TableEntries.Add(new ImmutableDataTable<string>.ImmutableObjectTableEntry(){Value = "one", Weight = 1});
            immutableStringTable.TableEntries.Add(new ImmutableDataTable<string>.ImmutableObjectTableEntry(){Value = "two", Weight = 1});

            var chanceField = typeof(ImmutableDataTable<string>.ImmutableObjectTableEntry).GetField("Chance",
                BindingFlags.Instance | BindingFlags.NonPublic);

            var nextValue = immutableStringTable.ReadOnlyValue;

            if (nextValue != "one" && nextValue != "two")
            {
                Assert.Fail(nextValue);
            }

            var first = immutableStringTable.TableEntries[0];
            var second = immutableStringTable.TableEntries[1];

            Assert.AreEqual(50.0f, chanceField.GetValue(first));
            Assert.AreEqual(50.0f, chanceField.GetValue(second));
        }

        public class RefTestBehavior : MonoBehaviour
        {
            public RefImmutableString immutableStringRef = new RefImmutableString();
            public RefMutableString mutableStringRef = new RefMutableString();
        }

        [Test]
        public void Object_RefTests_Success()
        {
            var testObject = new GameObject("testObject");
            var testBehavior = testObject.AddComponent<RefTestBehavior>();

            var immutableUseConstantField =
                typeof(RefImmutableString).GetField("UseConstant", BindingFlags.Instance | BindingFlags.NonPublic);
            var mutableUseConstantField =
                typeof(RefMutableString).GetField("UseConstant", BindingFlags.Instance | BindingFlags.NonPublic);
            var immutableConstantField =
                typeof(RefImmutableString).GetField("Constant", BindingFlags.Instance | BindingFlags.NonPublic);

            
            Assert.AreEqual(true,immutableUseConstantField.GetValue(testBehavior.immutableStringRef));
            Assert.AreEqual(true,mutableUseConstantField.GetValue(testBehavior.mutableStringRef));

            var expectedData = "testValue";
            immutableConstantField.SetValue(testBehavior.immutableStringRef, expectedData);
            testBehavior.mutableStringRef.Value = expectedData;
            
            Assert.AreEqual(expectedData, testBehavior.immutableStringRef.ReadOnlyValue);
            Assert.AreEqual(expectedData, testBehavior.mutableStringRef.ReadOnlyValue);
            Assert.AreEqual(expectedData, testBehavior.mutableStringRef.Value);
            
            immutableUseConstantField.SetValue(testBehavior.immutableStringRef, false);
            mutableUseConstantField.SetValue(testBehavior.mutableStringRef, false);

            // no object set, can't access value of nothing
            Assert.Catch(typeof(NullReferenceException), () =>
            {
                testBehavior.mutableStringRef.Value = "throw";
            });

            var mutableString = ScriptableObject.CreateInstance<MutableString>();
            mutableString.Value = expectedData;

            var immutableString = ScriptableObject.CreateInstance<ImmutableString>();
            
            var immutableStringBackingData = typeof(ImmutableString).GetField("value", BindingFlags.Instance | BindingFlags.NonPublic);

            immutableStringBackingData.SetValue(immutableString, expectedData);
            
            var mutableObjectField =
                typeof(RefMutableString).GetField("Object", BindingFlags.Instance | BindingFlags.NonPublic);
            var immutableObjectField =
                typeof(RefImmutableString).GetField("Object", BindingFlags.Instance | BindingFlags.NonPublic);
            
            mutableObjectField.SetValue(testBehavior.mutableStringRef, mutableString);
            immutableObjectField.SetValue(testBehavior.immutableStringRef, immutableString);
            
            Assert.AreEqual(expectedData, testBehavior.immutableStringRef.ReadOnlyValue);
            Assert.AreEqual(expectedData, testBehavior.mutableStringRef.ReadOnlyValue);
            Assert.AreEqual(expectedData, testBehavior.mutableStringRef.Value);
        }

        [Test]
        public void Sets_Success()
        {
            var set = ScriptableObject.CreateInstance<GameObjectSet>();
            
            Assert.AreEqual(0, set.Count);
            
            var one = new GameObject("one");
            var setElemOne = one.AddComponent<GameObjectSetElement>();
            setElemOne.ParentSet = set;

            var two = new GameObject("two");
            var setElemTwo = two.AddComponent<GameObjectSetElement>();
            setElemTwo.ParentSet = set;
            
            // we cheat to enable these
            var OnEnabledMethod =
                typeof(GameObjectSetElement).GetMethod("OnEnable", BindingFlags.Instance | BindingFlags.NonPublic);

            OnEnabledMethod.Invoke(setElemOne, new object[0]);
            OnEnabledMethod.Invoke(setElemTwo, new object[0]);
            
            Assert.AreEqual(2, set.Count);
            
            // we cheat to enable these
            var OnDisabledMethod =
                typeof(GameObjectSetElement).GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic);

            OnDisabledMethod.Invoke(setElemOne, new object[0]);
            OnDisabledMethod.Invoke(setElemTwo, new object[0]);
            
            Assert.AreEqual(0, set.Count);
        }
    }
}
