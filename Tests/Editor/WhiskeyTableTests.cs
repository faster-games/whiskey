using FasterGames.Whiskey.Boxes;
using FasterGames.Whiskey.Tables;
using NUnit.Framework;
using UnityEngine;

namespace FasterGames.Whiskey.Editor.Tests
{
    public class WhiskeyTableTests
    {
        [Test]
        public void FloatTable_Direct()
        {
            var rng = ScriptableObject.CreateInstance<DefaultRngSource>();
            var ft = ScriptableObject.CreateInstance<FloatTable>();
            ft.random = rng;
            
            Assert.AreEqual(0, ft.Entries.Count);
            
            ft.Add(1f, 1f);
            Assert.AreEqual(1, ft.TotalWeightInTable);
            Assert.AreEqual(1, ft.Entries.Count);
            Assert.AreEqual(1f, ft.Read());
            ft.Add(2f, 1f);
            Assert.AreEqual(2, ft.TotalWeightInTable);
        }
        
        [Test]
        public void FloatTable_Boxed()
        {
            var rng = ScriptableObject.CreateInstance<DefaultRngSource>();
            var ft = ScriptableObject.CreateInstance<FloatTable>();
            ft.random = rng;
            
            Assert.AreEqual(0, ft.Entries.Count);

            var box = ScriptableObject.CreateInstance<BoxedFloat>();
            box.Write(10f);
            var boxRef = new Ref<float>(box);
            
            ft.Add(boxRef, 1f);
            Assert.AreEqual(1, ft.TotalWeightInTable);
            Assert.AreEqual(1, ft.Entries.Count);
            Assert.AreEqual(10f, ft.Read());
            ft.Add(2f, 1f);
            Assert.AreEqual(2, ft.TotalWeightInTable);
        }
    }
}
