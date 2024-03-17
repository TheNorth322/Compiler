using System.Diagnostics;
using Compiler.domain.repository;

namespace Compiler.data.service;

public class TextService : ITextRepository
{
    public void CallTask(string localization)
    {
        OpenHtmlDocument($"references\\task-{localization}.html");
    }

    public void CallGrammar(string localization)
    {
        OpenHtmlDocument($"references\\grammar-{localization}.html");
    }

    public void CallAnalysisMethod(string localization)
    {
        OpenHtmlDocument($"references\\analysis-method-{localization}.html");
    }

    public void CallNeutralizationMethod(string localization)
    {
        OpenHtmlDocument($"references\\neutralization-{localization}.html");
    }

    public void CallLiterature(string localization)
    {
        OpenHtmlDocument($"references\\literature-{localization}.html");
    }

    public void CallGrammarClassification(string localization)
    {
        OpenHtmlDocument($"references\\grammar-classification-{localization}.html");
    }

    public void CallSourceCode(string localization)
    {
        OpenHtmlDocument($"references\\code-{localization}.html"); 
    }

    private void OpenHtmlDocument(string path)
    {
        Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
    }
}