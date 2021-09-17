using UnityEngine;

namespace FasterGames.Whiskey.Tables
{
    /// <summary>
    /// Random number generator source, as a scriptable object.
    /// </summary>
    public abstract class RngSource : ScriptableObject, IRng
    {
        /// <inheritdoc />
        public abstract float Range(float minInclusive, float maxInclusive);
    }
}