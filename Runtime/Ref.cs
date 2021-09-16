using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FasterGames.Whiskey
{
    /// <summary>
    /// A <see cref="IReadable{T}"/> reference to some data of type {T}.
    /// </summary>
    /// <remarks>
    /// The data itself, may be direct (e.g. a constant) or indirect (e.g. inside a <see cref="Box{T}"/>)
    /// </remarks>
    /// <typeparam name="T">the referenced data type</typeparam>
    [Serializable]
    public class Ref<T> : IReadable<T>
    {
        public static class FieldNames
        {
            public static readonly string accessor = nameof(Ref<Object>.accessor);
        }
        
        /// <summary>
        /// The referenced value
        /// </summary>
        public T Value => accessor.Value;

        /// <summary>
        /// Internal accessor for ref
        /// </summary>
        [SerializeField]
        protected RefAccessor<T> accessor = new RefAccessor<T>();
        
        /// <inheritdoc />
        public T Read()
        {
            return accessor.Value;
        }
    }
}