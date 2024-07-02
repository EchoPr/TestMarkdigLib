using Markdig;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System.IO;
using System.Linq;

public class MarkdownToRichTextBlockConverter
{
    public static void LoadMarkdownIntoRichTextBlock(RichTextBlock richTextBlock, string markdownFilePath)
    {
        var markdown = File.ReadAllText(markdownFilePath);
        var pipeline = new MarkdownPipelineBuilder().Build();
        var document = Markdown.Parse(markdown, pipeline);

        richTextBlock.Blocks.Clear();

        foreach (var block in document)
        {
            if (block is Markdig.Syntax.ParagraphBlock paragraphBlock)
            {
                var paragraph = new Paragraph();
                foreach (var inline in paragraphBlock.Inline)
                {
                    if (inline is Markdig.Syntax.Inlines.LiteralInline literalInline)
                    {
                        paragraph.Inlines.Add(new Run { Text = literalInline.Content.ToString() });
                    }
                }
                richTextBlock.Blocks.Add(paragraph);
            }
            else if (block is Markdig.Syntax.HeadingBlock headingBlock)
            {
                var paragraph = new Paragraph();
                var run = new Run { Text = headingBlock.Inline.FirstChild.ToString() };
                switch (headingBlock.Level)
                {
                    case 1:
                        run.FontSize = 24;
                        break;
                    case 2:
                        run.FontSize = 20;
                        break;
                    case 3:
                        run.FontSize = 18;
                        break;
                }
                paragraph.Inlines.Add(run);
                richTextBlock.Blocks.Add(paragraph);
            }
            
        }
    }
}
