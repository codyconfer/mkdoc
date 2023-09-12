using System.Text.Json;
using mkdoc.Records;
using static mkdoc.Functions.FileSystemOperator;

namespace mkdoc.Functions;

public static class DataHasher
{
    public static async Task<HashData> ParseHashData(ArgMap argMap)
    {
        try
        {
            var command = argMap.Command;
            var data = argMap.Data;
            var path = GetTemplatePath(command);
            var hashDataPath = GetHashDataPath(command, argMap);
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
            throw new ApplicationException(
                e switch
                {
                    ApplicationException => $"Error Parsing HashData: {e.Message}",
                    ArgumentException => $"Error Parsing HashData: {e.Message}",
                    DirectoryNotFoundException
                        => $"Error Accessing Template Directory: {e.Message}",
                    _ => "Error Parsing HashData."
                }
            );
        }
    }
}
