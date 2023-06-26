using System.Text;
using Grynwald.MarkdownGenerator;
using EasyReadme;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

string fileContents = await File.ReadAllTextAsync("README.yaml");
var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
ReadMe readme = deserializer.Deserialize<ReadMe>(fileContents);

var document = new MdDocument();

if (readme.Header.Show)
{
    if (readme.Header.UseShields)
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
        //document.Root.Add(new MdHeading($"<h3 align=\"center\">{readme.Project.Name}</h3>", 1));
    }
    if (readme.Header.UseTagLine)
    {
        document.Root.Add(new MdParagraph(new MdRawMarkdownSpan($"<p align=\"center\">{readme.Project.TagLine}</p>")));
    }
}

foreach (Section section in readme.Sections)
{
}

for (int i = 0; i < readme.Sections.Count; i++)
{
    //if (!readme.Project.IsGitHubProject() && i == 0)
    if (!readme.Header.Show && i == 0)
    {
        document.Root.Add(new MdHeading(readme.Project.Name, 1));
        document.Root.Add(new MdParagraph(readme.Sections[i].Content));
    }
    else
    {
        document.Root.Add(new MdHeading(readme.Sections[i].Heading, 1));
        if (readme.Sections[i].Content != null)
        {
            document.Root.Add(new MdParagraph(readme.Sections[i].Content));
        }
        if (readme.Sections[i].ListContent != null)
        {
            MdBulletList featureList = new MdBulletList();
            foreach (var item in readme.Sections[i].ListContent)
            {
                featureList.Add(new MdListItem(new MdParagraph(item)));
                //document.Root.Add(new MdBulletList(new MdListItem(new MdParagraph(readme.Sections[i].ListContent)), new MdListItem()));
            }
            document.Root.Add(featureList);
        }
    }
}

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

// save document to a file
document.Save("README.md");

