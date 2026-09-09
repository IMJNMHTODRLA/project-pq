using System;
using Godot;

namespace ProjectPQ.Scripts.Games.Items.Empties;

public partial class Empty : Item
{
    public override Texture2D Icon => throw new NotSupportedException();
    public override string Name => throw new NotSupportedException();
    public override string Description => throw new NotSupportedException();
}