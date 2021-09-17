using System.Collections.Generic;
using Codice.CM.Common;

namespace FasterGames.Whiskey
{
    /// <summary>
    /// A value equality element.
    /// </summary>
    /// <typeparam name="T">value type</typeparam>
    public interface IValueEquals<in T>
    {
        /// <summary>
        /// Ensures an inner value matches another.
        /// </summary>
        /// <param name="other">other value</param>
        /// <returns>true if equal, false if not equal</returns>
        bool ValueEquals(T other);
    };
    
    /// <summary>
    /// A readable element.
    /// </summary>
    /// <typeparam name="T">element type</typeparam>
    public interface IReadable<out T>
    {
        /// <summary>
        /// Read the element.
        /// </summary>
        /// <returns>element data</returns>
        T Read();
    }

    /// <summary>
    /// A writable element.
    /// </summary>
    /// <typeparam name="T">element type</typeparam>
    public interface IWritable<in T>
    {
        /// <summary>
        /// Write to the element.
        /// </summary>
        /// <param name="value">element data</param>
        void Write(T value);
    }

    /// <summary>
    /// An rng element.
    /// </summary>
    public interface IRng
    {
        /// <summary>
        /// Generates a random number within a range.
        /// </summary>
        /// <param name="minInclusive">min inclusive number</param>
        /// <param name="maxInclusive">max inclusive number</param>
        /// <returns>random number</returns>
        float Range(float minInclusive, float maxInclusive);
    }
}