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
    private const string LanguagePath = $"{ParentPath}/language";
    private const string CsvPath = $"{LanguagePath}/language.csv";
    private const string LangIdPath = $"{ParentPath}/Maps/LangId.cs";
    private const string LangKeyPath = $"{ParentPath}/Maps/LangKey.cs";
    private const string GeneratedLanguagePath = $"{ParentPath}/Maps/language.csv";

    public static void Generate()
    {
        string absoluteCsvPath =
            ProjectSettings.GlobalizePath(CsvPath);

        if (!File.Exists(absoluteCsvPath))
            LogUtils.Throw<FileNotFoundException>(
                $"CSV 찾지 못함: {CsvPath}"
            );

        string[] lines =
            File.ReadAllLines(
                absoluteCsvPath,
                Encoding.UTF8
            );

        if (lines.Length == 0)
            LogUtils.Throw<InvalidDataException>(
                "CSV가 비었음"
            );

        List<string[]> rows =
        [
            ..lines.Select(x => x.Split(','))
        ];

        GenerateLangId(rows);
        GenerateLangKey();
    }

    private static string GenerateStaticClass(
        string className,
        List<string> strings
    )
    {
        StringBuilder sb = new();

        sb.AppendLine("// <auto-generator/>");
        sb.AppendLine();
        sb.AppendLine(
            "namespace ProjectPQ.addons.localizers.Maps;"
        );
        sb.AppendLine();
        sb.AppendLine(
            $"public static class {className}"
        );
        sb.AppendLine("{");

        foreach (string str in strings)
        {
            ValidateIdentifier(str);

            sb.AppendLine(
                $"    public static readonly string {str} = \"{str}\";"
            );
        }

        sb.AppendLine("}");

        return sb.ToString();
    }

    private static void GenerateLangId(
        List<string[]> rows
    )
    {
        string[] headers = rows[0];

        ValidateHeaders(headers);

        List<string> languages =
        [
            ..headers.Skip(1)
        ];

        foreach (string lang in languages)
            ValidateUppercase(
                lang,
                "Language ID"
            );

        WriteFile(
            LangIdPath,
            GenerateStaticClass(
                "LangId",
                languages
            )
        );
    }

    private static void GenerateLangKey()
    {
        string absoluteLanguagePath =
            ProjectSettings.GlobalizePath(LanguagePath);

        if (!Directory.Exists(absoluteLanguagePath))
            LogUtils.Throw<DirectoryNotFoundException>(
                $"Language 폴더 찾지 못함: {LanguagePath}"
            );

        string[] csvFiles =
            Directory.GetFiles(
                absoluteLanguagePath,
                "*.csv",
                SearchOption.AllDirectories
            );

        List<string> languages = [];
        List<string> keys = [];
        List<string[]> translations = [];

        foreach (string csvFile in csvFiles)
        {
            string relativePath =
                Path.GetRelativePath(
                    absoluteLanguagePath,
                    csvFile
                );

            string prefix =
                Path.GetFileNameWithoutExtension(
                    relativePath
                );

            string directory =
                Path.GetDirectoryName(relativePath)
                ?? string.Empty;

            if (!string.IsNullOrEmpty(directory))
            {
                prefix =
                    $"{directory}_{prefix}";
            }

            prefix = prefix
                .Replace(
                    Path.DirectorySeparatorChar,
                    '_'
                )
                .Replace(
                    Path.AltDirectorySeparatorChar,
                    '_'
                )
                .ToUpperInvariant();

            string[] lines =
                File.ReadAllLines(
                    csvFile,
                    Encoding.UTF8
                );

            if (lines.Length == 0)
                LogUtils.Throw<InvalidDataException>(
                    $"CSV가 비었음: {relativePath}"
                );

            // Header
            string[] headers =
                lines[0].Split(',');

            if (headers.Length < 2)
                LogUtils.Throw<InvalidDataException>(
                    $"CSV에 Language column이 없음: {relativePath}"
                );

            if (languages.Count == 0)
            {
                languages.AddRange(
                    headers.Skip(1)
                );

                foreach (string language in languages)
                {
                    ValidateUppercase(
                        language,
                        "Language ID"
                    );
                }
            }
            else
            {
                string[] currentLanguages =
                    headers.Skip(1).ToArray();

                if (!languages.SequenceEqual(currentLanguages))
                {
                    LogUtils.Throw<InvalidDataException>(
                        $"CSV의 Language columns가 서로 다름: {relativePath}"
                    );
                }
            }

            // Rows
            for (int i = 1; i < lines.Length; i++)
            {
                string[] columns =
                    lines[i].Split(',');

                if (columns.Length == 0)
                    continue;

                string key =
                    columns[0].Trim();

                if (string.IsNullOrWhiteSpace(key))
                    continue;

                ValidateUppercase(
                    key,
                    $"Localization Key ({relativePath})"
                );

                if (columns.Length != headers.Length)
                {
                    LogUtils.Throw<InvalidDataException>(
                        $"Column 개수가 맞지 않음: {relativePath} / {key}"
                    );
                }

                string fullKey =
                    $"{prefix}_{key}";

                ValidateIdentifier(fullKey);

                if (keys.Contains(fullKey))
                {
                    LogUtils.Throw<InvalidDataException>(
                        $"중복된 Localization Key: {fullKey}"
                    );
                }

                keys.Add(fullKey);

                // KEY를 fullKey로 교체
                string[] translation =
                    columns.ToArray();

                translation[0] = fullKey;

                translations.Add(translation);
            }
        }

        // -----------------------------
        // Maps/language.csv 생성
        // -----------------------------

        StringBuilder csv = new();

        csv.Append("KEY");

        foreach (string language in languages)
            csv.Append($",{language}");

        csv.AppendLine();

        foreach (string[] translation in translations)
        {
            csv.AppendLine(
                string.Join(",", translation)
            );
        }

        WriteFile(
            GeneratedLanguagePath,
            csv.ToString()
        );

        // -----------------------------
        // LangKey.cs 생성
        // -----------------------------

        WriteFile(
            LangKeyPath,
            GenerateStaticClass(
                "LangKey",
                keys
            )
        );
    }


    private static void ValidateHeaders(
        string[] headers
    )
    {
        if (headers.Length < 2)
            LogUtils.Throw<InvalidDataException>(
                "Localization CSV requires KEY and Language columns."
            );

        if (headers.Any(string.IsNullOrWhiteSpace))
            LogUtils.Throw<InvalidDataException>(
                "CSV header cannot be empty."
            );

        if (headers[0] != "KEY")
            LogUtils.Throw<InvalidDataException>(
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
            LogUtils.Throw<ArgumentException>(
                $"{type} must be uppercase: {value}"
            );
    }

    private static void ValidateIdentifier(
        string value
    )
    {
        if (!SyntaxFacts.IsValidIdentifier(value))
            LogUtils.Throw<InvalidDataException>(
                $"Invalid identifier: {value}"
            );
    }

    private static void WriteFile(
        string path,
        string content
    )
    {
        string absolute =
            ProjectSettings.GlobalizePath(path);

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
