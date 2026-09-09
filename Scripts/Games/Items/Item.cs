using Godot;
using ProjectPQ.Scripts.Games.Items.Empties;

namespace ProjectPQ.Scripts.Games.Items;

public abstract partial class Item : Resource
{
    public static Empty Empty { get; } = new Empty();

    public abstract Texture2D Icon { get; }

    public abstract string Name { get; }
    public abstract string Description { get; }

    public virtual bool Equals(Item? other) =>
        other != null &&
        this.Type == other.Type;

    public bool IsEmpty() => Equals(Empty);
    public bool IsNotEmpty() => !IsEmpty();

    public Item Clone() =>
        (Item) Duplicate(true);
}