
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
/// A string event
/// </summary>
[CreateAssetMenu(menuName = "Whiskey/Events/StringEvent")]
public class StringEvent : BaseEvent<StringListener, string>
{
    /// <summary>
    /// string handler storage
    /// </summary>
    [SerializeField]
    private List<StringListener> handlers = new List<StringListener>();

    /// <inheritdoc/>
    public override List<StringListener> Handlers => handlers;
}

}