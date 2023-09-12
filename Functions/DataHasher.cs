using System.Text.Json;
using mkdoc.Operators;
using mkdoc.Records;

namespace mkdoc.Functions;

public static class DataHasher
{
    private static readonly string DefaultErrorPrefix = "Error Parsing HashData";
    private static readonly string DirectoryErrorPrefix = "Error Accessing Template Directory";

    private static ApplicationException HandleException(this Exception e) =>
        new(
            e switch
            {
                ApplicationException => e.Message.PrefixError(DefaultErrorPrefix),
                ArgumentException => e.Message.PrefixError(DefaultErrorPrefix),
                DirectoryNotFoundException => e.Message.PrefixError(DirectoryErrorPrefix),
                _ => DefaultErrorPrefix
            }
        );

    public static async Task<HashData> ParseHashData(ArgMap argMap)
    {
        try
        {
            var command = argMap.Command;
            var data = argMap.Data;
            var path = FileSystemOperator.GetTemplatePath(command);
            var hashDataPath = FileSystemOperator.GetHashDataPath(command, argMap);
            if (string.IsNullOrEmpty($"{hashDataPath}"))
                return new(path, data);
            var hashRaw = await File.ReadAllTextAsync($"{hashDataPath}");
            var hashData = JsonSerializer.Deserialize<Dictionary<string, object>>(hashRaw);
            foreach (var arg in data)
                hashData[arg.Key] = arg.Value;
            return new(path, hashData);
        }
        catch (Exception e)
        {
            throw e.HandleException();
        }
    }
}
