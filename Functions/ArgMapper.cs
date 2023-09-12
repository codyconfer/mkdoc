using mkdoc.Operators;
using mkdoc.Records;

namespace mkdoc.Functions;

public static class ArgMapper
{
    private static readonly string DefaultErrorPrefix = "Error Parsing Arguments";

    private static ArgumentException HandleException(this Exception e) =>
        new(
            e switch
            {
                ApplicationException => e.Message.PrefixError(DefaultErrorPrefix),
                _ => DefaultErrorPrefix
            }
        );

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
            throw e.HandleException();
        }
    }
}
