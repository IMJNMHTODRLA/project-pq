using System;
using Godot;

namespace ProjectPQ.Scripts;

public interface IScene
{
    public abstract static string ScenePath { get; }

    public void OnSceneLoaded() {}
}

public static class SceneLoader
{
    public static T Load<T>(bool disableLoadAction = false)
        where T : Node, IScene
    {
        PackedScene scene = GD.Load<PackedScene>(T.ScenePath);
        
        T tScene = scene.Instantiate<T>();
        
        if (!disableLoadAction)
            tScene.OnSceneLoaded();
        
        return tScene;
    }

    public static T Change<T>(bool disableLoadAction = false)
        where T : Node, IScene
    {
        T node = Load<T>(disableLoadAction);

        SceneTree tree = Engine.GetMainLoop() as SceneTree
            ?? throw new InvalidOperationException("SceneTree not found.");

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