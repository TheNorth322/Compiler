using Compiler.data.parser.Interface;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.data.parser.states;

public class ConstState : IState
{
    private IParser _parser;
    private LocalizationProvider _localizationProvider;
    
    public ConstState(IParser parser)
    {
        _parser = parser;
        _localizationProvider = LocalizationProvider.Instance;
    }

    public void Parse(Lexeme lexeme)
    {
        if (lexeme.Type != LexemeType.Const)
        {
           _parser.AddErrorLexeme(new ErrorLexeme(lexeme, _localizationProvider.GetStringByCode("WaitedForText") + " Const")); 
        }
        else
        {
            _parser.SetState(_parser.IdSpaceState);   
        } 
    }
}