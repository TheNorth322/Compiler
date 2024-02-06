using System.Windows;
using System.Windows.Controls;
using Compiler.compiler.viewModels;

namespace Compiler.compiler.views;

public partial class TextEditorUserControl : UserControl
{
    public TextEditorUserControl()
    {
        InitializeComponent();
    }

    private void TextEditorUserControl_OnLoaded(object sender, RoutedEventArgs e)
    {
        ((this.DataContext as TextEditorViewModel)!).SelectAllButtonClicked += OnSelectAllButtonClicked;
        ((this.DataContext as TextEditorViewModel)!).CutButtonClicked += OnCutButtonClicked;
        ((this.DataContext as TextEditorViewModel)!).DeleteButtonClicked += OnDeleteButtonClicked;
        ((this.DataContext as TextEditorViewModel)!).CopyButtonClicked += OnCopyButtonClicked;
        ((this.DataContext as TextEditorViewModel)!).PutButtonClicked += OnPasteButtonClicked;
        ((this.DataContext as TextEditorViewModel)!).CancelButtonClicked += OnCancelButtonClicked;
        ((this.DataContext as TextEditorViewModel)!).RepeatButtonClicked += OnRepeatButtonClicked;
    }

    private void OnCancelButtonClicked()
    {
        textEditor.Undo();
    }

    private void OnRepeatButtonClicked()
    {
        textEditor.Redo();
    }

    private void OnPasteButtonClicked()
    {
        textEditor.Paste();
    }

    private void OnCopyButtonClicked()
    {
        textEditor.Copy();
    }

    private void OnCutButtonClicked()
    {
        textEditor.Cut();
    }

    private void OnDeleteButtonClicked()
    {
        textEditor.Text = string.Empty;
    }

    private void OnSelectAllButtonClicked()
    {
        textEditor.SelectAll();
    }
}