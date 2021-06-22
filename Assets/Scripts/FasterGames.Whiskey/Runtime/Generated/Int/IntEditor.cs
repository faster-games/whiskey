
// <auto-generated>
// This code was generated by a tool. Any changes made manually will be lost
// the next time this code is regenerated.
// </auto-generated>

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FasterGames.Whiskey
{
#if UNITY_EDITOR
/// <summary>
/// Custom editor for <see cref="IntEvent"/>s
/// </summary>
[UnityEditor.CanEditMultipleObjects]
[UnityEditor.CustomEditor(typeof(IntEvent))]
public class IntEventEditor : UnityEditor.Editor
{
    /// <summary>
    /// Storage for data to raise the event with
    /// </summary>
    private int v;

    /// <inheritdoc/>
    public override void OnInspectorGUI()
    {
        UnityEngine.GUI.enabled = UnityEngine.Application.isPlaying;

        UnityEditor.EditorGUILayout.LabelField("Listener" + " (" + (targets.Length > 1 ? "Multi" : "Single") + ") Count", SumTargetListeners().ToString());

        v = (int)UnityEditor.EditorGUILayout.IntField(v);

        if (UnityEngine.GUILayout.Button("Raise"))
        {
            foreach (var tgt in targets)
            {
                var evt = (IntEvent)tgt;
                evt.Trigger(v);
            }
        }
    }

    /// <summary>
    /// Internal helper to count attached listeners
    /// </summary>
    private int SumTargetListeners()
    {
        int res = 0;
        foreach (var tgt in targets)
        {
            var evt = (IntEvent)tgt;
            res += evt.Count;
        }

        return res;
    }
}
#endif

}
