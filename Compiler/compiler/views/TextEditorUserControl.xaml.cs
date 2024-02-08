using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Compiler.compiler.viewModels;

namespace Compiler.compiler.views;

public partial class TextEditorUserControl : UserControl
{
    private const double FONT_MAX_SIZE = 60d;
    private const double FONT_MIN_SIZE = 5d;

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

    private void TextEditor_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        bool ctrl = Keyboard.Modifiers == ModifierKeys.Control;
        if (ctrl)
        {
            this.UpdateFontSize(e.Delta > 0);
            e.Handled = true;
        }
    }

    private void UpdateFontSize(bool increase)
    {
        double currentSize = textEditor.FontSize;
        if (increase)
        {
            if (currentSize < FONT_MAX_SIZE)
            {
                double newSize = Math.Min(FONT_MAX_SIZE, currentSize + 1);
                textEditor.FontSize = newSize;
            }
        }
        else
        {
            if (currentSize > FONT_MIN_SIZE)
            {
                double newSize = Math.Max(FONT_MIN_SIZE, currentSize - 1);
                textEditor.FontSize = newSize;
            }
        }
    }
}