using FasterGames.Whiskey.Boxes;
using NUnit.Framework;
using UnityEngine;

namespace FasterGames.Whiskey.Editor.Tests
{
    public class WhiskeyBoxTests
    {
        /// <summary>
        /// A custom box for testing
        /// </summary>
        class CustomFloatBox : Box<float>
        {
            /// <summary>
            /// Sets the inner box value directly
            /// </summary>
            /// <param name="value">value to set</param>
            /// <returns>the box; for chaining</returns>
            public CustomFloatBox With(float value)
            {
                this.data = value;

                return this;
            }
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void CustomBox_Comparisons()
        {
            var first = ScriptableObject.CreateInstance<CustomFloatBox>().With(10f);
            var second = ScriptableObject.CreateInstance<CustomFloatBox>().With(10f);
            
            // boxes aren't equal to each other if they aren't the same instance
            Assert.AreNotEqual(first, second);
            
            Assert.False(first.Equals(second));
            Assert.False(first.Equals(1f));
            
            // but the same instance is equal, of course
            Assert.AreEqual(first, first);

            // and a value comparison will check against the box contents
            Assert.True(first.ValueEquals(10f));
            
            // even if it's another box
            Assert.True(first.ValueEquals(second));
        }

        [Test]
        public void BoxedInt_Comparisons()
        {
            var first = ScriptableObject.CreateInstance<BoxedInt>();
            first.Write(10);
            var second = ScriptableObject.CreateInstance<BoxedInt>();
            second.Write(10);
            
            // boxes aren't equal to each other if they aren't the same instance
            Assert.AreNotEqual(first, second);
            
            // but the same instance is equal, of course
            Assert.AreEqual(first, first);
            
            // and a value comparison will check against the box contents
            Assert.True(first.ValueEquals(10));
            
            // even if it's another box
            Assert.True(first.ValueEquals(second));
        }

        [Test]
        public void BoxedInt_ReadWrite()
        {
            var first = ScriptableObject.CreateInstance<BoxedInt>();
            
            // default(T) assigns value at start
            Assert.AreEqual(0, first.Read());
            
            first.Write(10);
            
            // writing works
            Assert.AreEqual(10, first.Read());
        }

        class CustomRef<T> : Ref<T>
        {
            public Selector getSelector() => selector;

            public void setSelector(Selector sel) => selector = sel;
            
            public Box<T> getBox() => box;

            public void setBox(Box<T> b) => box = b;
            
            public T getRaw() => raw;

            public T setRaw(T v) => raw = v;
        }
        
        [Test]
        public void Ref_Selection()
        {
            var accessor = new CustomRef<float>();
            
            // default selector is direct
            Assert.AreEqual(Ref<float>.Selector.Direct, accessor.getSelector());

            accessor.setRaw(10);
            
            // ensure rw works
            Assert.AreEqual(10, accessor.Value);
            
            // ensure we are writing to the correct place
            Assert.AreEqual(10, accessor.getRaw());

            // by default, the box is null
            Assert.Null(accessor.getBox());

            accessor.setSelector(Ref<float>.Selector.Boxed);

            var floatBox = ScriptableObject.CreateInstance<BoxedFloat>();
            accessor.setBox(floatBox);
            
            // ensure the default box values are reflected
            Assert.AreEqual(0, accessor.Value);
            Assert.AreEqual(0, accessor.getBox().Read());

            floatBox.Write(10);
            
            // ensure updated values are reflected
            Assert.AreEqual(10, accessor.Value);
            Assert.AreEqual(10, accessor.getBox().Read());
        }
        
        class CustomMutRef<T> : MutRef<T>
        {
            public Selector getSelector() => selector;

            public void setSelector(Selector sel) => selector = sel;
            
            public Box<T> getBox() => box;

            public void setBox(Box<T> b) => box = b;
            
            public T getRaw() => raw;
        }
        
        [Test]
        public void MutRef_Selection()
        {
            var accessor = new CustomMutRef<float>();
            
            // default selector is direct
            Assert.AreEqual(MutRef<float>.Selector.Direct, accessor.getSelector());

            accessor.Value = 10;
            
            // ensure rw works
            Assert.AreEqual(10, accessor.Value);
            
            // ensure we are writing to the correct place
            Assert.AreEqual(10, accessor.getRaw());

            // by default, the box is null
            Assert.Null(accessor.getBox());

            accessor.setSelector(MutRef<float>.Selector.Boxed);

            var floatBox = ScriptableObject.CreateInstance<BoxedFloat>();
            accessor.setBox(floatBox);
            
            // ensure the default box values are reflected
            Assert.AreEqual(0, accessor.Value);
            Assert.AreEqual(0, accessor.getBox().Read());

            accessor.Value = 10;
            
            // ensure updated values are reflected
            Assert.AreEqual(10, accessor.Value);
            Assert.AreEqual(10, accessor.getBox().Read());
        }

        [Test]
        public void Ref_Read()
        {
            var readableFloat = new Ref<float>();
            
            // read works via prop and method
            Assert.AreEqual(0f, readableFloat.Value);
            Assert.AreEqual(0f, readableFloat.Read());
        }

        [Test]
        public void Ref_Comparisons()
        {
            var first = new Ref<float>();
            var second = new Ref<float>();
            
            // refs are equal if the contents are equal
            Assert.AreEqual(first, second);
            
            // and the same instance is equal, of course
            Assert.AreEqual(first, first);
            
            // and a value comparison will check against the ref contents
            Assert.True(first.ValueEquals(0));
            
            // even if it's another ref
            Assert.True(first.ValueEquals(second));
        }

        [Test]
        public void MutRef_ReadWrite()
        {
            var rwFloat = new MutRef<float>();
            
            // default reads work
            Assert.AreEqual(0f, rwFloat.Value);
            Assert.AreEqual(0f, rwFloat.Read());

            rwFloat.Value = 10f;

            // write works via prop
            Assert.AreEqual(10f, rwFloat.Value);
            Assert.AreEqual(10f, rwFloat.Read());
            
            rwFloat.Write(20f);
            
            // write works via method
            Assert.AreEqual(20f, rwFloat.Value);
            Assert.AreEqual(20f, rwFloat.Read());
        }
        
        [Test]
        public void MutRef_Comparisons()
        {
            var first = new MutRef<float>();
            var second = new MutRef<float>();
            
            // TODO(bengreenier): should ensure that two refs pointing at different Boxes are not equal
            
            // mut refs are equal if the contents are the same
            Assert.AreEqual(first, second);
            
            // and the same instance is equal, of course
            Assert.AreEqual(first, first);
            
            // and a value comparison will check against the mut ref contents
            Assert.True(first.ValueEquals(0));
            
            // even if it's another mut ref
            Assert.True(first.ValueEquals(second));
        }
    }
}
