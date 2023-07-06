﻿using System.Text;
using Grynwald.MarkdownGenerator;
using EasyReadme;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

var processYamls = ScanProjectForYamls();

foreach (var file in processYamls)
{
    Console.WriteLine($"Processing {file}");

    string fileContents = await File.ReadAllTextAsync(file);
    var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
    ReadMe readme = deserializer.Deserialize<ReadMe>(fileContents);

    var document = new MdDocument();

    if (readme.Header.Show)
    {
        if (readme.Project.IsGitHubProject() && readme.Header.UseShields)
        {
            if (readme.GitHub != null && readme.GitHub.Shields != null)
            {
                var shields = new StringBuilder();
                foreach (var shield in readme.GitHub.Shields)
                {
                    if (shield.Show)
                    {
                        if (shield.Label != null)
                        {
                            shields.Append($"[![{shield.Label}][{shield.Name}-shield]][{shield.Name}-url]");
                        }
                        else
                        {
                            shields.Append($"[![{shield.Name}][{shield.Name}-shield]][{shield.Name}-url]");
                        }
                    }
                }
                document.Root.Add(new MdParagraph(new MdRawMarkdownSpan(shields.ToString())));
            }
        }
        if (readme.Header.UseLogo)
        {
            var html = new StringBuilder();
            html.Append("<br /><div align=\"center\">");
            html.Append("<br />");
            html.Append($"<a href=\"{readme.Project.CloudUri}\"><img src=\"{readme.Project.LogoUri}\" alt=\"Logo\" width=\"150\"></a>");
            html.Append("</div><br />");
            document.Root.Add(new MdParagraph(new MdRawMarkdownSpan(html.ToString())));
        }
        if (readme.Header.UseName)
        {
            document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"<h1 align=\"center\">{readme.Project.Name}</h1>")));
        }
        if (readme.Header.UseTagLine)
        {
            document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"<p align=\"center\">{readme.Project.TagLine}</p>")));
        }
        if (readme.Header.UseTableOfContent)
        {
            document.Root.Add(new MdParagraph(new MdRawMarkdownSpan(readme.GenerateTableOfContent())));
        }
    }

    if (readme.Sections != null)
    {
        for (int i = 0; i < readme.Sections.Count; i++)
        {
            var section = readme.Sections[i];

            document.Root.Add(new MdHeading(ReplaceVariables(readme.Project, section.Heading), section.HeadingSize));

            if (section.ContentBlocks != null)
            {
                foreach (var contentBlock in section.ContentBlocks)
                {
                    if (contentBlock.Text != null)
                    {
                        document.Root.Add(new MdParagraph(ReplaceVariables(readme.Project, contentBlock.Text)));
                    }
                    if (contentBlock.Raw != null)
                    {
                        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan(ReplaceVariables(readme.Project, contentBlock.Raw))));
                    }
                    if (contentBlock.CodeBlock != null && contentBlock.CodeBlock.Content != null)
                    {
                        document.Root.Add(new MdCodeBlock(contentBlock.CodeBlock.Content, contentBlock.CodeBlock.Type));
                    }
                    if (contentBlock.BlockQuote != null)
                    {
                        document.Root.Add(new MdBlockQuote(new MdRawMarkdownSpan(contentBlock.BlockQuote)));
                    }
                    if (contentBlock.BulletList != null)
                    {
                        MdBulletList list = new MdBulletList();
                        foreach (var item in contentBlock.BulletList)
                        {
                            list.Add(new MdListItem(new MdParagraph(new MdRawMarkdownSpan(ReplaceVariables(readme.Project, item)))));
                        }
                        document.Root.Add(list);
                    }
                    if (contentBlock.OrderedList != null)
                    {
                        MdOrderedList list = new MdOrderedList();
                        foreach (var item in contentBlock.OrderedList)
                        {
                            list.Add(new MdListItem(new MdParagraph(new MdRawMarkdownSpan(ReplaceVariables(readme.Project, item)))));
                        }
                        document.Root.Add(list);
                    }
                    if (contentBlock.Image != null)
                    {
                        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan(contentBlock.Image.GenerateMarkup())));
                    }
                }
            }
        }
    }

    if (readme.Project.IsGitHubProject())
    {
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[version-shield]: https://img.shields.io/nuget/v/{readme.Project.ProjectName()}.svg?style=for-the-badge")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[version-url]: https://www.nuget.org/packages/{readme.Project.ProjectName()}")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[build-shield]: https://img.shields.io/github/actions/workflow/status/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}/{readme.Project.BuildFile()}?branch=main&event=push&label=Build&style=for-the-badge")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[build-url]: https://github.com/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}/actions/workflows/{readme.Project.BuildFile()}?query=branch%3Amain")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[contributors-shield]: https://img.shields.io/github/contributors/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}.svg?style=for-the-badge")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[contributors-url]: https://github.com/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}/graphs/contributors")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[forks-shield]: https://img.shields.io/github/forks/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}.svg?style=for-the-badge")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[forks-url]: https://github.com/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}/network/members")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[stars-shield]: https://img.shields.io/github/stars/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}.svg?style=for-the-badge")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[stars-url]: https://github.com/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}/stargazers")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[issues-shield]: https://img.shields.io/github/issues/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}.svg?style=for-the-badge")));
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"[issues-url]: https://github.com/{readme.Project.ProjectOwner()}/{readme.Project.ProjectName()}/issues")));
    }
    document.Save($"{file.Replace(".yml", ".md").Replace(".yaml", ".md").Replace("readme", "README")}");
}

static string ReplaceVariables(Project project, string content)
{
    return content.Replace("*cloudUri", project.CloudUri)
        .Replace("*name", project.Name)
        .Replace("*docsUri", project.DocsUri)
        .Replace("*homePage", project.HomePage);
}

static List<string> ScanProjectForYamls()
{
    var projectYamls = Directory.EnumerateFiles("./", "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".yml", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase))
            .ToList();

    var processYamls = new List<string>();

    foreach (var file in projectYamls)
    {
        var relPath = Path.GetRelativePath(Directory.GetCurrentDirectory(), file);
        System.Console.WriteLine($"Include ./{Path.GetRelativePath(Directory.GetCurrentDirectory(), file).Replace("\\", "/")}? Enter Y/N");
        string? reply = Console.ReadLine();
        if (reply != null && reply.ToUpper() == "Y")
        {
            processYamls.Add(file.Replace(".YML", ".yml").Replace(".YAML", ".yaml"));
        }
    }
    return processYamls;
}

