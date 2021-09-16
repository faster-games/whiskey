using System.Collections.Generic;
using UnityEngine;

namespace FasterGames.Whiskey.T4Host
{
    /// <summary>
    /// Configuration for whiskey generation.
    /// </summary>
    [CreateAssetMenu(menuName = "Whiskey/Config")]
    public class WhiskeyConfig : ScriptableObject
    {
        /// <summary>
        /// Config for boxes.
        /// </summary>
        [Tooltip("Configures the Whiskey Box system")]
        public List<BoxConfig> boxConfig;
        
        /// <summary>
        /// Config for events.
        /// </summary>
        [Tooltip("Configures the Whiskey Event system")]
        public List<EventConfig> eventConfig;
        
        /// <summary>
        /// Config for tables.
        /// </summary>
        [Tooltip("Configures the Whiskey Table system")]
        public List<TableConfig> tableConfig;

        /// <summary>
        /// Flag; If false, overrides default whiskey configuration.
        /// Otherwise, extends default whiskey configuration.
        /// </summary>
        [Tooltip("Flag; If false, overrides default whiskey configuration.")]
        public bool extendsDefaultSet = true;
    }
}