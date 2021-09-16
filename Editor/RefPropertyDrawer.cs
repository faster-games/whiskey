using UnityEditor;
using UnityEngine;

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

            EditorGUI.PropertyField(position, property.FindPropertyRelative(nameof(Ref<Object>.FieldNames.accessor)), new GUIContent(property.displayName));
            
            EditorGUI.EndProperty();
        }
    }
}