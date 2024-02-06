using System.Windows.Controls;

namespace Compiler.utils;

public static class LineNumberDecorator
{
    public static void AddLineNumbers(RichTextBox richTextBox, TextBlock lineNumbers)
    {
        richTextBox.TextChanged += (sender, e) => { UpdateLineNumbers(richTextBox, lineNumbers); };
        UpdateLineNumbers(richTextBox, lineNumbers);
    }

    private static void UpdateLineNumbers(RichTextBox richTextBox, TextBlock lineNumbers)
    {
        var document = richTextBox.Document;
        if (document == null) return;

        string[] lines = richTextBox.Document.ToString()
            .Split("\n");
        lineNumbers.Text = "";
        for (int i = 1; i <= lines.Length; i++)
        {
            lineNumbers.Text += i + "\n";
        }
    }
}