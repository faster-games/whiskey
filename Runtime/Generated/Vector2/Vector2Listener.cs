
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
/// A Vector2 reaction
/// </summary>
[Serializable]
public class Vector2Reaction : UnityEvent<Vector2>
{
}

/// <summary>
/// A Vector2 listener
/// </summary>
public class Vector2Listener : BaseListener<Vector2Event, Vector2Reaction, Vector2>
{
#pragma warning disable 0649
    /// <summary>
    /// Vector2 event storage
    /// </summary>
    [SerializeField]
    private Vector2Event @event;

    /// <summary>
    /// Vector2 reaction storage
    /// </summary>
    [SerializeField]
    private Vector2Reaction reaction = new Vector2Reaction();
#pragma warning restore 0649

    /// <inheritdoc/>
    public override Vector2Reaction Reaction => reaction;

    /// <inheritdoc/>
    public override Vector2Event Event { get { return @event; } set { @event = value; } }

    /// <inheritdoc/>
    private void OnEnable() => @event.Subscribe(this);

    /// <inheritdoc/>
    private void OnDisable() => @event.Unsubscribe(this);
}

}