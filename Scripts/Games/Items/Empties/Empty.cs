using System;
using Godot;

namespace ProjectPQ.Scripts.Games.Relics.Empties;

public partial class Empty : Item
{
    public override Texture2D Icon => throw new NotSupportedException("Type이 Empty인데 Icon에 접근");
    public override string Name => throw new NotSupportedException("Type이 Empty인데 Name에 접근");
    public override string Description => throw new NotSupportedException("Type이 Empty인데 Description에 접근");
}