using System;
using FasterGames.Whiskey.Boxes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FasterGames.Whiskey.Editor
{
    /// <summary>
    /// A custom property drawer for <see cref="Ref{T}"/> instances.
    /// </summary>
    [CustomPropertyDrawer(typeof(Ref<>), useForChildren: true)]
    public class RefPropertyDrawer : PropertyDrawer
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
            
            var selectorProp = property.FindPropertyRelative(Ref<Object>.FieldNames.selector);

            // popup for selector enum value
            selectorProp.enumValueIndex =
                EditorGUI.Popup(dropdownRect, selectorProp.enumValueIndex, selectorProp.enumDisplayNames);

            // handle selector types ui
            var selectorPropValue = Enum.Parse(typeof(Ref<Object>.Selector),
                selectorProp.enumDisplayNames[selectorProp.enumValueIndex]);
            switch (selectorPropValue)
            {
                // draw a prop field for raw
                case Ref<Object>.Selector.Direct:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(Ref<Object>.FieldNames.raw),
                        GUIContent.none);
                    break;
                // draw a property field for boxed
                case Ref<Object>.Selector.Boxed:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(Ref<Object>.FieldNames.box),
                        GUIContent.none);
                    break;
                // draw a property field for table
                case Ref<Object>.Selector.Table:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(Ref<Object>.FieldNames.table),
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