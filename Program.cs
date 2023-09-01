using mkdoc;
using Stubble.Core.Builders;

// ...
// Invocation: mkdoc {template-name} -hashdata.json {path-to-json} -{key} {value}
//  {template-name} REQUIRED
//  -hashdata.json {path-to-json} OPTIONAL
//      (if not specified, hashData is assumed to be located at @templates/{template-name}.hashData)
// -{key} {value} OPTIONAL
//      (if specified, will overwrite values in hashData)
// ...


try
{
    var (action, argMap) = Args.ParseArgMap(args);
    var (path, hashData) = await HashData.ParseHashData(action, argMap);
    var template = await File.ReadAllTextAsync(path);
    var stubble = new StubbleBuilder()
        .Configure(settings =>
        {
            settings.SetIgnoreCaseOnKeyLookup(true);
            settings.SetMaxRecursionDepth(512);
        })
        .Build();
    var output = stubble.Render(template, hashData);
    var fileName = $"{action}{path[(path.LastIndexOf('.'))..]}";
    await File.WriteAllTextAsync(
        $"{Directory.GetCurrentDirectory()}/{fileName}",
        output,
        CancellationToken.None
    );
}
catch (Exception e)
{
    Console.WriteLine(
        e switch
        {
            ArgumentException => e.Message,
            _ => "Unknown error occured."
        }
    );
}
