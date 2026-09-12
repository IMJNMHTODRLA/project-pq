using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;

namespace ProjectPQ.Scripts;

public static class LogUtils
{
    public static Exception ThrowError<T>(
        string message,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = -1
    ) where T : Exception
    {
#if DEBUG
        GD.PrintErr($"[에러 발생!({filePath}:{lineNumber})]\n{message}");
#endif
        throw (T?) Activator.CreateInstance(typeof(T), message)
            ?? Activator.CreateInstance<T>();
    }
}

public static class CallableUtils
{
    public static void NextCall(Action action, params Variant[] args) =>
        Callable.From(action).CallDeferred(args);
}

public static class SafeUtils
{
    public static T? Run<T>(Func<T> action, Action<Exception> onCatch)
    {
        try
        {
            return action();
        }
        catch (Exception e)
        {
            onCatch(e);
            return default;
        }
    }

    public static void Run(Action action, Action<Exception> onCatch)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            onCatch(e);
        }
    }

    public static async Task<T?> RunAsync<T>(Func<Task<T>> action, Action<Exception> onCatch)
    {
        try
        {
            return await action();
        }
        catch (Exception e)
        {
            onCatch(e);
            return default;
        }
    }

    public static async Task RunAsync(Func<Task> action, Action<Exception> onCatch)
    {
        try
        {
            await action();
        }
        catch (Exception e)
        {
            onCatch(e);
        }
    }
}

public static class ColorUtils
{
    public const string White = "#F2F2F2";

    public const string Gray = "#999999";
    public const string DarkGray = "#4C4C4C";

    public const string Black = "#0C0C0C";
}

public static class CollectionExtensions
{
    public static bool IsValidIndex(int i, int len) => i > 0 && i < len;

    public static T? GetOrNull<T>(this IReadOnlyList<T> list, int index) where T : class =>
        IsValidIndex(index, list.Count) ? list[index] : null;
    
    public static bool SetOrSkip<T>(this IList<T> list, int index, T value)
    {
        bool result = IsValidIndex(index, list.Count);
        if (result) list[index] = value;
        return result;
    }



    public static T? GetOrNull<T>(this T[] array, int index) where T : class =>
        IsValidIndex(index, array.Length) ? array[index] : null;

    public static bool SetOrSkip<T>(this T[] array, int index, T value)
    {
        bool result = IsValidIndex(index, array.Length);
        if (result) array[index] = value;
        return result;
    }
}

public static class ObjectExtensions
{
    extension(object obj)
    {
        public Type Type => obj.GetType();
    }

    extension([NotNullWhen(false)] object? obj)
    {
        public bool IsNull 
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => obj == null;
        }
    }
}

public static class RandomUtils
{
    public static ulong NextUInt64(this Random shared)
    {
        Span<byte> bytes = stackalloc byte[8];
        Random.Shared.NextBytes(bytes);
        
        return BitConverter.ToUInt64(bytes);
    }

    public static bool Chance(this float rate) => Random.Shared.NextSingle() < rate;

    public static T NextElement<T>(this IReadOnlyList<T> values)
    {
        int random = Random.Shared.Next(values.Count);
        return values[random];
    }
}
