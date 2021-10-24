
// <auto-generated>
// This code was generated by a tool. Any changes made manually will be lost
// the next time this code is regenerated.
// </auto-generated>

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FasterGames.Whiskey
{

    /// <summary>
    /// A int that can be changed at runtime
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Whiskey/Objects/MutableInt")]
    public class MutableInt : MutableObject<int>
    {
        [SerializeField]
        protected int value;
        
        public override int ReadOnlyValue
        {
            get => value;
        }

        public override int Value
        {
            get => value;
            set => this.value = value;
        }
    }

}