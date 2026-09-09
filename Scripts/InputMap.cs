using Godot;

namespace ProjectPQ.Scripts;

public static class InputMap
{
    public static StringName W { get; } = new("w");
    public static StringName A { get; } = new("a");
    public static StringName S { get; } = new("s");
    public static StringName D { get; } = new("d");

    public static StringName MouseLeft { get; } = new("mouse_left");
}