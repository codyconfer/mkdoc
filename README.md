# mkdoc

mkdoc is a C# console application that templates files with [mustache](https://mustache.github.io/). 

## Tech Stack

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET](https://docs.microsoft.com/en-us/dotnet/)
- [Stubble](https://github.com/StubbleOrg/Stubble)
- [Mustache Templates](https://mustache.github.io/)

## Getting Started

### From Source

#### Install

```bash
brew install dotnet \
&& mkdir ~/.mkdoc && mkdir ~/.mkdoc/src \
&& git clone https://github.com/codyconfer/mkdoc.git ~/.mkdoc/src \
&& cd ~/.mkdoc/src \
&& dotnet publish mkdoc.csproj -o .. \
&& cd && clear && echo >> ~/.zshrc \
&& echo '# mkdoc' >> ~/.zshrc \
&& echo 'export PATH="$PATH:$HOME/.mkdoc/"' >> ~/.zshrc \
&& echo 'export TEMPLATE_DIR_PATH="$HOME/.mkdoc/templates"' >> ~/.zshrc \
&& source ~/.zshrc
```

#### Update

```bash
cd ~/.mkdoc/src \
&& git fetch \
&& git pull \
&& dotnet publish -o ..
```

#### Uninstall

```bash
rm -R ~/.mkdoc
```

### Usage

```bash
mkdoc {template-name} /i {path-to-json} /o {output-file-name} -{key} {value}
```

- {template-name} template filename with or without extension REQUIRED
- /i {path-to-json} OPTIONAL (if not specified, hashData is assumed to be located at @templates/{template-name}.hashData.json)
- /o {output-file-name} OPTIONAL (if not specified, output file will be named {template-name})
- -{key} {value} OPTIONAL (if specified, will overwrite values in hashData)

### Adding Templates

Add [mustache](https://mustache.github.io/) templates to `~/.mkdoc/templates`
