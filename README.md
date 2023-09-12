# mkdoc

mkdoc is a C# console application that templates files with mustache. 

## Tech Stack

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET](https://docs.microsoft.com/en-us/dotnet/)
- [Stubble](https://github.com/StubbleOrg/Stubble)
- [Mustache Templates](https://mustache.github.io/)

## Getting Started

### From Source

#### Installation

```bash
brew install dotnet \
&& mkdir ~/.mkdoc && mkdir ~/.mkdoc/src \
&& git clone https://github.com/codyconfer/mkdoc.git ~/.mkdoc/src \
&& cd ~/.mkdoc/src \
&& dotnet publish -o .. \
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

### Usage

```bash
mkdoc {template-name} -hashdata.json {path-to-json} -{key} {value}
```

- {template-name} REQUIRED
- -hashdata.json {path-to-json} OPTIONAL (if not specified, hashData is assumed to be located at @templates/{template-name}.hashData)
- -{key} {value} OPTIONAL (if specified, will overwrite values in hashData)
