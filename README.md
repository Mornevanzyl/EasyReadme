[![issues][issues-shield]][issues-url][![stars][stars-shield]][stars-url][![forks][forks-shield]][forks-url]

<br /><div align="center"><br /><a href="https://github.com/Mornevanzyl/EasyReadme"><img src="https://noxorg.dev/docs/images/EasyReadme.png" alt="Logo" width="150"></a></div><br />

<h1 align="center">EasyReadme</h1>

<p align="center">Taking the fuss out of project README files!</p>

<details><summary>Table of Contents</summary><ol><li><a href="#about">About</a></li><li><a href="#key-features">Key Features</a></li><li><a href="#getting-started">Getting started</a></li><ul><li><a href="#prerequisites">Prerequisites</a></li><li><a href="#running-the-application">Running the application</a></li><li><a href="#adding-a-new-section">Adding a new section</a></li><li><a href="#adding-a-list">Adding a list</a></li><li><a href="#adding-a-codeblock">Adding a codeblock</a></li><li><a href="#using-blockquotes-and-images">Using blockquotes and images</a></li><li><a href="#using-a-header">Using a header</a></li><li><a href="#using-github-shields">Using GitHub shields</a></li></ul><li><a href="#acknowledgements">Acknowledgements</a></li><li><a href="#contributing">Contributing</a></li><li><a href="#contact">Contact</a></li></ol></details>

# About

EasyReadme is a lightweight utility that simplifies the task of creating a README.md file for your project repository. It reads a simple YAML configuration file and scaffolds a README file that you can edit.

> 💡 This project `README.md` was built entirely by configuring the `readme.yaml` file in the project root.

# Key Features

- Scaffold your README by configuring a simple YAML file.
- Switch GitHub project shields on/off with little effort.
- Auto-generation of multi-level Table of Content from document sections.
- Single entry project name, logo, URIs (project repo and build file), and tagline.
- Content blocks support normal text, images, raw markdown, code blocks (with syntax types) and lists (both bullet and numbered).
- Compatible with GitHub and Azure DevOps.

# Getting started

## Prerequisites

Make sure you have [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) or later installed.

## Running the application

```powershell
git clone https://github.com/Mornevanzyl/EasyReadme.git
```

Modify the ```readme.yaml``` with descriptors and settings appropriate to your project, or copy one of the starter templates in the [/templates](https://github.com/Mornevanzyl/EasyReadme/tree/main/templates) folder. The graphic below gives an overview of getting started.

<div align="center"><img src="https://noxorg.dev/docs/images/EasyReadme_demo_start.gif" alt="" width="100%"><br /></div>

## Adding a new section

To add an additional section, simply add a new section node with a heading, (optional) heading size and as many `contentBlocks` as required. In the example below we've added a new 'Acknowledgements' section and converted the `text` type to a `raw` type to inject raw markdown which ensures the links are displayed correctly.

<div align="center"><img src="https://noxorg.dev/docs/images/EasyReadme_demo_add_section_raw.gif" alt="" width="100%"><br /></div>

## Adding a list

You can easily add a list by using a `contentBlocks` type `bulletList` or `orderedList` as required. The latter will generate a numbered list.

<div align="center"><img src="https://noxorg.dev/docs/images/EasyReadme_demo_add_list.gif" alt="" width="100%"><br /></div>

## Adding a codeblock

Codeblocks may be added to your README by using a `contentBlocks` type `codeBlock`. The content is added using the `content` property and optional syntax support is specified using the `type` property. The demo below shows how to add a [C#](https://learn.microsoft.com/en-us/dotnet/csharp/) codeblock and then a [Mermaid](https://mermaid.js.org/) codeblock.

> 💡 Remember to add a `|` or `>` after `content` property to ensure multi-line support for strings. Correct indentation is also crucial.

<div align="center"><img src="https://noxorg.dev/docs/images/EasyReadme_demo_add_codeblocks.gif" alt="" width="100%"><br /></div>

## Using blockquotes and images

Blockquotes may be added to your README by using a `contentBlocks` type `blockQuote`. Images are added using a `contentBlocks` type `image`. The image is specified using the `uri` property and optional alt text and width support using the `altText` and `width` properties respectively.

<div align="center"><img src="https://noxorg.dev/docs/images/EasyReadme_demo_add_blockquote_and_image.gif" alt="" width="100%"><br /></div>

## Using a header

Activate a separate and dedicated header by setting the `show` property under the `header` node to `true`. Additional properties like `useLogo`, `useName` and `useTagLine` can also be switched on or off.

> 💡 Remember to populate the associated properties under the `project` node like `logoUri` and `tagLine`.

<div align="center"><img src="https://noxorg.dev/docs/images/EasyReadme_demo_show_header.gif" alt="" width="100%"><br /></div>

## Using GitHub shields

Activate GitHub project shields by setting the `useShields` property under the `header` node to `true`. Configure the specific shields you'd like to display under the dedicated `gitHub` -> `shields` section. The label displayed with the shield can be overridden by specifying an optional `label` property.

You can activate and use the Build shield by setting `show` to true and specifiying the build file under the `buildUri` property under the `project` node.

> 💡 The build file uri is available on GitHub by clicking the `Build & Test` sub-tab under the `Actions` tab. The `.yml` file specified in the url of your browser address is the build file.

<div align="center"><img src="https://noxorg.dev/docs/images/EasyReadme_demo_github_shields.gif" alt="" width="100%"><br /></div>

# Acknowledgements

Thanks to [Andreas Grünwald](https://github.com/ap0llo) for his cool [Markdown Generator](https://github.com/ap0llo/markdown-generator) package.

# Contributing

I just wrote this utility because I got tired copying and pasting previous project READMEs and updating all the links, content ect. I have no doubt that there are probably a gazillion better ways of doing it, but it does the job.

Contributions is what makes the open\-source world go 'round and lets us learn from one another. If you'd like to make a meaningful contribution, please follow the routine below

1. Fork the project
2. Create your feature branch, commit and push your changes
3. Open a pull request

# Contact

Connect on [Twitter](https://twitter.com/catbeatsadrum) or [GitHub](https://github.com/Mornevanzyl)

[version-shield]: https://img.shields.io/nuget/v/EasyReadme.svg?style=for-the-badge

[version-url]: https://www.nuget.org/packages/EasyReadme

[build-shield]: https://img.shields.io/github/actions/workflow/status/Mornevanzyl/EasyReadme/?branch=main&event=push&label=Build&style=for-the-badge

[build-url]: https://github.com/Mornevanzyl/EasyReadme/actions/workflows/?query=branch%3Amain

[contributors-shield]: https://img.shields.io/github/contributors/Mornevanzyl/EasyReadme.svg?style=for-the-badge

[contributors-url]: https://github.com/Mornevanzyl/EasyReadme/graphs/contributors

[forks-shield]: https://img.shields.io/github/forks/Mornevanzyl/EasyReadme.svg?style=for-the-badge

[forks-url]: https://github.com/Mornevanzyl/EasyReadme/network/members

[stars-shield]: https://img.shields.io/github/stars/Mornevanzyl/EasyReadme.svg?style=for-the-badge

[stars-url]: https://github.com/Mornevanzyl/EasyReadme/stargazers

[issues-shield]: https://img.shields.io/github/issues/Mornevanzyl/EasyReadme.svg?style=for-the-badge

[issues-url]: https://github.com/Mornevanzyl/EasyReadme/issues
