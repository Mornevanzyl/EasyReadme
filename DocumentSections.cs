using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReadme
{
    public class ReadMe
    {
        public Project Project { get; set; }
        public Header Header { get; set; }
        public List<Section> Sections { get; set; }
        public GitHub GitHub { get; set; }
        public string GenerateTableOfContent()
        {
            int previousLevel = 1;
            var tableOfContent = new StringBuilder();
            tableOfContent.Append($"<details><summary>Table of Contents</summary><ol>");
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
            tableOfContent.Append($"</ol></details><br />");
            return tableOfContent.ToString();
        }
    }

    public class Project
    {
        public string Name { get; set; }
        public string LogoUri { get; set; }
        public string CloudUri { get; set; }
        public string BuildUri { get; set; }
        public string? TagLine { get; set; }
        public string ProjectName()
        {
            var fullUri = new Uri(this.CloudUri);
            return fullUri.Segments[fullUri.Segments.Count() -1 ];
        }

        public string ProjectOwner()
        {
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
            var fullUri = new Uri(this.CloudUri);
            return this.CloudUri.Contains("github.com");
        }
    }

    public class GitHub
    {
        public List<Shield> Shields { get; set; }
    }

    public class Shield
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public bool Show { get; set; }
    }

    public class Header
    {
        public bool Show { get; set; }
        public bool UseShields { get; set; }
        public bool UseLogo { get; set; }
        public bool UseName { get; set; }
        public bool UseTagLine { get; set; }
        public bool UseTableOfContent { get; set; }
    }

    public class Section
    {
        public string Heading { get; set; }
        public int HeadingSize { get; set; } = 1;
        public List<ContentBlock> ContentBlocks { get; set; }
    }

    public class ContentBlock
    {
        public string Text { get; set; }
        public string Raw { get; set; }
        public string CodeBlock { get; set; }
        public string[] List { get; set; }
    }
}