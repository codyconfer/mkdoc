using mkdoc.Records;

namespace mkdoc.Operators;

public static class FileSystemOperator
{
    private static string GetTemplateDirPath() =>
        Environment.GetEnvironmentVariable("TEMPLATE_DIR_PATH")
        ?? $"{Directory.GetCurrentDirectory()}/templates/";

    public static string GetTemplatePath(string action)
    {
        var templateDirPath = GetTemplateDirPath();
        var fileExists = File.Exists($"{templateDirPath}/{action}");
        var matchFiles = Directory.GetFiles($"{templateDirPath}", $"{action}.*");
        return fileExists ? $"{templateDirPath}/{action}" : matchFiles[0];
    }

    public static string GetHashDataPath(string action, ArgMap argMap)
    {
        if (argMap.Config.TryGetValue("i", out var inputArg))
            return $"{inputArg}";
        var templateDirPath = GetTemplateDirPath();
        var hasHashDataArg = argMap.Data.TryGetValue("hashdata.json", out var hashDataPath);
        if (hasHashDataArg)
            argMap.Data.Remove("hashdata.json");
        hashDataPath ??= File.Exists($"{templateDirPath}/{action}.hashdata.json")
            ? $"{templateDirPath}/{action}.hashdata.json"
            : string.Empty;
        return hashDataPath.ToString();
    }

    public static string GetOutputFilePath(string command, string templatePath, ArgMap argMap)
    {
        if (argMap.Config.TryGetValue("o", out var outputArg))
            return $"{Directory.GetCurrentDirectory()}/{outputArg}";
        var fileName = command.Contains('.')
            ? command
            : $"{command}{templatePath[(templatePath.LastIndexOf('.'))..]}";
        fileName = fileName.Contains('/') ? fileName[(fileName.LastIndexOf('/') + 1)..] : fileName;
        return $"{Directory.GetCurrentDirectory()}/{fileName}";
    }
}
