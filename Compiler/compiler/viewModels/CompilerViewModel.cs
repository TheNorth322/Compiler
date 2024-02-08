using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Compiler.domain.useCases;
using Compiler.utils;
using FileInfo = Compiler.domain.entity.FileInfo;

namespace Compiler.compiler.viewModels;

public class CompilerViewModel : ViewModelBase
{
    private ObservableCollection<TextEditorViewModel> _textEditorsViewModels;
    private TextEditorViewModel _selectedTextEditor;
    private FileUseCase _fileUseCase;
    private TextUseCase _textUseCase;
    private CompilerUseCase _compilerUseCase;
    private ReferenceUseCase _referenceUseCase;
    private string _currentLanguage;
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
    public ICommand ChangeLanguageCommand { get; }
    public ICommand SaveCommand { get; }
    public Action ExitButtonClicked { get; set; }
    public EventHandler<string> ChangeLanguageAction { get; set; }
    public Action ShowWantToSaveMessageBox { get; set; }

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
        SaveCommand = new RelayCommand<object>(SaveExecute);
        ChangeLanguageCommand = new RelayCommand<object>(ChangeLanguageExecute);

        _fileUseCase = fileUseCase;
        _textUseCase = textUseCase;
        _compilerUseCase = compilerUseCase;
        _referenceUseCase = referenceUseCase;
        _currentLanguage = "ru-RU";
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
        foreach (TextEditorViewModel textEditor in TextEditorViewModels)
        {
            if (textEditor.Saved == false)
            {
                SelectedTextEditorViewModel = textEditor;
                ShowWantToSaveMessageBox?.Invoke();
            }
        }

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
        FileInfo fileInfo = new FileInfo("Новый документ.txt", "", ".txt", "");
        TextEditorViewModel vm = new TextEditorViewModel(fileInfo.FileName, fileInfo.FilePath,
            fileInfo.FileExtension, fileInfo.FileContents);
        _textEditorsViewModels.Add(vm);
        _selectedTextEditor = vm;
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

    public void SaveExecute(object param)
    {
        SaveFile(_selectedTextEditor.FilePath);
        _selectedTextEditor.FileSaved();
    }

    public void SaveAsExecute(object param)
    {
        SaveFileDialog openFileDialog = new SaveFileDialog();
        openFileDialog.ShowDialog();
        SaveFile(openFileDialog.FileName);
        _selectedTextEditor.FileSaved();
    }

    private void SaveFile(string filePath)
    {
        try
        {
            string fileExtension = Path.GetExtension(filePath);

            FileInfo fileInfo = new FileInfo(_selectedTextEditor.Header, filePath, fileExtension,
                _selectedTextEditor.FileContents);

            _fileUseCase.SaveFile(fileInfo);
        }
        catch (Exception e)
        {
            return;
        }
    }

    public void OpenFile(string filePath)
    {
        FileInfo fileInfo = _fileUseCase.OpenFile(filePath);
        TextEditorViewModel vm = new TextEditorViewModel(fileInfo.FileName, fileInfo.FilePath,
            fileInfo.FileExtension, fileInfo.FileContents);
        _textEditorsViewModels.Add(vm);
        _selectedTextEditor = vm;
    }

    public void ChangeLanguageExecute(object param)
    {
        _currentLanguage = (_currentLanguage == "ru-RU") ? "en-US" : "ru-RU";
        foreach (TextEditorViewModel vm in TextEditorViewModels)
        {
            vm.ChangeLanguageEvent?.Invoke(this, _currentLanguage);
        }
        ChangeLanguageAction?.Invoke(this, _currentLanguage);
    }
}