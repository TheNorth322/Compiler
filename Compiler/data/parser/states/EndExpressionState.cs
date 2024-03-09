using Compiler.data.parser.Interface;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.data.parser.states;

public class EndExpressionState : IState
{
    private IParser _parser;
    private LocalizationProvider _localizationProvider;

    public EndExpressionState(IParser parser)
    {
        _parser = parser;
        _localizationProvider = LocalizationProvider.Instance;
    }

    public void Parse(Lexeme lexeme)
    {
        if (lexeme.Type != LexemeType.EndOfStatement)
        {
            _parser.AddErrorLexeme(new ErrorLexeme(lexeme,
                _localizationProvider.GetStringByCode("WaitedForText") + " ;"));
        }
        else
        {
            _parser.SetState(_parser.ConstState);
        }
    }
}