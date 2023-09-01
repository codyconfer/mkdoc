using System.Text.Json;
using static mkdoc.FileSystem;

namespace mkdoc;

public static class HashData
{
    public static async Task<(string action, Dictionary<string, object> hashData)> ParseHashData(
        string action,
        Dictionary<string, object> argMap
    )
    {
        try
        {
            var path = GetTemplatePath(action);
            var hashDataPath = GetHashDataPath(action, argMap);
            if (string.IsNullOrEmpty($"{hashDataPath}"))
                return (path, argMap);
            var hashRaw = await File.ReadAllTextAsync($"{hashDataPath}");
            var hashData = JsonSerializer.Deserialize<Dictionary<string, object>>(hashRaw);
            foreach (var arg in argMap)
                hashData.Add(arg.Key, arg.Value);
            return (path, hashData);
        }
        catch (Exception e)
        {
            var message = "Error Parsing HashData";
            throw new ApplicationException(
                e switch
                {
                    ApplicationException => $"{message}: {e.Message}",
                    ArgumentException => $"{message}: {e.Message}",
                    _ => $"{message}."
                }
            );
        }
    }

    private static string GetTemplatePath(string action)
    {
        var templateDirPath = GetTemplateDirPath();
        var fileExists = File.Exists($"{templateDirPath}/{action}");
        var matchFiles = Directory.GetFiles($"{templateDirPath}", $"{action}.*");
        return fileExists ? $"{templateDirPath}/{action}" : matchFiles[0];
    }

    private static string GetHashDataPath(string action, Dictionary<string, object> argMap)
    {
        var templateDirPath = GetTemplateDirPath();
        var hasHashDataArg = argMap.TryGetValue("hashdata.json", out var hashDataPath);
        if (hasHashDataArg)
            argMap.Remove("hashdata.json");
        hashDataPath ??= File.Exists($"{templateDirPath}/{action}.hashdata.json")
            ? $"{templateDirPath}/{action}.hashdata.json"
            : string.Empty;
        return hashDataPath.ToString();
    }
}
