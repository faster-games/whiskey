
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
    /// A float that can be changed at runtime
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Whiskey/Objects/MutableFloat")]
    public class MutableFloat : MutableObject<float>
    {
        [SerializeField]
        protected float value;
        
        public override float ReadOnlyValue
        {
            get => value;
        }

        public override float Value
        {
            get => value;
            set => this.value = value;
        }
    }

}