using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Compiler.domain.useCases;
using Compiler.utils;
using FileInfo = Compiler.domain.entity.FileInfo;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Compiler.compiler.viewModels;

public class CompilerViewModel : ViewModelBase
{
    private ObservableCollection<TextEditorViewModel> _textEditorsViewModels;
    private TextEditorViewModel _selectedTextEditor;
    private FileUseCase _fileUseCase;
    private TextUseCase _textUseCase;
    private CompilerUseCase _compilerUseCase;
    private ReferenceUseCase _referenceUseCase;
    private LocalizationProvider _localizationProvider;

    public ICommand SelectAllCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand CutCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand CopyCommand { get; }
    public ICommand PutCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand RepeatCommand { get; }

    public ICommand OpenExampleCommand { get; }
    public ICommand CallReferenceCommand { get; }
    public ICommand CreateCommand { get; }
    public ICommand OpenCommand { get; }
    public ICommand SaveAsCommand { get; }
    public ICommand ChangeLanguageCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CallProgramDescriptionCommand { get; }

    public ICommand StartAnalyzationCommand { get; }

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
        CallReferenceCommand = new RelayCommand<object>(CallReferenceExecute);
        CallProgramDescriptionCommand = new RelayCommand<object>(CallProgramDescription);
        StartAnalyzationCommand = new RelayCommand<object>(StartAnalyzationExecute);
        OpenExampleCommand = new RelayCommand<object>(OpenExampleExecute);

        _fileUseCase = fileUseCase;
        _textUseCase = textUseCase;
        _compilerUseCase = compilerUseCase;
        _referenceUseCase = referenceUseCase;
        _textEditorsViewModels = new ObservableCollection<TextEditorViewModel>();
        _localizationProvider = LocalizationProvider.Instance;

        CreateExecute(this);
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
        try
        {
            if (SelectedTextEditorViewModel != null && !SelectedTextEditorViewModel.Saved)
            {
                ShowWantToSaveMessageBox?.Invoke();
            }

            string header = _localizationProvider.GetStringByCode("NewDocumentHeader");

            FileInfo fileInfo = new FileInfo(header, "", ".txt", "");
            TextEditorViewModel vm = new TextEditorViewModel(fileInfo.FileName, fileInfo.FilePath,
                fileInfo.FileExtension, fileInfo.FileContents, _compilerUseCase);
            _textEditorsViewModels.Add(vm);
            _selectedTextEditor = vm;
        }
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OpenExecute(object param)
    {
        try
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult value = openFileDialog.ShowDialog();

            switch (value)
            {
                case DialogResult.Abort:
                case DialogResult.Cancel:
                case DialogResult.No:
                    return;
                default:
                    break;
            }

            if (openFileDialog.CheckFileExists)
            {
                string filePath = openFileDialog.FileName;
                OpenFile(filePath);
            }
        }
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void SaveExecute(object param)
    {
        try
        {
            SaveFile(_selectedTextEditor.FilePath);
            _selectedTextEditor.FileSaved();
        }
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void SaveAsExecute(object param)
    {
        try
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            switch (result)
            {
                case DialogResult.Abort:
                case DialogResult.Cancel:
                case DialogResult.No:
                    return;
                default:
                    SaveFile(openFileDialog.FileName);
                    _selectedTextEditor.FileSaved();
                    return;
            }
        }
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void StartAnalyzationExecute(object param)
    {
        _selectedTextEditor.StartAnalyzation();
    }

    public void OpenFile(string filePath)
    {
        try
        {
            FileInfo fileInfo = _fileUseCase.OpenFile(filePath);
            TextEditorViewModel vm = new TextEditorViewModel(fileInfo.FileName, fileInfo.FilePath,
                fileInfo.FileExtension, fileInfo.FileContents, _compilerUseCase);
            _textEditorsViewModels.Add(vm);
            _selectedTextEditor = vm;
        }
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void ChangeLanguageExecute(object param)
    {
        try
        {
            _localizationProvider.CurrentLocalizationCode =
                (_localizationProvider.CurrentLocalizationCode == "ru-RU") ? "en-US" : "ru-RU";
            foreach (TextEditorViewModel vm in TextEditorViewModels)
            {
                vm.ChangeLanguageEvent?.Invoke(this, _localizationProvider.CurrentLocalizationCode);
            }

            ChangeLanguageAction?.Invoke(this, _localizationProvider.CurrentLocalizationCode);
        }
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CallReferenceExecute(object param)
    {
        try
        {
            _referenceUseCase.CallReference(_localizationProvider.CurrentLocalizationCode);
        }
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CallProgramDescription(object param)
    {
        try
        {
            _referenceUseCase.CallProgramDescription(_localizationProvider.CurrentLocalizationCode);
        }
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OpenExampleExecute(object obj)
    {
        try
        {
            string path = "example/example.txt";
            OpenFile(path); 
        }
        catch (Exception ex)
        {
            string message = _localizationProvider.GetStringByCode("ExceptionMessage");
            string header = _localizationProvider.GetStringByCode("ExceptionHeader");

            MessageBox.Show(message + ex.Message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}