using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Compiler.domain.entity;
using Compiler.domain.useCases;
using Compiler.utils;
using ICSharpCode.AvalonEdit.Document;

namespace Compiler.compiler.viewModels;

public class TextEditorViewModel : ViewModelBase
{
    private string _syntaxHighlightingCode;
    private ObservableCollection<CompilationErrorViewModel> _compilationErrors;
    private ObservableCollection<LexemeViewModel> _lexemeViewModels;
    private CompilerUseCase _compilerUseCase;

    private int _errorsCount;
    private string _originalFileName;
    private string _fileName;
    private string _filePath;
    private string _fileContents;
    private string _fileExtension;
    private bool _saved;

    private TextDocument _textDocument;
    public Action SelectAllButtonClicked { get; set; }
    public Action CutButtonClicked { get; set; }
    public Action DeleteButtonClicked { get; set; }
    public Action CopyButtonClicked { get; set; }
    public Action PutButtonClicked { get; set; }
    public Action CancelButtonClicked { get; set; }
    public Action RepeatButtonClicked { get; set; }
    
    public EventHandler<TextEditorViewModel> CloseTab { get; set; }
    public EventHandler<int> UpdateErrorsCounter { get; set; }

    public ICommand CloseCommand { get; }

    public TextEditorViewModel()
    {
        _compilationErrors = new ObservableCollection<CompilationErrorViewModel>();
        _lexemeViewModels = new ObservableCollection<LexemeViewModel>();
        CloseCommand = new RelayCommand<object>(CloseExecute);
        _errorsCount = 0;
    }


    public TextEditorViewModel(string fileName, string filePath, string fileExtension, string fileContents,
        CompilerUseCase compilerUseCase) : this()
    {
        _compilerUseCase = compilerUseCase;
        _fileName = fileName;
        _filePath = filePath;
        _fileContents = fileContents;
        _fileExtension = fileExtension;
        _saved = true;
        _originalFileName = fileName;
        GetSyntaxHighlightingCode();
        _textDocument = new TextDocument(fileContents);
    }

    public ObservableCollection<CompilationErrorViewModel> CompilationErrors
    {
        get => _compilationErrors;
        set
        {
            _compilationErrors = value;
            OnPropertyChanged(nameof(CompilationErrors));
        }
    }

    public string SyntaxHighlightingCode
    {
        get => _syntaxHighlightingCode;
        set
        {
            _syntaxHighlightingCode = value;
            ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition(value);
            OnPropertyChanged(nameof(SyntaxHighlightingCode));
        }
    }

    public int ErrorsCount
    {
        get => _errorsCount;
        set
        {
            _errorsCount = value;
            OnPropertyChanged(nameof(ErrorsCount));
        }
    }

    public string Header
    {
        get => _fileName;
        set
        {
            _fileName = value;
            OnPropertyChanged(nameof(Header));
        }
    }

    public string FileContents
    {
        get => _fileContents;
        set
        {
            _fileContents = value;
            OnPropertyChanged(nameof(FileContents));
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

    public bool Saved
    {
        get => _saved;
        set
        {
            _saved = value;
            OnPropertyChanged(nameof(Saved));
        }
    }

    public TextDocument TextDocument
    {
        get => _textDocument;
        set
        {
            _textDocument = value;
            OnPropertyChanged(nameof(TextDocument));
        }
    }

    public ObservableCollection<LexemeViewModel> LexemeViewModels
    {
        get => _lexemeViewModels;
        set
        {
            _lexemeViewModels = value;
            OnPropertyChanged(nameof(LexemeViewModels));
        }
    }

    public EventHandler<string> ChangeLanguageEvent { get; set; }

    public void UpdateCompilationErrors(ObservableCollection<CompilationErrorViewModel> compilationErrorViewModels)
    {
        _compilationErrors = compilationErrorViewModels;
    }

    private void GetSyntaxHighlightingCode()
    {
        switch (_fileExtension)
        {
            case ".cs":
                SyntaxHighlightingCode = "C#";
                break;
            case ".js":
                SyntaxHighlightingCode = "JavaScript";
                break;
            case ".html":
                SyntaxHighlightingCode = "HTML";
                break;
            case ".css":
                SyntaxHighlightingCode = "CSS";
                break;
            case ".cpp":
                SyntaxHighlightingCode = "C++";
                break;
            case ".jar":
                SyntaxHighlightingCode = "Java";
                break;
            default:
                SyntaxHighlightingCode = "";
                break;
        }
    }

    public void FileSaved()
    {
        Saved = true;
        Header = _originalFileName;
    }

    public void FileUnsaved()
    {
        Saved = false;
        Header = "* " + _originalFileName;
    }

    public void StartAnalyzation()
    {
        this._lexemeViewModels.Clear();
        _compilationErrors.Clear();

        List<Lexeme> lexemes = _compilerUseCase.Analyze(FileContents);
        List<ParsingError> errors = _compilerUseCase.Parse(FileContents);

        ErrorsCount = errors.Count;

        foreach (Lexeme lexeme in lexemes)
        {
            _lexemeViewModels.Add(new LexemeViewModel(lexeme));
        }

        for (int i = 0; i < errors.Count; i++)
        {
            _compilationErrors.Add(new CompilationErrorViewModel(i + 1, errors[i].StartIndex,
                errors[i].EndIndex, errors[i].ExpectedLexeme, errors[i].ReceivedLexeme, errors[i].PartToDismiss));
        }

        UpdateErrorsCounter?.Invoke(this, ErrorsCount);
    }

    private void CloseExecute(object obj)
    {
        CloseTab?.Invoke(this, this);
    }
}