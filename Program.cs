using mkdoc.Functions;
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
    var argMap = ArgMapper.ParseArgMap(args);
    var command = argMap.Command;
    var hashData = await DataHasher.ParseHashData(argMap);
    var templatePath = hashData.TemplatePath;
    var template = await File.ReadAllTextAsync(templatePath);
    var stubble = new StubbleBuilder()
        .Configure(settings =>
        {
            settings.SetIgnoreCaseOnKeyLookup(true);
            settings.SetMaxRecursionDepth(512);
        })
        .Build();
    var output = stubble.Render(template, hashData.Data);
    await File.WriteAllTextAsync(
        FileSystemOperator.GetOutputFilePath(command, templatePath),
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
            ApplicationException => e.Message,
            _ => "Unknown error occured."
        }
    );
}
