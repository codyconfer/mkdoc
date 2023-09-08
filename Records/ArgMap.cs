namespace mkdoc.Records;

public record ArgMap(
    string Command,
    Dictionary<string, object> Config,
    Dictionary<string, object> Data,
    string TemplatePath = default
);
