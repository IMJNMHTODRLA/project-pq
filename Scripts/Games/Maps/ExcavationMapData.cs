using System.Collections.Generic;
using Godot;

namespace ProjectPQ.Scripts.Games.Maps;

public abstract class ExcavationMapData : MapData
{
    private readonly List<Vector2> _digPointCoords = [];
    public IReadOnlyList<Vector2> DigPointCoords => _digPointCoords;

    public ExcavationMapData()
    {
        RegenDigPoints();
        TickManager.Self.NextDay += RegenDigPoints;
    }

    protected virtual void RegenDigPoints(long _ = 0)
    {
        Node currentScene = SceneLoader.GetSceneTree().CurrentScene;
        ExcavationMap? excMap = currentScene as ExcavationMap;
        
        if (excMap?.LinkMapData.Type == this.Type)
            return;

        int length = 10;

        for (int i = 0; i < length; i++)
            _digPointCoords[i] = new(
                GDUtils.RandfRange(-100.0f, 100.0f),
                GDUtils.RandfRange(-100.0f, 100.0f)
            );
    }

    public void RemoveDigPoints(Vector2 pos) =>
        _digPointCoords.Remove(pos);
}