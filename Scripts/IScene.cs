using System;
using Godot;

namespace ProjectPQ.Scripts;

public interface ISceneArgs;
public readonly struct EmptyArgs : ISceneArgs;

public interface IScene<TArgs>
    where TArgs : struct, ISceneArgs
{
    public abstract static string ScenePath { get; }

    public void SceneInit(TArgs args) {}
}

public static class SceneLoader
{
    public static T Load<T, TArgs>(TArgs args = default)
        where T : Node, IScene<TArgs>
        where TArgs : struct, ISceneArgs
    {
        PackedScene scene = GD.Load<PackedScene>(T.ScenePath);
        
        T tScene = scene.Instantiate<T>();
        
        tScene.SceneInit(args);
        
        return tScene;
    }

    public static T Change<T, TArgs>(TArgs args = default)
        where T : Node, IScene<TArgs>
        where TArgs : struct, ISceneArgs
    {
        T node = Load<T, TArgs>(args);

        SceneTree tree = Engine.GetMainLoop() as SceneTree
            ?? throw LogUtils.Throw<InvalidOperationException>("SceneTree not found.");

        tree.ChangeSceneToNode(node);

        return node;
    }
}

public static class SceneUtils
{
    public static void DetachNode(this Node node)
    {
        node.GetParent()?.RemoveChild(node);
    }

    public static void AttachNode(this Node parent, Node child)
    {
        if (child.GetParent() != null)
            child.Reparent(parent);
        else
            parent.AddChild(child);
    }
}