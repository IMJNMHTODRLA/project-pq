using System;
using System.Collections.Generic;
using Godot;
using ProjectPQ.Scripts.Games;
using ProjectPQ.Scripts.Guis.Inventories;

namespace ProjectPQ.Scripts.Guis;

public sealed partial class GuiManager : Singleton<GuiManager>
{
    private readonly Dictionary<Type, Control> _activeGuis = [];

    private void Toggle<T>()
        where T : Control, IScene<EmptyArgs>
    => Toggle<T, EmptyArgs>();

    private void Toggle<T, TArgs>(TArgs args = default)
        where T : Control, IScene<TArgs>
        where TArgs : struct, ISceneArgs
    {
        Type type = typeof(T);
        if (_activeGuis.Remove(type, out Control? control))
        {
            control.SafeQueueFree();
            return;
        }

        Control loadControl = SceneLoader.Load<T, TArgs>(args);
        _activeGuis[type] = loadControl;

        _canvasLayer.AttachNode(loadControl);
    }

    private CanvasLayer _canvasLayer = new();

    public override void _Ready()
    {
        this.AttachNode(_canvasLayer);
    }

    public override void _UnhandledInput(InputEvent input)
    {
        if (input.IsActionPressed(InputMap.E))
            Toggle<Inventory>();  
    }
}