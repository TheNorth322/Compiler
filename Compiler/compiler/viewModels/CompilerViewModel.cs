using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Compiler.domain.useCases;
using Compiler.utils;

namespace Compiler.compiler.viewModels;

public class CompilerViewModel : ViewModelBase
{
    private ObservableCollection<CompilationErrorViewModel> _compilationErrors;
    private FileUseCase _fileUseCase;
    private EditUseCase _editUseCase;
    private TextUseCase _textUseCase;
    private CompilerUseCase _compilerUseCase;
    private ReferenceUseCase _referenceUseCase;

    public ICommand SelectAllCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand CutCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand CopyCommand { get; }
    public ICommand PutCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand RepeatCommand { get; }
    
    public Action SelectAllButtonClicked { get; set; }
    public Action ExitButtonClicked { get; set; }
    public Action CutButtonClicked { get; set; }
    public Action DeleteButtonClicked { get; set; }
    public Action CopyButtonClicked { get; set; }
    public Action PutButtonClicked { get; set; }
    public Action CancelButtonClicked { get; set; }
    public Action RepeatButtonClicked { get; set; }
    
    
    public CompilerViewModel(FileUseCase fileUseCase, EditUseCase editUseCase, TextUseCase textUseCase,
        CompilerUseCase compilerUseCase, ReferenceUseCase referenceUseCase)
    {
        SelectAllCommand = new RelayCommand<object>(SelectAllExecute);
        ExitCommand = new RelayCommand<object>(ExitExecute);
        CutCommand = new RelayCommand<object>(CutExecute);
        DeleteCommand = new RelayCommand<object>(DeleteExecute);
        CopyCommand = new RelayCommand<object>(CopyExecute);
        PutCommand = new RelayCommand<object>(PutExecute);
        CancelCommand = new RelayCommand<object>(CancelExecute);
        RepeatCommand = new RelayCommand<object>(RepeatExecute);
        
        _fileUseCase = fileUseCase;
        _editUseCase = editUseCase;
        _textUseCase = textUseCase;
        _compilerUseCase = compilerUseCase;
        _referenceUseCase = referenceUseCase;

        _compilationErrors = new ObservableCollection<CompilationErrorViewModel>();
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

    private void CancelExecute(object param)
    {
        CancelButtonClicked?.Invoke(); 
    }

    private void RepeatExecute(object param)
    {
        RepeatButtonClicked?.Invoke();
    }
    private void SelectAllExecute(object param)
    {
        SelectAllButtonClicked?.Invoke();
    }

    private void ExitExecute(object param)
    {
        ExitButtonClicked?.Invoke();
    }

    private void CutExecute(object param)
    {
        CutButtonClicked?.Invoke();
    }

    private void DeleteExecute(object param)
    {
        DeleteButtonClicked?.Invoke();
    }

    private void CopyExecute(object param)
    {
        CopyButtonClicked?.Invoke();
    }

    private void PutExecute(object param)
    {
        PutButtonClicked?.Invoke();
    }
}