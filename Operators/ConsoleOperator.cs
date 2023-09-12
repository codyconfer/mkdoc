namespace mkdoc.Operators;

public static class ConsoleOperator
{
    private static string Prefix(this string message, string level) =>
        $"[{level}] | {message}";

    private static string Print(this string message, string level)
    {
        var output = message.Prefix(level);
        Console.WriteLine(output);
        return output;
    }

    public static string PrefixError(this string message, string prefix) => $"{prefix}: {message}";

    public static string PrintInfo(this string message) => message.Print("INFO");

    public static string PrintWarning(this string message) => message.Print("WARNING");

    public static string PrintWarning(Exception e) =>
        Print(
            e switch
            {
                ArgumentException => e.Message,
                ApplicationException => e.Message,
                _ => "Unknown error occured."
            },
            "WARNING"
        );

    public static string PrintError(this string message) => message.Print("ERROR");

    public static string PrintError(this Exception e) =>
        Print(
            e switch
            {
                ArgumentException => e.Message,
                ApplicationException => e.Message,
                _ => "Unknown error occured."
            },
            "ERROR"
        );
}
