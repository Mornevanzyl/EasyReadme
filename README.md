[![issues][issues-shield]][issues-url][![stars][stars-shield]][stars-url][![forks][forks-shield]][forks-url]

<br /><div align="center"><br /><a href="https://github.com/Mornevanzyl/EasyReadme"><img src="https://noxorg.dev/docs/images/EasyReadme.png" alt="Logo" width="150"></a></div><br />

<h1 align="center">EasyReadme</h1>

<p align="center">Taking the fuss out of project README files!</p>

<details><summary>Table of Contents</summary><ol><li><a href="#about">About</a></li><li><a href="#key-features">Key Features</a></li><li><a href="#getting-started">Getting started</a></li><ul><li><a href="#prerequisites">Prerequisites</a></li><li><a href="#running-the-application">Running the application</a></li></ul><li><a href="#acknowledgements">Acknowledgements</a></li><li><a href="#contributing">Contributing</a></li><li><a href="#contact">Contact</a></li></ol></details>

# About

EasyReadme is a lightweight utility that simplifies the task of creating a README.md file for your project repository. It reads a simple YAML configuration file and scaffolds a README file that you can edit.

> 💡 This project `README.md` was built entirely by configuring the `readme.yaml` file in the project root.

# Key Features

- Scaffold your README by configuring a simple YAML file.
- Switch GitHub project shields on/off with little effort.
- Auto-generation of multi-level Table of Content from document sections.
- Single entry project URIs and info.
- Content blocks support normal text, images, raw markdown, code blocks and lists (both bullet and numbered).
- Compatible with GitHub and Azure DevOps.

# Getting started

## Prerequisites

Make sure you have [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) or later installed.

## Running the application

```
git clone https://github.com/Mornevanzyl/EasyReadme.git
```

Modify the ```readme.yaml``` with descriptors and settings appropriate to your project.

```
dotnet run
```

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
