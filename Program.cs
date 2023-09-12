using mkdoc.Functions;
using mkdoc.Operators;
using Stubble.Core.Builders;

static void Timestamp() =>
    $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}".PrintInfo();

try
{
    Timestamp();
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
    Timestamp();
    Environment.Exit(0);
}
catch (Exception e)
{
    e.PrintError();
    Environment.Exit(1);
}
