using System.IO;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace ProjectPQ.Scripts;

public enum AppDataNaming
{
    SnakeCase,
    CamelCase,
    PascalCase,
    ScreamingSnakeCase
}

public static class AppDataNamingExtensions
{
    public static string Convert(this AppDataNaming naming, string value) =>
        naming switch
        {
            AppDataNaming.SnakeCase => value.ToSnakeCase(),
            AppDataNaming.CamelCase => value.ToCamelCase(),
            AppDataNaming.PascalCase => value.ToPascalCase(),
            AppDataNaming.ScreamingSnakeCase => value.ToSnakeCase().ToUpperInvariant(),

            _ => value
        };
}

public static partial class AppDataUtils
{
    public static string UserDataDir => OS.GetUserDataDir();

    public static string GetFullPath(string path, AppDataNaming naming) =>
        Path.Combine(UserDataDir, NormalizePath(path, naming));
    
    private static string NormalizePath(string path, AppDataNaming naming)
    {
        string directory = Path.GetDirectoryName(path) ?? "";
        string fileName = Path.GetFileName(path);

        string name = Path.GetFileNameWithoutExtension(fileName);
        string ext = Path.GetExtension(fileName).ToLowerInvariant();

        return Path.Combine(
            naming.Convert(directory),
            naming.Convert(name) + ext
        );
    }
 
    public static async Task WriteTextAsync(
        string path, string contents = "",
        AppDataNaming naming = AppDataNaming.SnakeCase
    )
    {
        string fullPath = GetFullPath(path, naming);

        string? directory = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        await File.WriteAllTextAsync(fullPath, contents, Encoding.UTF8);
    }

    public static async Task<string?> ReadTextAsync(string path, AppDataNaming naming = AppDataNaming.SnakeCase)
    {
        string fullPath = GetFullPath(path, naming);

        if (!File.Exists(fullPath))
            return null;

        return await File.ReadAllTextAsync(
            fullPath,
            Encoding.UTF8
        );
    }

    public static async Task<string> ReadTextAsync(
        string path, string orDefault,
        AppDataNaming naming = AppDataNaming.SnakeCase
    ) => await ReadTextAsync(path, naming) ?? orDefault;
}