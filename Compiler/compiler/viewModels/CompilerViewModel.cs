using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Compiler.domain.entity;
using Compiler.domain.useCases;
using Compiler.utils;

namespace Compiler.compiler.viewModels;

public class CompilerViewModel : ViewModelBase
{
    private ObservableCollection<TextEditorViewModel> _textEditorsViewModels;
    private TextEditorViewModel _selectedTextEditor;
    private FileUseCase _fileUseCase;
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

    public ICommand CreateCommand { get; }
    public ICommand OpenCommand { get; }
    public ICommand SaveAsCommand { get; }
    public Action ExitButtonClicked { get; set; }


    public CompilerViewModel(FileUseCase fileUseCase, TextUseCase textUseCase,
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
        CreateCommand = new RelayCommand<object>(CreateExecute);
        OpenCommand = new RelayCommand<object>(OpenExecute);
        SaveAsCommand = new RelayCommand<object>(SaveAsExecute);

        _fileUseCase = fileUseCase;
        _textUseCase = textUseCase;
        _compilerUseCase = compilerUseCase;
        _referenceUseCase = referenceUseCase;

        _textEditorsViewModels = new ObservableCollection<TextEditorViewModel>();
    }


    public TextEditorViewModel SelectedTextEditorViewModel
    {
        get => _selectedTextEditor;
        set
        {
            _selectedTextEditor = value;
            OnPropertyChanged(nameof(SelectedTextEditorViewModel));
        }
    }

    public ObservableCollection<TextEditorViewModel> TextEditorViewModels
    {
        get => _textEditorsViewModels;
        set
        {
            _textEditorsViewModels = value;
            OnPropertyChanged(nameof(TextEditorViewModels));
        }
    }

    private void CancelExecute(object param)
    {
        SelectedTextEditorViewModel.CancelButtonClicked?.Invoke();
    }

    private void RepeatExecute(object param)
    {
        SelectedTextEditorViewModel.RepeatButtonClicked?.Invoke();
    }

    private void SelectAllExecute(object param)
    {
        SelectedTextEditorViewModel.SelectAllButtonClicked?.Invoke();
    }

    private void ExitExecute(object param)
    {
        ExitButtonClicked?.Invoke();
    }

    private void CutExecute(object param)
    {
        SelectedTextEditorViewModel.CutButtonClicked?.Invoke();
    }

    private void DeleteExecute(object param)
    {
        SelectedTextEditorViewModel.DeleteButtonClicked?.Invoke();
    }

    private void CopyExecute(object param)
    {
        SelectedTextEditorViewModel.CopyButtonClicked?.Invoke();
    }

    private void PutExecute(object param)
    {
        SelectedTextEditorViewModel.PutButtonClicked?.Invoke();
    }

    private void CreateExecute(object param)
    {
    }

    private void OpenExecute(object param)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.ShowDialog();
        if (openFileDialog.CheckFileExists)
        {
            string filePath = openFileDialog.FileName;
            OpenFile(filePath);
        }
    }

    private void SaveAsExecute(object param)
    {
    }

    public void OpenFile(string filePath)
    {
        FileInfo fileInfo = _fileUseCase.OpenFile(filePath);
        TextEditorViewModel vm = new TextEditorViewModel(fileInfo.FileName, fileInfo.FilePath,
            fileInfo.FileExtension, fileInfo.FileContents);
        _textEditorsViewModels.Add(vm);
        _selectedTextEditor = vm;
    }
}