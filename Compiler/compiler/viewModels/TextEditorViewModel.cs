using System;
using System.Collections.ObjectModel;
using Compiler.domain.entity;
using Compiler.utils;
using ICSharpCode.AvalonEdit.Document;

namespace Compiler.compiler.viewModels;

public class TextEditorViewModel : ViewModelBase
{
    private string _syntaxHighlightingCode;
    private ObservableCollection<CompilationErrorViewModel> _compilationErrors;

    private string _fileName;
    private string _filePath;
    private string _fileContents;
    private string _fileExtension;
    private TextDocument _textDocument;
    public Action SelectAllButtonClicked { get; set; }
    public Action CutButtonClicked { get; set; }
    public Action DeleteButtonClicked { get; set; }
    public Action CopyButtonClicked { get; set; }
    public Action PutButtonClicked { get; set; }
    public Action CancelButtonClicked { get; set; }
    public Action RepeatButtonClicked { get; set; }

    public TextEditorViewModel()
    {
        _compilationErrors = new ObservableCollection<CompilationErrorViewModel>();
    }

    public TextEditorViewModel(string fileName, string filePath, string fileExtension, string fileContents) : this()
    {
        _fileName = fileName;
        _filePath = filePath;
        _fileContents = fileContents;
        _fileExtension = fileExtension;
        _textDocument = new TextDocument(fileContents); 
        GetSyntaxHighlightingCode();
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
            OnPropertyChanged(nameof(SyntaxHighlightingCode));
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

    public TextDocument TextDocument
    {
        get => _textDocument;
        set
        {
            _textDocument = value;
            OnPropertyChanged(nameof(TextDocument));
        }
    }
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
                SyntaxHighlightingCode = "C+";
                break;
            case ".jar":
                SyntaxHighlightingCode = "Java";
                break;
            default:
                SyntaxHighlightingCode = "";
                break;
        }
    }
}