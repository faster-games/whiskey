using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FasterGames.Whiskey.PropertyAttributes;
using UnityEditor;
using UnityEngine;

namespace FasterGames.Whiskey.Editor
{
    /// <summary>
    /// Property drawer for generic object selection assistance.
    /// </summary>
    /// <remarks>
    /// Provides a better generics experience for editor selection of derived types. 
    /// </remarks>
    [CustomPropertyDrawer(typeof(GenericDrawerAttribute))]
    public class GenericObjectPropertyDrawer : PropertyDrawer
    {
        private Type m_RefType;
        private List<Type> m_ChildTypes;
        private string[] m_TypeOptions;
        private int m_TypeOptionsIndex = 0;
        private Type m_TypeFilter;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // parse the attribute
            var attr = (GenericDrawerAttribute) attribute;

            // draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var srcIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            var third = position.width / 3;
            var dropdownRect = new Rect(position.x, position.y, third * 1, position.height);
            var valueRect = new Rect(position.x + (third * 1), position.y, third * 2, position.height);
            
            // try to walk the type tree (always do it if caching is disabled with the attribute)
            if (m_RefType == null || attr.CacheTypes == false)
            {
                if (TryFindReferenceType(property, out m_RefType))
                {
                    m_ChildTypes = FindSubTypes(m_RefType);

                    // remove ignored entries (if any) based on attr
                    m_ChildTypes.RemoveAll((t) => attr.IgnoredNamespaces.Any(n => t.Namespace == n));
                    
                    m_TypeOptions = m_ChildTypes.Select(t => t.Name).Union(new string[]{m_RefType.Name}).ToArray();
                }
            }

            // draw the layout
            m_TypeOptionsIndex = EditorGUI.Popup(dropdownRect, m_TypeOptionsIndex, m_TypeOptions);
            m_TypeFilter = m_TypeOptionsIndex >= m_ChildTypes.Count
                ? m_RefType
                : m_ChildTypes[m_TypeOptionsIndex];

            if (m_TypeFilter == null)
            {
                // no luck with providing better filtering, use default
                EditorGUI.ObjectField(valueRect, property);
            }
            else
            {
                // use better filtering
                EditorGUI.ObjectField(valueRect, property, m_TypeFilter, GUIContent.none);
            }

            EditorGUI.indentLevel = srcIndentLevel;
            
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Try to <see cref="FindReferenceType"/>
        /// </summary>
        /// <param name="prop">property to search</param>
        /// <param name="refType">found type</param>
        /// <returns>true on success; false on failure</returns>
        private static bool TryFindReferenceType(SerializedProperty prop, out Type refType)
        {
            try
            {
                refType = SerializedObjectUtils.FindPropertyType(prop);
                if (refType != null)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                refType = null;
            }
            
            return false;
        }

        /// <summary>
        /// Scans all assemblies for sub types, given a generic type.
        /// </summary>
        /// <param name="genericType">type to scan for</param>
        /// <returns>derived types, if any</returns>
        private static List<Type> FindSubTypes(Type genericType)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(t =>
                t.BaseType is {IsGenericType: true} && t.BaseType == genericType).ToList();
        }
    }
}