#warning 추후에 language 폴더 안에다가 items/test.csv 이렇게 만들어지면 뭐 미래의 내가 알아서 생각하겠지ㅇㅇ

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Godot;
using Microsoft.CodeAnalysis.CSharp;
using ProjectPQ.Scripts;

namespace ProjectPQ.addons.localizers;

public static class LocalizationGenerator
{
    private const string ParentPath = "res://addons/localizers";

    private const string CsvPath = $"{ParentPath}/language/language.csv";
    private const string LangIdPath = $"{ParentPath}/Maps/LangId.cs";
    private const string LangKeyPath = $"{ParentPath}/Maps/LangKey.cs";

    public static void Generate()
    {
        string absoluteCsvPath = ProjectSettings.GlobalizePath(CsvPath);

        if (!File.Exists(absoluteCsvPath))
            LogUtils.ThrowError<FileNotFoundException>(
                $"CSV 찾지 못함: {CsvPath}"
            );

        string[] lines =
            File.ReadAllLines(
                absoluteCsvPath,
                Encoding.UTF8
            );

        if (lines.Length == 0)
            LogUtils.ThrowError<InvalidDataException>(
                "CSV가 비었음"
            );

        List<string[]> rows = [..lines.Select(x => x.Split(','))];

        GenerateLangId(rows);
        GenerateLangKey(rows);
    }

    private static string GenerateStaticClass(string className, List<string> strings)
    {
        StringBuilder sb = new();

        sb.AppendLine("// <auto-generator/>");
        sb.AppendLine();
        sb.AppendLine("namespace ProjectPQ.addons.localizers.Maps;");
        sb.AppendLine();
        sb.AppendLine($"public static class {className}");
        sb.AppendLine("{");

        foreach (string str in strings)
        {
            ValidateIdentifier(str);
            sb.AppendLine($"    public static string {str} {{ get; }} = \"{str}\";");
        }

        sb.AppendLine("}");

        return sb.ToString();
    }

    private static void GenerateLangId(List<string[]> rows)
    {
        string[] headers = rows[0];
        ValidateHeaders(headers);

        List<string> languages = [..headers.Skip(1)];

        foreach (string lang in languages)
            ValidateUppercase(lang, "Language ID");

        WriteFile(
            LangIdPath,
            GenerateStaticClass("LangId", languages)
        );
    }

    private static void GenerateLangKey(List<string[]> rows)
    {
        List<string> keys = [];

        for (int i = 1; i < rows.Count; i++)
        {
            string key = rows[i][0];

            ValidateUppercase(
                key,
                "Localization Key"
            );

            keys.Add(key);
        }

        WriteFile(
            LangKeyPath,
            GenerateStaticClass("LangKey", keys)
        );
    }

    private static void ValidateHeaders(string[] headers)
    {
        if (headers.Length < 2)
            LogUtils.ThrowError<InvalidDataException>(
                "Localization CSV requires KEY and Language columns."
            );

        if (headers.Any(string.IsNullOrWhiteSpace))
            LogUtils.ThrowError<InvalidDataException>(
                "CSV header cannot be empty."
            );

        if (headers[0] != "KEY")
            LogUtils.ThrowError<InvalidDataException>(
                $"First column must be KEY. Found: {headers[0]}"
            );

        foreach (string header in headers)
            ValidateUppercase(
                header,
                "CSV Header"
            );
    }

    private static void ValidateUppercase(
        string value,
        string type
    )
    {
        if (value != value.ToUpper())
            LogUtils.ThrowError<ArgumentException>(
                $"{type} must be uppercase: {value}"
            );
    }

    private static void ValidateIdentifier(string value)
    {
        if (!SyntaxFacts.IsValidIdentifier(value))
            LogUtils.ThrowError<InvalidDataException>($"Invalid identifier: {value}");
    }

    private static void WriteFile(
        string path,
        string content
    )
    {
        string absolute = ProjectSettings.GlobalizePath(path);
        Directory.CreateDirectory(
            Path.GetDirectoryName(absolute)!
        );

        File.WriteAllText(
            absolute,
            content,
            Encoding.UTF8
        );
    }
}