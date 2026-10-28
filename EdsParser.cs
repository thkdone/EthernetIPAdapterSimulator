using System.Text.RegularExpressions;

namespace EthernetIPAdapterSimulator;

public enum ParameterCategory
{
    Configuration,
    Input,
    Output
}

public sealed class EdsParameter
{
    public int Number { get; init; }
    public string Name { get; init; } = string.Empty;
    public string DataType { get; init; } = string.Empty;
    public int DataSize { get; init; }
    public ParameterCategory Category { get; init; }
}

public sealed class EdsFileDefinition
{
    public string FilePath { get; init; } = string.Empty;
    public string DeviceName { get; init; } = string.Empty;
    public List<EdsParameter> Parameters { get; init; } = new();
    public List<EdsParameter> ConfigurationParameters { get; init; } = new();
    public List<EdsParameter> InputParameters { get; init; } = new();
    public List<EdsParameter> OutputParameters { get; init; } = new();
}

public static partial class EdsParser
{
    public static EdsFileDefinition Parse(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("EDS file path is required.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("EDS file was not found.", filePath);
        }

        var lines = File.ReadAllLines(filePath);
        var parameters = new List<EdsParameter>();

        for (var i = 0; i < lines.Length; i++)
        {
            var trimmed = lines[i].Trim();
            if (!trimmed.StartsWith("Param", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var parameterMatch = Regex.Match(trimmed, @"Param(?<number>\d+)", RegexOptions.IgnoreCase);
            if (!parameterMatch.Success)
            {
                continue;
            }

            var parameterNumber = int.Parse(parameterMatch.Groups["number"].Value);
            var builder = new List<string>();
            for (var j = i; j < lines.Length; j++)
            {
                builder.Add(lines[j]);
                if (lines[j].TrimEnd().EndsWith(";", StringComparison.Ordinal))
                {
                    i = j;
                    break;
                }
            }

            var blockText = string.Join(Environment.NewLine, builder);
            var category = GetParameterCategory(parameterNumber);
            var parameter = new EdsParameter
            {
                Number = parameterNumber,
                Name = ExtractParameterName(blockText),
                DataType = ExtractDataType(blockText),
                DataSize = ExtractDataSize(blockText),
                Category = category
            };

            parameters.Add(parameter);
        }

        var deviceName = ExtractDeviceName(lines);

        return new EdsFileDefinition
        {
            FilePath = filePath,
            DeviceName = deviceName,
            Parameters = parameters,
            ConfigurationParameters = parameters.Where(p => p.Category == ParameterCategory.Configuration).ToList(),
            InputParameters = parameters.Where(p => p.Category == ParameterCategory.Input).ToList(),
            OutputParameters = parameters.Where(p => p.Category == ParameterCategory.Output).ToList()
        };
    }

    private static ParameterCategory GetParameterCategory(int parameterNumber)
    {
        if (parameterNumber >= 1 && parameterNumber <= 3)
        {
            return ParameterCategory.Configuration;
        }

        if (parameterNumber >= 11 && parameterNumber <= 19)
        {
            return ParameterCategory.Input;
        }

        if (parameterNumber >= 101 && parameterNumber <= 104)
        {
            return ParameterCategory.Output;
        }

        return ParameterCategory.Configuration;
    }

    private static string ExtractParameterName(string blockText)
    {
        var match = Regex.Match(blockText, @"""(?<name>[^""]+)"",\s*\$\s*name", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        if (match.Success)
        {
            return match.Groups["name"].Value.Trim();
        }

        var fallback = Regex.Match(blockText, @"""(?<name>[^""]+)""", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        return fallback.Success ? fallback.Groups["name"].Value.Trim() : "Unnamed parameter";
    }

    private static string ExtractDataType(string blockText)
    {
        var match = Regex.Match(blockText, @"0x(?<value>[A-Fa-f0-9]+)\s*,\s*\$\s*Data Type", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        if (match.Success)
        {
            return $"0x{match.Groups["value"].Value.Trim().ToUpperInvariant()}";
        }

        return "Unknown";
    }

    private static int ExtractDataSize(string blockText)
    {
        var match = Regex.Match(blockText, @"(?<size>\d+)\s*,\s*\$\s*Data Size in bytes", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        if (match.Success)
        {
            return int.Parse(match.Groups["size"].Value);
        }

        return 0;
    }

    private static string ExtractDeviceName(string[] lines)
    {
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"ProdName\s*=\s*""(?<name>[^""]+)""", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups["name"].Value;
            }
        }

        return "Unnamed Device";
    }
}
