using System;
using System.Collections.Generic;
using UnityEngine;

namespace FasterGames.Whiskey.T4Host
{
    /// <summary>
    /// Configuration object for a type.
    /// </summary>
    [Serializable]
    public class TypeConfig
    {
        /// <summary>
        /// The type name
        /// </summary>
        [Tooltip("The type name")]
        public string name;
        
        /// <summary>
        /// The type qualifier
        /// </summary>
        /// <remarks>
        /// For example: `UnityEngine` qualifies `Vector3`
        /// </remarks>
        [Tooltip("The type qualifier")]
        public string qualifier;
    }
    
    /// <summary>
    /// Configuration for the whiskey <see cref="FasterGames.Whiskey.Box{T}"/> system.
    /// </summary>
    [Serializable]
    public class BoxConfig
    {
        /// <summary>
        /// The type to generate a <see cref="FasterGames.Whiskey.Box{T}"/> for.
        /// </summary>
        [Tooltip("The type to generate a box for")]
        public TypeConfig type;
        
        /// <summary>
        /// The asset menu prefix to use for the box.
        /// </summary>
        [Tooltip("The asset menu prefix to use for the box")]
        public string assetMenuNamePrefix;
        
        /// <summary>
        /// The asset menu name to use for the box.
        /// </summary>
        /// <remarks>
        /// This is always appended to <see cref="assetMenuNamePrefix"/>
        /// </remarks>
        [Tooltip("The asset menu name to use for the box")]
        public string assetMenuName;
    }
    
    /// <summary>
    /// Configuration for the whiskey event system.
    /// </summary>
    [Serializable]
    public class EventConfig
    {
        /// <summary>
        /// The event name.
        /// </summary>
        [Tooltip("The event name")]
        public string name;
        
        /// <summary>
        /// The arguments to generate an event for.
        /// </summary>
        [Tooltip("The arguments to generate an event for")]
        public List<TypeConfig> argTypes;
        
        /// <summary>
        /// Flag; Indicates if a <see cref="UnityEngine.MonoBehaviour"/> will be generated with a <see cref="UnityEngine.Events.UnityEvent"/> to handle the event.
        /// </summary>
        [Tooltip("Flag; Indicates if a MonoBehaviour will be generated with a UnityEvent to handle this event type")]
        public bool generateBehaviourListener = false;
        
        /// <summary>
        /// The asset menu prefix to use for the box.
        /// </summary>
        [Tooltip("The asset menu prefix to use for the box")]
        public string assetMenuNamePrefix;
        
        /// <summary>
        /// The asset menu name to use for the box.
        /// </summary>
        /// <remarks>
        /// This is always appended to <see cref="assetMenuNamePrefix"/>
        /// </remarks>
        [Tooltip("The asset menu name to use for the box")]
        public string assetMenuName;
    }

    /// <summary>
    /// Configuration for the whiskey table system.
    /// </summary>
    [Serializable]
    public class TableConfig
    {
        /// <summary>
        /// The type to generate a <see cref="FasterGames.Whiskey.Table{T}"/> for.
        /// </summary>
        [Tooltip("The type to generate a Table for")]
        public TypeConfig type;
        
        /// <summary>
        /// The asset menu prefix to use for the box.
        /// </summary>
        [Tooltip("The asset menu prefix to use for the box")]
        public string assetMenuNamePrefix;
        
        /// <summary>
        /// The asset menu name to use for the box.
        /// </summary>
        /// <remarks>
        /// This is always appended to <see cref="assetMenuNamePrefix"/>
        /// </remarks>
        [Tooltip("The asset menu name to use for the box")]
        public string assetMenuName;
    }
}