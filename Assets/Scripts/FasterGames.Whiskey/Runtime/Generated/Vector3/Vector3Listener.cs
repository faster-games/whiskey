
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
/// <summary>
/// A Vector3 reaction
/// </summary>
[Serializable]
public class Vector3Reaction : UnityEvent<Vector3>
{
}

/// <summary>
/// A Vector3 listener
/// </summary>
public class Vector3Listener : BaseListener<Vector3Event, Vector3Reaction, Vector3>
{
#pragma warning disable 0649
    /// <summary>
    /// Vector3 event storage
    /// </summary>
    [SerializeField]
    private Vector3Event @event;

    /// <summary>
    /// Vector3 reaction storage
    /// </summary>
    [SerializeField]
    private Vector3Reaction reaction = new Vector3Reaction();
#pragma warning restore 0649

    /// <inheritdoc/>
    public override Vector3Reaction Reaction => reaction;

    /// <inheritdoc/>
    public override Vector3Event Event { get { return @event; } set { @event = value; } }

    /// <inheritdoc/>
    private void OnEnable() => @event.Subscribe(this);

    /// <inheritdoc/>
    private void OnDisable() => @event.Unsubscribe(this);
}

}
