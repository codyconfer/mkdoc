# mkdoc

mkdoc is a C# console application that templates files with mustache. 

## Tech Stack

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET](https://docs.microsoft.com/en-us/dotnet/)
- [Stubble](https://github.com/StubbleOrg/Stubble)
- [Mustache Templates](https://mustache.github.io/)

## Getting Started

### Installation

//TODO working on workflows

### Usage

```bash
mkdoc {template-name} -hashdata.json {path-to-json} -{key} {value}
```

- {template-name} REQUIRED
- -hashdata.json {path-to-json} OPTIONAL (if not specified, hashData is assumed to be located at @templates/{template-name}.hashData)
- -{key} {value} OPTIONAL (if specified, will overwrite values in hashData)

### Building from Source

```bash
dotnet publish -o ./dist mkdoc.csproj
```
