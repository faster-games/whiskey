# Getting Started

Whiskey is a framework for data and events, leaning heavily on [ScriptableObject](https://docs.unity3d.com/Manual/class-ScriptableObject.html) to make this possible. If you haven't seen [Ryan's talk on ScriptableObject's](https://www.youtube.com/watch?v=raQ3iHhE_Kk) I recommend watching that first.

Learn more about the following topics in their dedicated sections:

- [Boxes](./boxes.md) - Data instances persisted as `Assets`
- [Tables](./tables.md) - Weighted probability tables persisted as `Assets`
- [Events](./events.md) - Events and reactions for when important things occur

Whiskey provides a number of built in constructs as well as base classes that can be quickly extended to for customization. It also provides a set of editor extensions to make working with it's features easier.

If something isn't working, please feel free to [Check the Issues](https://github.com/faster-games/whiskey/issues) and [Open a new Issue](https://github.com/faster-games/whiskey/issues/new) as needed! Thank you! 🙏

## Quickstart

> Disclaimer: By nature, Whiskey isn't easy to demonstrate. As such, this may not be as "quick" as you'd expect! 😅

- Right click in the [Project Window](https://docs.unity3d.com/Manual/ProjectView.html) select: `Create, Whiskey, Boxes, Int`
- Name the created data instance
- Select the data instance
- Edit the data in the [Inspector Window](https://docs.unity3d.com/Manual/UsingTheInspector.html)
- Right click in the [Project Window](https://docs.unity3d.com/Manual/ProjectView.html) select: `Create, C# Script`
- Name the created script `IntTestBehaviour`
- Edit the script, adding the following:
```cs
using FasterGames.Whiskey.Boxes;
using UnityEngine;

public class IntTestBehaviour : MonoBehaviour
{
    public Ref<int> intRef;

    private void Start()
    {
        Debug.Log(intRef.Value);
    }
}
```
- Right click in the [Hierarchy Window](https://docs.unity3d.com/Manual/Hierarchy.html) select: `Create Empty`
- Name the created object
- Select the object in the [Hierarchy Window](https://docs.unity3d.com/Manual/Hierarchy.html), then in the [Inspector Window](https://docs.unity3d.com/Manual/UsingTheInspector.html) select `Add Component` and search for `IntTestBehaviour`, selecting the first result (which should be the script you created above)
- Note the `Int Ref` field is displayed, by default it is a `Direct` reference. A direct reference is effectively a hard coded value.
- Select the `Direct` dropdown, and change it to `Boxed`. Note the field changes, allowing you to drag and drop an object reference, and showing an additional dropdown field.
- Ensure the additional dropdown field is set to `BoxedInt`, which will allow the editor to more accurately assist you in selecting the object.
- Click the object reference field, and select the data instance you created above.
- You have now made your first data reference using Whiskey. Run the code, and note that the value stored in your data instance is logged by the `IntTestBehaviour` script on `Start`.

## Terminology

This section describes the key terms that Whiskey uses when describing data in a Unity game. It's designed to be a quick reference, not an exhaustive overview.

### ScriptableObject

See https://docs.unity3d.com/Manual/class-ScriptableObject.html

### Data Instance

A piece of data stored as a ScriptableObject on disk, within the `Assets` folder of a Unity project.

### Box

A wrapper for C# classes and structures that allow it to be stored as a Data Instance, inside a Scriptable Object. This also defines the interfaces for equality, reading, and writing that you'll use to consume data throughout your game.

### Table

A probability table used to store weighted values, either as direct constants, or via a reference to some Box. A good overview for how these types of tables work can be [found here](https://lostgarden.home.blog/2014/12/08/loot-drop-tables/).

### Ref

A reference to data, either as a direct constant, via a reference to a Box, or via a reference to a Table. `Ref` is a read-only construct, and provides access via both the `Read()` method, and a `Value` property with a getter.

### MutRef

A reference to data, either as a direct constant, via a reference to a Box. `MutRef` is mutable, meaning the data supports reading and writing. Access is provided by `Read()`, `Write()` and a `Value` property with both a getter and a setter.

### Event

A strongly-typed collection of listeners, that can be raised with `Raise()` when some action occurs. Listeners can be added with `AddListener()` and removed with `RemoveListener()`.

### EventListener

A `MonoBehaviour` component that binds an Event to a reaction (described by a [UnityEvent](https://docs.unity3d.com/Manual/UnityEvents.html)). The reaction will occur when the event is raised, if the `GameObject` the listener is attached to is enabled at that time.

Calls `AddListener()` and `RemoveListener()` on the event, to bind itself as a listener.
