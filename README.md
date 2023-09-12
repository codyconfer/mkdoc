# mkdoc

mkdoc is a C# console application that templates files with [mustache](https://mustache.github.io/). 

---

## Stack

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET](https://docs.microsoft.com/en-us/dotnet/)
- [Stubble](https://github.com/StubbleOrg/Stubble)
- [Mustache Templates](https://mustache.github.io/)

---

## Getting Started

### From Source

#### Build

1. Install [.NET](https://docs.microsoft.com/en-us/dotnet/core/install)
2. Make directories in the path `~/.mkdoc/src`
3. Clone this repo into `~/.mkdoc/src`
4. Publish the project to `~/.mkdoc`

#### Configure

1. Add `~/.mkdoc/` to your path
2. Set the `TEMPLATE_DIR_PATH` environment variable to the path of your templates directory.

#### Uninstall

1. Remove `~/.mkdoc/` directory
2. Remove from path and environment variables

> #### macOS Quick Setup
>    
> ###### Build
>
>```bash
>brew install dotnet \
>&& mkdir ~/.mkdoc && mkdir ~/.mkdoc/src \
>&& git clone https://github.com/codyconfer/mkdoc.git ~/.mkdoc/src \
>&& cd ~/.mkdoc/src \
>&& dotnet publish mkdoc.csproj -o .. 
>```
>
>###### Configure
>
>```bash
>cd && clear && echo >> ~/.zshrc \
>&& echo '# mkdoc' >> ~/.zshrc \
>&& echo 'export PATH="$PATH:$HOME/.mkdoc/"' >> ~/.zshrc \
>&& echo 'export TEMPLATE_DIR_PATH="$HOME/.mkdoc/templates"' >> ~/.zshrc \
>&& source ~/.zshrc
>```
>
>![launch](https://media.giphy.com/media/3oz8xH9l1ci0ZJFawM/giphy.gif)
>
>###### Uninstall
>
>```bash
>rm -R ~/.mkdoc
>```

---

## Usage

```bash
mkdoc {template-name} /i {path-to-json} /o {output-file-name} -{key} {value}
```

- {template-name} template filename with or without extension REQUIRED
- /i {path-to-json} OPTIONAL (if not specified, hashData is assumed to be located at @templates/{template-name}.hashData.json)
- /o {output-file-name} OPTIONAL (if not specified, output file will be named {template-name})
- -{key} {value} OPTIONAL (if specified, will overwrite values in hashData)

### Adding Templates

Add [mustache](https://mustache.github.io/) templates to `~/.mkdoc/templates`

### Default Hash Data

Add a file named `{template-name}.hashData.json` to `~/.mkdoc/templates`

---

>#### Included Example
> 
> ##### Readme.md
> 
> 
> *template*: `~/.mkdoc/templates/README.md` <br/>
> *data*: `~/.mkdoc/templates/README.hashData.json` <br/>
> *acceptable commands*: 
> ```bash
> mkdoc README
> mkdoc README /i ~/Desktop/README.hashData.json
> mkdoc readme.md
> mkdoc readme.md /o foobar.md -title Foobar
> ```
> 
> ###### Template *README.md*
> 
> ```markdown
> # {{title}}
>
> ## Stack
>
> ---
>
> ## Getting Started
>
> ### From Source
>
> #### Build
>
>
>
> #### Configure
>
>
>
> #### Uninstall
>
>
>
> >#### Quick Setup
> >
> > ###### Build
> >
> >
> >
> > ###### Configure
> >
> >
> >
> > ###### Uninstall
> >
>
> ## Usage
>
> ## Contributing
>
> ```
> 
> ###### Data *README.hashdata.json*
> 
> ```json
> {
>   "title": "Application Name"
> }
> ```
> 
> ##### Tutorial
> 
> 1. Changing default data
>   1. Open `~/.mkdoc/templates/README.hashData.json`
>   2. Change the value of `title` to `tutorial`
>   3. Run `mkdoc README`
>   4. List the files in current directory. You should see a file named `README.md`
>   5. Check the contents of `README.md`. You should see `# tutorial`
> 2. Override default data
>   1. Run `mkdoc README -title Override`
>   2. Check the contents of `README.md`. You should see `# Override`
> 3. Changing the output file name
>   1. Run `mkdoc README /o foobar.md`
>   2. List the files in current directory. You should see a file named `foobar.md`
> 4. Creating a Template
>   1. Create a new file named `tutorial.md` in `~/.mkdoc/templates`
>   2. Copy the contents of `README.md` into `tutorial.md`
>   3. Change `{{title}}` to `{{heading}}` in `tutorial.md`
>   4. Run `mkdoc tutorial -heading "Final Tutorial"`
>   5. List the files in current directory. You should see a file named `tutorial.md`
>   6. Check the contents of `tutorial.md`. You should see `# Final Tutorial`
