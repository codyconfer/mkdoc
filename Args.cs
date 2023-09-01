namespace mkdoc;

public static class Args
{
    public static (string action, Dictionary<string, object> argMap) ParseArgMap(string[] args)
    {
        try
        {
            if (args.Length < 1)
                throw new ArgumentException("Action not specified.");
            var action = args[0];
            var argMap = new Dictionary<string, object>();
            for (var i = 0; i < args[1..].Length; i++)
            {
                var arg = args[i];
                if (!arg.StartsWith('-'))
                    continue;
                var key = arg.TrimStart('-');
                i++;
                var value = args[i];
                argMap.Add(key, value);
            }

            return (action, argMap);
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
