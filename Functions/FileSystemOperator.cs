namespace mkdoc.Functions;

public static class FileSystemOperator
{
    public static string GetTemplateDirPath() =>
        Environment.GetEnvironmentVariable("TEMPLATE_DIR_PATH")
        ?? $"{Directory.GetCurrentDirectory()}/templates/";

    public static string GetTemplatePath(string action)
    {
        var templateDirPath = GetTemplateDirPath();
        var fileExists = File.Exists($"{templateDirPath}/{action}");
        var matchFiles = Directory.GetFiles($"{templateDirPath}", $"{action}.*");
        return fileExists ? $"{templateDirPath}/{action}" : matchFiles[0];
    }

    public static string GetHashDataPath(string action, Dictionary<string, object> argMap)
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

    public static string GetOutputFilePath(string command, string templatePath)
    {
        var fileName = $"{command}{templatePath[(templatePath.LastIndexOf('.'))..]}";
        fileName = fileName.Contains('/') ? fileName[(fileName.LastIndexOf('/') + 1)..] : fileName;
        return $"{Directory.GetCurrentDirectory()}/{fileName}";
    }
}
