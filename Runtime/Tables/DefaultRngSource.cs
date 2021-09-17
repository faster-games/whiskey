using UnityEngine;

namespace FasterGames.Whiskey.Tables
{
    /// <summary>
    /// An rng source that uses <see cref="UnityEngine.Random"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "Whiskey/Default Rng")]
    public class DefaultRngSource : RngSource
    {
        /// <summary>
        /// The seed to use.
        /// </summary>
        [Tooltip("The seed to use")]
        public int seed;

        /// <summary>
        /// Internal flag for lazy init
        /// </summary>
        private bool m_IsDirty = true;
        
        /// <inheritdoc />
        public override float Range(float minInclusive, float maxInclusive)
        {
            if (m_IsDirty)
            {
                Random.InitState(seed);
                m_IsDirty = false;
            }
            
            return Random.Range(minInclusive, maxInclusive);
        }
    }
}