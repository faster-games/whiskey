
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
/// A int event
/// </summary>
[CreateAssetMenu(menuName = "Whiskey/Events/IntEvent")]
public class IntEvent : BaseEvent<IntListener, int>
{
    /// <summary>
    /// int handler storage
    /// </summary>
    [SerializeField]
    private List<IntListener> handlers = new List<IntListener>();

    /// <inheritdoc/>
    public override List<IntListener> Handlers => handlers;
}

}