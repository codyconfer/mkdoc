using mkdoc.Records;

namespace mkdoc.Functions;

public static class ArgMapper
{
    public static ArgMap ParseArgMap(string[] args)
    {
        try
        {
            if (args.Length < 1)
                throw new ArgumentException("Action not specified.");
            var command = args[0];
            var config = new Dictionary<string, object>();
            var data = new Dictionary<string, object>();
            for (var i = 0; i < args[1..].Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith('/'))
                {
                    var key = arg.TrimStart('/');
                    i++;
                    var value = args[i];
                    config.Add(key, value);
                }
                else if (arg.StartsWith('-'))
                {
                    var key = arg.TrimStart('-');
                    i++;
                    var value = args[i];
                    data.Add(key, value);
                }
            }

            return new ArgMap(command, config, data);
        }
        catch (Exception e)
        {
            var message = "Error parsing arguments";
            throw new ArgumentException(
                e switch
                {
                    ApplicationException => $"{message}: {e.Message}",
                    _ => $"{message}."
                }
            );
        }
    }
}
