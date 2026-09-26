using System;

namespace ProjectPQ.Scripts;

public static class GameMetadata
{
    public static class Version
    {
        public static GameVersion Latest => V1_0_0;

        public static GameVersion V1_0_0 => "v1.0.0";
    }
}

public readonly record struct GameId(long Value)
{
    public GameId() : this(Random.Shared.NextInt64())
    {
    }

    public static implicit operator long(GameId id) => id.Value;
    public static implicit operator GameId(long id) => new(id);
}

public readonly record struct GameVersion(string Value)
{
    public GameVersion() : this(GameMetadata.Version.Latest)
    {
    }

    public static implicit operator string(GameVersion ver) => ver.Value;
    public static implicit operator GameVersion(string str) => new(str);
}