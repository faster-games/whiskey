using UnityEngine;

namespace FasterGames.Whiskey.PropertyAttributes
{
    /// <summary>
    /// A property attribute for better editor generic types
    /// </summary>
    /// <remarks>
    /// A property tagged with this attribute will be reflected over in the editor
    /// and a selector of derived implementations for the generic will be shown
    /// This allows the object inspector to better assist selecting objects. 
    /// </remarks>
    public class GenericDrawerAttribute : PropertyAttribute
    {
        /// <summary>
        /// Flag; Indicates if the drawer should cache it's type list for subsequent draws inside the same inspector.
        /// </summary>
        /// <remarks>
        /// If you aren't sure what this is, you probably want it on to improve editor performance
        /// </remarks>
        public bool CacheTypes { get; private set; }
        
        /// <summary>
        /// Set of namespaces from which generics will be ignored.
        /// </summary>
        /// <remarks>
        /// Use this to exclude test types from the editor dropdowns.
        /// </remarks>
        public string[] IgnoredNamespaces { get; private set; }
        
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="cacheTypes"><see cref="CacheTypes"/></param>
        /// <param name="ignoredNamespaces"><see cref="IgnoredNamespaces"/></param>
        public GenericDrawerAttribute(bool cacheTypes = true, string[] ignoredNamespaces = null)
        {
            CacheTypes = cacheTypes;
            IgnoredNamespaces = ignoredNamespaces ?? new string[0];
        }
    }
}