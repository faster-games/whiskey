
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
    /// A reference to an immutable float, either via a constant or <see cref="ImmutableFloat"/>
    /// </summary>
    [Serializable]
    public class RefImmutableFloat : RefImmutable<float>
    {
        /// <summary>
        /// Raw operator
        /// </summary>
        /// <remarks>
        /// Allows us to treat the instance as if it's TRaw type for data access
        /// </remarks>
        /// <param name="reference">the reference to operate on</param>
        public static implicit operator float(RefImmutableFloat reference)
        {
            return reference.ReadOnlyValue;
        }
    }

}
