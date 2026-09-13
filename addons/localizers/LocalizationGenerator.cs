using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
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
        string absoluteCsvPath = ProjectSettings.GlobalizePath(CsvPath);

        if (!File.Exists(absoluteCsvPath))
            LogUtils.Throw<FileNotFoundException>(
                $"CSV 찾지 못함: {CsvPath}"
            );

        List<string[]> rows =
            ReadCsv(absoluteCsvPath);

        if (rows.Count == 0)
            LogUtils.Throw<InvalidDataException>(
                "CSV가 비었음"
            );

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
        sb.AppendLine("namespace ProjectPQ.addons.localizers.Maps;");
        sb.AppendLine();
        sb.AppendLine($"public static class {className}");
        sb.AppendLine("{");

        foreach (string str in strings)
        {
            ValidateIdentifier(str);

            sb.AppendLine(
                $"    public const string {str} = \"{str}\";"
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

        List<string> languages = [..headers.Skip(1)];

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
            ProjectSettings.GlobalizePath(
                LanguagePath
            );

        if (!Directory.Exists(absoluteLanguagePath))
            LogUtils.Throw<DirectoryNotFoundException>(
                $"Language 폴더 찾지 못함: {LanguagePath}"
            );

        string[] csvFiles = [..Directory.GetFiles(
            absoluteLanguagePath,
            "*.csv",
            SearchOption.AllDirectories
        )
        .OrderBy(x => x)];

        if (csvFiles.Length == 0)
            LogUtils.Throw<InvalidDataException>(
                $"Language CSV가 없음: {LanguagePath}"
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

            List<string[]> rows = ReadCsv(csvFile);

            if (rows.Count == 0)
                LogUtils.Throw<InvalidDataException>(
                    $"CSV가 비었음: {relativePath}"
                );

            string[] headers = rows[0];

            ValidateHeaders(headers);

            List<string> currentLanguages = [..headers.Skip(1)];

            foreach (string language in currentLanguages)
                ValidateUppercase(
                    language,
                    $"Language ID ({relativePath})"
                );

            // 모든 CSV는 동일한 언어 컬럼을 가져야 함
            if (languages.Count == 0)
                languages.AddRange(
                    currentLanguages
                );
            else if (!languages.SequenceEqual(currentLanguages))
                LogUtils.Throw<InvalidDataException>(
                    $"CSV의 Language columns가 서로 다름: {relativePath}"
                );

            string prefix = CreatePrefix(relativePath);

            foreach (string[] row in rows.Skip(1))
            {
                if (row.Length == 0) continue;

                string key = row[0].Trim();

                if (string.IsNullOrWhiteSpace(key))
                    continue;

                if (row.Length != headers.Length)
                    LogUtils.Throw<InvalidDataException>(
                        $"Column 개수가 맞지 않음: {relativePath} / {key}"
                    );

                ValidateUppercase(
                    key,
                    $"Localization Key ({relativePath})"
                );

                string fullKey = $"{prefix}_{key}";

                ValidateIdentifier(fullKey);

                if (keys.Contains(fullKey))
                {
                    LogUtils.Throw<InvalidDataException>(
                        $"중복된 Localization Key: {fullKey}"
                    );
                }

                keys.Add(fullKey);

                string[] translation = [..row];

                translation[0] = fullKey;

                translations.Add(translation);
            }
        }

        // -----------------------------
        // Maps/language.csv
        // -----------------------------

        StringBuilder csv = new();

        csv.Append(
            EscapeCsvField("KEY")
        );

        foreach (string language in languages)
        {
            csv.Append(',');
            csv.Append(
                EscapeCsvField(language)
            );
        }

        csv.AppendLine();

        foreach (string[] translation in translations)
            csv.AppendLine(
                string.Join(
                    ",",
                    translation.Select(
                        EscapeCsvField
                    )
                )
            );

        WriteFile(
            GeneratedLanguagePath,
            csv.ToString()
        );

        // -----------------------------
        // LangKey.cs
        // -----------------------------

        WriteFile(
            LangKeyPath,
            GenerateStaticClass(
                "LangKey",
                keys
            )
        );
    }

    private static string CreatePrefix(
        string relativePath
    )
    {
        string prefix =
            Path.GetFileNameWithoutExtension(
                relativePath
            );

        string directory =
            Path.GetDirectoryName(
                relativePath
            ) ?? string.Empty;

        if (!string.IsNullOrEmpty(directory))
            prefix = $"{directory}_{prefix}";
        

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

        ValidateIdentifier(prefix);

        return prefix;
    }

    private static List<string[]> ReadCsv(
        string path
    )
    {
        using StreamReader reader = new(
            path,
            new UTF8Encoding(
                encoderShouldEmitUTF8Identifier: false
            )
        );

        using CsvReader csv = new(
            reader,
            new CsvConfiguration(
                CultureInfo.InvariantCulture
            )
            {
                HasHeaderRecord = false,

                // CSV의 필드 개수가 이상할 경우
                // 우리가 직접 검증한다.
                DetectColumnCountChanges = false,

                // 공백은 직접 Trim한다.
                TrimOptions = TrimOptions.None
            }
        );

        List<string[]> rows = [];

        while (csv.Read())
        {
            string[]? record = csv.Parser.Record;

            if (record is not null)
                rows.Add(record);
        }

        return rows;
    }

    private static string EscapeCsvField(
        string value
    )
    {
        if (value.Contains('"'))
            value = value.Replace(
                "\"",
                "\"\""
            );

        if (
            value.Contains(',') ||
            value.Contains('"') ||
            value.Contains('\r') ||
            value.Contains('\n')
        )
        {
            return $"\"{value}\"";
        }

        return value;
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
        if (value != value.ToUpperInvariant())
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
            new UTF8Encoding(
                encoderShouldEmitUTF8Identifier: false
            )
        );
    }
}
