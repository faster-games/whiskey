using System;
using System.Collections.Generic;
using FasterGames.Whiskey.PropertyAttributes;
using FasterGames.Whiskey.Tables;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FasterGames.Whiskey.Boxes
{
    /// <summary>
    /// A <see cref="IReadable{T}"/> reference to some data of type {T}.
    /// </summary>
    /// <remarks>
    /// The data itself, may be direct (e.g. a constant) or indirect (e.g. inside a <see cref="Box{T}"/>)
    /// </remarks>
    /// <typeparam name="T">the referenced data type</typeparam>
    [Serializable]
    public class Ref<T> : IEquatable<Ref<T>>, IReadable<T>, IValueEquals<Ref<T>>, IValueEquals<T>
    {
        public static class FieldNames
        {
            public static readonly string selector = nameof(Ref<Object>.selector);
            public static readonly string box = nameof(Ref<Object>.box);
            public static readonly string raw = nameof(Ref<Object>.raw);
            public static readonly string table = nameof(Ref<Object>.table);
        }

        /// <summary>
        /// The selector determines which data type will be accessed.
        /// </summary>
        public enum Selector
        {
            /// <summary>
            /// Provides access directly - stores the value internally
            /// </summary>
            Direct,
            
            /// <summary>
            /// Provides access indirectly - stores reference to a box internally
            /// </summary>
            Boxed,
            
            /// <summary>
            /// Provides access by sampling a table - stores reference to probability table internally
            /// </summary>
            Table
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Ref()
        {
            
        }

        /// <summary>
        /// Value ctor
        /// </summary>
        /// <param name="raw">raw value to store</param>
        public Ref(T raw)
        {
            this.raw = raw;
            this.selector = Selector.Direct;
        }

        /// <summary>
        /// Value ctor
        /// </summary>
        /// <param name="box">box value to store</param>
        public Ref(Box<T> box)
        {
            this.box = box;
            this.selector = Selector.Boxed;
        }

        /// <summary>
        /// The data type to store and access
        /// </summary>
        [SerializeField]
        protected Selector selector;
        
        /// <summary>
        /// Storage for raw data
        /// </summary>
        [SerializeField]
        protected T raw;
        
        /// <summary>
        /// Storage for boxed data
        /// </summary>
        [SerializeField]
        [GenericDrawer(ignoredNamespaces: new []{"FasterGames.Whiskey.Editor.Tests"})]
        protected Box<T> box;

        /// <summary>
        /// Storage for the table
        /// </summary>
        [SerializeField]
        [GenericDrawer(ignoredNamespaces: new []{"FasterGames.Whiskey.Editor.Tests"})]
        protected Table<T> table;
        
        /// <summary>
        /// Element accessor
        /// </summary>
        /// <remarks>
        /// If the particular operation isn't supported (e.g. read-only) will throw
        /// </remarks>
        /// <exception cref="NotImplementedException">Invalid configuration - missing <see cref="Selector"/> value (development error).</exception>
        public T Value
        {
            get
            {
                switch (selector)
                {
                    case Selector.Direct:
                        return raw;
                    case Selector.Boxed:
                        return box.Read();
                    case Selector.Table:
                        return table.Read();
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <inheritdoc />
        public T Read()
        {
            return Value;
        }
        
        /// <inheritdoc />
        public bool ValueEquals(Ref<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        /// <inheritdoc />
        public bool ValueEquals(T other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }

        /// <inheritdoc />
        public bool Equals(Ref<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return selector == other.selector && EqualityComparer<T>.Default.Equals(raw, other.raw) && Equals(box, other.box);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ref<T>) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                // auto-generated by resharper 
                var hashCode = (int) selector;
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(raw);
                hashCode = (hashCode * 397) ^ (box != null ? box.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}