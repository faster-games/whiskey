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
            
            // but the same instance is equal, of course
            Assert.AreEqual(first, first);
            
            // and a value comparison will check against the box contents
            Assert.AreEqual(10f, first);
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
            Assert.AreEqual(10, first);
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

        [Test]
        public void RefAccessor_Selection()
        {
            var accessor = new RefAccessor<float>();
            
            // default selector is raw
            Assert.AreEqual(RefAccessor<float>.Selector.Raw, accessor.selector);

            accessor.Value = 10;
            
            // ensure rw works
            Assert.AreEqual(10, accessor.Value);
            
            // ensure we are writing to the correct place
            Assert.AreEqual(10, accessor.raw);

            // by default, the box is null
            Assert.Null(accessor.boxed);
            
            accessor.selector = RefAccessor<float>.Selector.Boxed;

            var floatBox = ScriptableObject.CreateInstance<BoxedFloat>();
            accessor.boxed = floatBox;
            
            // ensure the default box values are reflected
            Assert.AreEqual(0, accessor.Value);
            Assert.AreEqual(0, accessor.boxed.Read());

            accessor.Value = 10;
            
            // ensure updated values are reflected
            Assert.AreEqual(10, accessor.Value);
            Assert.AreEqual(10, accessor.boxed.Read());
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
    }
}
