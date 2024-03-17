namespace Compiler.domain.repository;

public interface ITextRepository
{
    void CallTask(string localization);

    void CallGrammar(string localization);

    void CallAnalysisMethod(string localization);
    
    void CallNeutralizationMethod(string localization);
    
    void CallLiterature(string localization);

    void CallGrammarClassification(string localization);

    void CallSourceCode(string localization);
}