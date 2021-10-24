
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
/// A Vector3 event
/// </summary>
[CreateAssetMenu(menuName = "Whiskey/Events/Vector3Event")]
public class Vector3Event : BaseEvent<Vector3Listener, Vector3>
{
    /// <summary>
    /// Vector3 handler storage
    /// </summary>
    [SerializeField]
    private List<Vector3Listener> handlers = new List<Vector3Listener>();

    /// <inheritdoc/>
    public override List<Vector3Listener> Handlers => handlers;
}

}