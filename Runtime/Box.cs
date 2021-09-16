using System;
using System.Collections.Generic;
using UnityEngine;

namespace FasterGames.Whiskey
{
    /// <summary>
    /// A boxed data element that is readable, writable, equatable, and can be persisted as a scriptable object.
    /// </summary>
    /// <typeparam name="T">element type</typeparam>
    public abstract class Box<T> : ScriptableObject, IEquatable<Box<T>>, IEquatable<T>, IReadable<T>, IWritable<T>
    {
        /// <summary>
        /// The data
        /// </summary>
        [SerializeField]
        protected T data;

        /// <inheritdoc />
        public bool Equals(Box<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && EqualityComparer<T>.Default.Equals(data, other.data);
        }

        /// <inheritdoc />
        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(data, other) || EqualityComparer<T>.Default.Equals(data, other);
        }
        
        /// <inheritdoc />
        public T Read()
        {
            return data;
        }

        /// <inheritdoc />
        public void Write(T value)
        {
            data = value;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Box<T>) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                // autogenerated by re-sharper
                return (base.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(data);
            }
        }
    }
}