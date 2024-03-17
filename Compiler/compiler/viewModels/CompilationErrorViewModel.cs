using System.Windows.Controls;
using Compiler.utils;

namespace Compiler.compiler.viewModels;

public class CompilationErrorViewModel : ViewModelBase
{
    private int _index;
    private int _startIndex;
    private int _endIndex;
    private string _expectedLexeme;
    private string _receivedLexeme;
    private string _partToDismiss;
    private string _message;
        
    public CompilationErrorViewModel(int index, int startIndex, int endIndex, string expectedLexeme, string receivedLexeme, string partToDismiss)
    {
        _index = index;
        _startIndex = startIndex;
        _endIndex = endIndex;
        _expectedLexeme = expectedLexeme;
        _receivedLexeme = receivedLexeme;
        _partToDismiss = partToDismiss;
        
        LocalizationProvider localizationProvider = LocalizationProvider.Instance;
        _message = $"{localizationProvider.GetStringByCode("WaitedForText")} {expectedLexeme} ({localizationProvider.GetStringByCode("PartToDismiss")}: {partToDismiss})";
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