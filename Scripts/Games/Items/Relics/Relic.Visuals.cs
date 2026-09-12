using System.Collections.Generic;
using Godot;

namespace ProjectPQ.Scripts.Games.Relics.Relics;

public abstract partial class Relic
{
    protected abstract string BaseName { get; }
    protected abstract string BaseDescription { get; }

    protected abstract string PreservationFolder { get; }

    private readonly Dictionary<string, Texture2D> _cachePreservIcon = [];

    public override Texture2D Icon
    {
        get
        {
            string resPath = PreservationFolder
                .PathJoin($"{Preservation.Type.Name}.png");

            if (_cachePreservIcon.TryGetValue(resPath, out Texture2D? cache))
                return cache;

            Texture2D texture = GD.Load<Texture2D>(resPath);

            _cachePreservIcon[resPath] = texture;
            return texture;
        }
    }

    public override string Name
    {
        get
        {
            string fullStats = Preservation.Name + Rarity.Name;
            return $"{fullStats}[color={ColorUtils.White}]{BaseName}[/color]";
        }
    }

    public override string Description =>
        $"{Appraisal.Name}\n\n{BaseDescription}";
}