namespace TestMarkdig;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();

        string baseDir = AppContext.BaseDirectory;
        string relDir = "../../../../../../TestMarkdig/";

        var richTextBlock = new RichTextBlock();
        MarkdownToRichTextBlockConverter.LoadMarkdownIntoRichTextBlock(richTextBlock, relDir + "testmd.md");
        this.Content = richTextBlock;
    }

   
}
