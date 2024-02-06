using System.Windows.Controls;
using Compiler.utils;

namespace Compiler.compiler.viewModels;

public class CompilationErrorViewModel : ViewModelBase
{
    private int _index;
    private string _filePath;
    private int _line;
    private int _column;
    private string _message;

    public CompilationErrorViewModel(int index, string filePath, int line, int column, string message)
    {
        _index = index;
        _filePath = filePath;
        _line = line;
        _column = column;
        _message = message;
        _index = index;
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
    
    public string FilePath
    {
        get => _filePath;
        set
        {
            _filePath = value;
            OnPropertyChanged(nameof(FilePath));
        }
    }

    public int Line
    {
        get => _line;
        set
        {
            _line = value;
            OnPropertyChanged(nameof(Line));
        }
    }

    public int Column
    {
        get => _column;
        set
        {
            _column = value;
            OnPropertyChanged(nameof(Column));
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