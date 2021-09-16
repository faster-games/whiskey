namespace FasterGames.Whiskey
{
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
}