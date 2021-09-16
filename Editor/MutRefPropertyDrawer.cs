using UnityEditor;
using UnityEngine;

namespace FasterGames.Whiskey.Editor
{
    /// <summary>
    /// A custom property drawer for <see cref="MutRef{T}"/> instances.
    /// </summary>
    [CustomPropertyDrawer(typeof(MutRef<>), useForChildren: true)]
    public class MutRefPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.PropertyField(position, property.FindPropertyRelative(nameof(MutRef<Object>.FieldNames.accessor)), new GUIContent(property.displayName));
            
            EditorGUI.EndProperty();
        }
    }
}