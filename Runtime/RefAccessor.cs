using System;

namespace FasterGames.Whiskey
{
    /// <summary>
    /// Defines a conditional accessor for ref {T} manipulation.
    /// </summary>
    /// <remarks>
    /// As a consumer of Whiskey, you should not need to directly access instances of this class.
    /// See <see cref="Ref{T}"/> and <see cref="MutRef{T}"/> instead.
    /// </remarks>
    /// <typeparam name="T">ref type</typeparam>
    [Serializable]
    public class RefAccessor<T>
    {
        /// <summary>
        /// The selector determines which data type will be accessed.
        /// </summary>
        public enum Selector
        {
            /// <summary>
            /// Raw data access - a constant
            /// </summary>
            Raw = 1,
            
            /// <summary>
            /// Boxed data access - some <see cref="Box{T}"/>
            /// </summary>
            Boxed
        }

        /// <summary>
        /// The data type to store and access
        /// </summary>
        public Selector selector = Selector.Raw;
        
        /// <summary>
        /// Storage for raw data
        /// </summary>
        public T raw;
        
        /// <summary>
        /// Storage for boxed data
        /// </summary>
        public Box<T> boxed;

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
                    case Selector.Raw:
                        return raw;
                    case Selector.Boxed:
                        return boxed.Read();
                    default:
                        throw new NotImplementedException();
                }
            }

            set
            {
                switch (selector)
                {
                    case Selector.Raw:
                        raw = value;
                        break;
                    case Selector.Boxed:
                        boxed.Write(value);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}