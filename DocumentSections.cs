using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReadme
{
    public class ReadMe
    {
        public Project Project { get; set; } = new Project();
        public Header Header { get; set; } = new Header();
        public List<Section>? Sections { get; set; }
        public GitHub? GitHub { get; set; }
        public string GenerateTableOfContent()
        {
            int previousLevel = 1;
            var tableOfContent = new StringBuilder();
            tableOfContent.Append($"<details><summary>Table of Contents</summary><ol>");

            if (this.Sections == null) return "";

            foreach (var section in this.Sections)
            {
                if (section.HeadingSize == 1)
                {
                    tableOfContent.Append($"<li>");
                    tableOfContent.Append($"<a href=\"#{section.Heading.ToLower().Replace(" ", "-")}\">{section.Heading}</a>");
                    tableOfContent.Append($"</li>");
                }
                else
                {
                    if (section.HeadingSize == previousLevel)
                    {
                        tableOfContent.Remove(tableOfContent.Length - 5, 5);
                    }
                    else
                    {
                        tableOfContent.Append($"<ul>");
                    }
                    tableOfContent.Append($"<li><a href=\"#{section.Heading.ToLower().Replace(" ", "-")}\">{section.Heading}</a></li>");
                    tableOfContent.Append($"</ul>");
                }
                previousLevel = section.HeadingSize;
            }
            tableOfContent.Append($"</ol></details>");
            if (!this.Project.IsGitHubProject())
            {
                tableOfContent.Append($"<br />");
            }
            return tableOfContent.ToString();
        }
    }

    public class Project
    {
        public string Name { get; set; } = "MyProject";
        public string? LogoUri { get; set; }
        public string? CloudUri { get; set; }
        public string? BuildUri { get; set; }
        public string? DocsUri { get; set; }
        public string? HomePage { get; set; }
        public string? TagLine { get; set; }
        public string ProjectName()
        {
            if (this.CloudUri == null) return "";
            var fullUri = new Uri(this.CloudUri);
            return fullUri.Segments[fullUri.Segments.Count() -1 ];
        }

        public string ProjectOwner()
        {
            if (this.CloudUri == null) return "";
            var fullUri = new Uri(this.CloudUri);
            return fullUri.Segments[fullUri.Segments.Count() -2 ].Replace("/", "");
        }

        public string BuildFile()
        {
            if (this.BuildUri == null)
            {
                return "";
            }
            var fullUri = new Uri(this.BuildUri);
            return fullUri.Segments[fullUri.Segments.Count() -1 ];
        }

        public bool IsGitHubProject()
        {
            if (this.CloudUri == null)
            {
                return false;
            }
            var fullUri = new Uri(this.CloudUri);
            return this.CloudUri.Contains("github.com");
        }
    }

    public class GitHub
    {
        public List<Shield>? Shields { get; set; }
    }

    public class Shield
    {
        public string? Name { get; set; }
        public string? Label { get; set; }
        public bool Show { get; set; }
    }

    public class Header
    {
        public bool Show { get; set; } = false;
        public bool UseShields { get; set; }
        public bool UseLogo { get; set; }
        public bool UseName { get; set; }
        public bool UseTagLine { get; set; }
        public bool UseTableOfContent { get; set; }
    }

    public class Section
    {
        public string Heading { get; set; } = "Section Heading";
        public int HeadingSize { get; set; } = 1;
        public List<ContentBlock>? ContentBlocks { get; set; }
    }

    public class ContentBlock
    {
        public string? Text { get; set; }
        public string? Raw { get; set; }
        public CodeBlock? CodeBlock { get; set; }
        public string? BlockQuote { get; set; }
        public string[]? BulletList { get; set; }
        public string[]? OrderedList { get; set; }
        public Image? Image { get; set; }
    }

    public class CodeBlock
    {
        public string? Content { get; set; }
        public string? Type { get; set; }
    }

    public class Image
    {
        public string? Uri { get; set; }
        public string AltText { get; set; } = "";
        public string Align { get; set; } = "center";
        public int Width { get; set; } = 100;
        public string GenerateMarkup()
        {
            var html = new StringBuilder();
            html.Append($"<div align=\"{this.Align}\">");
            html.Append($"<img src=\"{this.Uri}\" alt=\"{this.AltText}\" width=\"{this.Width}%\">");
            html.Append($"<br /></div>");
            return html.ToString();
        }
    }
}