using mkdoc.Functions;
using mkdoc.Operators;
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
    $"Gathering hash data for {command} ...".PrintInfo();
    var hashData = await DataHasher.ParseHashData(argMap);
    var templatePath = hashData.TemplatePath;
    $"Reading template {templatePath} ...".PrintInfo();
    var template = await File.ReadAllTextAsync(templatePath);
    var stubble = new StubbleBuilder()
        .Configure(settings =>
        {
            settings.SetIgnoreCaseOnKeyLookup(true);
            settings.SetMaxRecursionDepth(512);
        })
        .Build();
    $"Rendering {command} ...".PrintInfo();
    var output = stubble.Render(template, hashData.Data);
    var outputPath = FileSystemOperator.GetOutputFilePath(command, templatePath, argMap);
    $"Writing to {outputPath} ...".PrintInfo();
    await File.WriteAllTextAsync(outputPath, output, CancellationToken.None);
    "Complete.".PrintInfo();
}
catch (Exception e)
{
    e.PrintError();
    Environment.Exit(1);
}
