using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FasterGames.Whiskey.Editor
{
    /// <summary>
    /// A custom property drawer for <see cref="RefAccessor{T}"/> to support edit-time switching between <see cref="RefAccessor{T}.Selector"/> values.
    /// </summary>
    /// <remarks>
    /// This allows the consumer to control which inner accessor value will be used via the editor.
    /// </remarks>
    [CustomPropertyDrawer(typeof(RefAccessor<>), useForChildren: true)]
    public class RefAccessorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);


            var srcIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            var third = position.width / 3;
            var dropdownRect = new Rect(position.x, position.y, third * 1, position.height);
            var valueRect = new Rect(position.x + (third * 1), position.y, third * 2, position.height);
            
            var selectorProp = property.FindPropertyRelative(nameof(RefAccessor<Object>.selector));

            // popup for selector enum value
            selectorProp.enumValueIndex =
                EditorGUI.Popup(dropdownRect, selectorProp.enumValueIndex, selectorProp.enumDisplayNames);

            // handle selector types ui
            var selectorPropValue = Enum.Parse(typeof(RefAccessor<Object>.Selector),
                selectorProp.enumDisplayNames[selectorProp.enumValueIndex]);
            switch (selectorPropValue)
            {
                // draw a prop field for raw
                case RefAccessor<Object>.Selector.Raw:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(nameof(RefAccessor<Object>.raw)),
                        GUIContent.none);
                    break;
                // draw a property field for boxed
                case RefAccessor<Object>.Selector.Boxed:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(nameof(RefAccessor<Object>.boxed)),
                        GUIContent.none);
                    break;
                default:
                    throw new NotImplementedException();
            }

            EditorGUI.indentLevel = srcIndentLevel;
            
            EditorGUI.EndProperty();
        }
    }
}