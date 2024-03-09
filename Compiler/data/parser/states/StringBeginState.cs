using Compiler.data.parser.Interface;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.data.parser.states;

public class StringBeginState : IState
{
    private IParser _parser;
    private LocalizationProvider _localizationProvider;

    public StringBeginState(IParser parser)
    {
        _parser = parser;
        _localizationProvider = LocalizationProvider.Instance;
    }

    public void Parse(Lexeme lexeme)
    {
        if (lexeme.Type != LexemeType.StringSeparator)
        {
            _parser.AddErrorLexeme(new ErrorLexeme(lexeme,
                _localizationProvider.GetStringByCode("WaitedForText") + " \'"));
        }
        else
        {
            _parser.SetState(_parser.StringState);
        }
    }
}