using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.compiler.viewModels;

public class LexemeViewModel : ViewModelBase
{
    private int _lineNumber;
    private int _startIndex;
    private int _endIndex;
    private int _lexemeCode;
    private string _lexemeType;
    private string _lexeme;

    public LexemeViewModel(int lineNumber, int startIndex, int endIndex, string lexemeType, string lexeme)
    {
        _lineNumber = lineNumber;
        _startIndex = startIndex;
        _endIndex = endIndex;
        _lexemeType = lexemeType;
        _lexeme = lexeme;
    }

    public LexemeViewModel(Lexeme lexeme)
    {
        _lineNumber = lexeme.LineNumber;
        _startIndex = lexeme.StartIndex;
        _endIndex = lexeme.EndIndex;
        _lexemeCode = (int) lexeme.Type; 
        _lexemeType = ConvertLexemeTypeToString(lexeme.Type);
        _lexeme = lexeme.Text;
    }


    public int LineNumber
    {
        get => _lineNumber;
        set
        {
            _lineNumber = value;
            OnPropertyChanged(nameof(LineNumber));
        }
    }

    public int StartIndex
    {
        get => _startIndex;
        set
        {
            _startIndex = value;
            OnPropertyChanged(nameof(StartIndex));
        }
    }

    public int EndIndex
    {
        get => _endIndex;
        set
        {
            _endIndex = value;
            OnPropertyChanged(nameof(EndIndex));
        }
    }

    public int LexemeCode
    {
        get => _lexemeCode;
        set
        {
            _lexemeCode = value;
            OnPropertyChanged(nameof(LexemeCode));
        }
    }
    public string LexemeTypeText
    {
        get => _lexemeType;
        set
        {
            _lexemeType = value;
            OnPropertyChanged(nameof(LexemeTypeText));
        }
    }
    
    public string Lexeme
    {
        get => _lexeme;
        set
        {
            _lexeme = value;
            OnPropertyChanged(nameof(Lexeme));
        }
    }
    
    private string? ConvertLexemeTypeToString(LexemeType lexemeType)
    {
        LocalizationProvider provider = LocalizationProvider.Instance;
        
        switch (lexemeType)
        {
                case LexemeType.Const:
                case LexemeType.String:
                case LexemeType.Integer:
                case LexemeType.Boolean:
                case LexemeType.True:
                case LexemeType.False:
                    return provider.GetStringByCode("KeyWordHeader");
                case LexemeType.Identifier:
                    return provider.GetStringByCode("IdentifierHeader");
                case LexemeType.Separator:
                    return provider.GetStringByCode("SeparatorHeader");
                case LexemeType.TypeAssignmentOperator:
                    return provider.GetStringByCode("TypeAssignmentOperatorHeader");
                case LexemeType.AssignmentOperator:
                    return provider.GetStringByCode("AssignmentOperatorHeader");
                case LexemeType.StringConstant:
                    return provider.GetStringByCode("StringConstHeader");
                case LexemeType.UnsignedInteger:
                    return provider.GetStringByCode("UnsignedIntegerHeader");
                case LexemeType.EndOfStatement:
                    return provider.GetStringByCode("EndOfExpressionHeader");
                case LexemeType.Invalid:
                    return provider.GetStringByCode("InvalidLexemeHeader");
                default:
                    return provider.GetStringByCode("Error");
        }
    }
}