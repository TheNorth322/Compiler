using Compiler.domain.repository;

namespace Compiler.domain.useCases;

public class TextUseCase
{
    private ITextRepository _textRepository;

    public TextUseCase(ITextRepository textRepository)
    {
        _textRepository = textRepository;
    }

    public void CallTask(string localization)
    {
        _textRepository.CallTask(localization); 
    }

    public void CallGrammar(string localization)
    {
        _textRepository.CallGrammar(localization);  
    }

    public void CallAnalysisMethod(string localization)
    {
        _textRepository.CallAnalysisMethod(localization);  
    }

    public void CallNeutralization(string localization)
    {
        _textRepository.CallNeutralizationMethod(localization);    
    }

    public void CallLiterature(string localization)
    {
       _textRepository.CallLiterature(localization); 
    }

    public void CallGrammarClassification(string localization)
    {
        _textRepository.CallGrammarClassification(localization); 
    }
}