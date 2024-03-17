using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using Compiler.data.parser;
using Compiler.domain.entity;
using Compiler.domain.enums;
using Compiler.utils;

namespace Compiler.compiler.viewModels;

public class CompilationErrorViewModel : ViewModelBase
{
    private int _index;
    private int _startIndex;
    private int _endIndex;
    private string _expectedLexeme;
    private string _partToDismiss;
    private string _message;
    public List<ErrorFragment> _errorFragments;
    
    public CompilationErrorViewModel(int index, ParsingError parsingError)
    {
        _index = index;
        
        if (parsingError.ParsingErrorType == ParsingErrorType.Regular)
        {
            _startIndex = parsingError.Errors.First().StartIndex;
            _endIndex = parsingError.Errors.Last().EndIndex;
        }

        _expectedLexeme = parsingError.ExpectedLexeme;
        _errorFragments = parsingError.Errors;
        _partToDismiss = "";
        
        foreach (ErrorFragment fragment in _errorFragments)
        {
            _partToDismiss += $"{fragment.Fragment} ";
        }
        
        LocalizationProvider localizationProvider = LocalizationProvider.Instance;
        _message = $"{localizationProvider.GetStringByCode("WaitedForText")} {_expectedLexeme} ({localizationProvider.GetStringByCode("PartToDismiss")}: {_partToDismiss})";
    }

    public int Index
    {
        get => _index;
        set
        {
            _index = value;
            OnPropertyChanged(nameof(Index));
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

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }
}