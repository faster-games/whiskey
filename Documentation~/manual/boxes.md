# Boxes

[View Built-in Boxes](xref:FasterGames.Whiskey.Boxes)

Boxes are the core component of Whiskey. They provide a wrapper for data that allows it to be saved on disk within your Unity project, and versioned on it's own.

A Box is used in code via the `Box<T>` class. It provides data access and equality mechanisms, including storage for the value itself. It also dervives from `ScriptableObject`, so it can be serialized on disk within a Unity project. This is what makes `Box<T>` unique - it's value is managed as an asset in it's own file, as part of your project.

Whiskey comes with many built-in subclasses of `Box<T>` for boxing up common pieces of data. Things like `float`, `string`, `Vector3`, etc. These classes are named `Boxed{className}` - e.g. `BoxedFloat`. You may create instances of these using the Asset menu in the [Project Window](https://docs.unity3d.com/Manual/ProjectView.html) by right clicking, and navigating to `Whiskey/Boxes`.

Once a `Box<T>` instance is created within the project, it can be referenced directly in the Editor with drag and drop, to fields of the correct type. This probably feels familiar, as it's similar to how Unity supports connecting other resources (like sprites, audio, and models) to engine components (like `SpriteRenderer`, `AudioClip`, and `MeshFilter`, respectively).

When a `Box<T>` is selected in the [Project Window](https://docs.unity3d.com/Manual/ProjectView.html), the [Inspector](https://docs.unity3d.com/Manual/UsingTheInspector.html) will allow you to edit it's current value. Note that any changes to a `Box<T>` at runtime in a build (e.g. outside the Editor completely) will not be saved. So this isn't a good system for game saves!

## References

Once you've created some Boxes, you'll probably want to reference these boxes in game logic. It might be tempting to do this directly with `Box<T>` (which does work), but with a little indirection, we can further decouple our game code from our game data.

To do that, we introduce `Ref<T>` and `MutRef<T>` - two classes that can be used instead of `Box<T>` to reference data from within game code with more flexibility. The reason for this extra layer of indirection is so that we can replace the data with values from other locations without changing to code. For instance, imagine a gameplay engineer is trying to prototype a damage increase for a single enemy. With the box, they need to first find the game logic script in the Inspector, trace it to the `Box<T>`, and edit the value there. If this value is used by multiple enemies, then updating the `Box<T>` updates all enemies - so the engineer would have to create a new `Box<T>`, update the reference, and then continue prototyping. 

With a `Ref<T>`, the gameplay engineer is able to modify the data type directly in the inspector to be a `Direct` reference, which is stored as a constant. They can make this change for just the one enemy, modifying the constant as needed during prototyping, all without modifying the other enemies.

Taking that concept one step further, `Ref<T>` also supports accessing the data from a [Table](./tables.md), meaning that they could swap to a set of weighted values instead of a constant, or a `Box<T>`. All of this is possible without modifying game logic at all, and often without needing to create any new assets.

There's one other concept that's important about `Ref<T>` - it's immutable. This means that you cannot write values into it from game logic, you can only read. This is what lets us provide lots of different options for where to get the value from. However, you may find you need data that is writable - For that, we introduce `MutRef<T>`.

`MutRef<T>` behaves similarly to `Ref<T>`, except it allows writing and cannot be backed by a [Table](./tables.md). This is because Tables do not always return the same value, so writing to one isn't a defined operation - which value in the Table would be replaced?

## Creating your own

To create your own `Box<T>`, a few steps are needed:

- Create a new file - Note it's name much match the `Box<T>` name
- Add a class that derives from `Box<T>` where `T` is what you want to store in the Box
- Optionally add a [`[CreateAssetMenu]`](https://docs.unity3d.com/ScriptReference/CreateAssetMenuAttribute.html) attribute to it, so that it can be created directly via the Editor UI

Here's one example:

```cs
// in filename: CustomBoxedInt.cs
[CreateAssetMenu(menuName = "Whiskey/Boxes/CustomInt")]
public class CustomBoxedInt : Box<int> {}
```

It stores an integer value inside a Box.
