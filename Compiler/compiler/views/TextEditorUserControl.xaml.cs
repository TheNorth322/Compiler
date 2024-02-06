using System.Windows.Controls;
using Compiler.compiler.viewModels;

namespace Compiler.compiler.views;

public partial class TextEditorUserControl : UserControl
{
    public TextEditorUserControl()
    {
        InitializeComponent();
        
    }

    public TextEditorUserControl(TextEditorViewModel textEditorViewModel) : base()
    {
        this.DataContext = textEditorViewModel;
        textEditorViewModel.SelectAllButtonClicked += OnSelectAllButtonClicked;
        textEditorViewModel.CutButtonClicked += OnCutButtonClicked;
        textEditorViewModel.DeleteButtonClicked += OnDeleteButtonClicked;
        textEditorViewModel.CopyButtonClicked += OnCopyButtonClicked;
        textEditorViewModel.PutButtonClicked += OnPasteButtonClicked;
        textEditorViewModel.CancelButtonClicked += OnCancelButtonClicked;
        textEditorViewModel.RepeatButtonClicked += OnRepeatButtonClicked;
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