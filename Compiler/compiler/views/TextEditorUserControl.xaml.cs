using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Compiler.compiler.viewModels;

namespace Compiler.compiler.views;

public partial class TextEditorUserControl : UserControl
{
    private const double FONT_MAX_SIZE = 60d;
    private const double FONT_MIN_SIZE = 5d;
    private TextEditorViewModel _vm;
    private ResourceDictionary local;

    public TextEditorUserControl()
    {
        /*local = new ResourceDictionary();
        local.Source = new Uri($"/locals/{Thread.CurrentThread.CurrentCulture.Name}.xaml", UriKind.RelativeOrAbsolute);

        Application.Current.Resources.MergedDictionaries.Add(local);*/
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
        ((this.DataContext as TextEditorViewModel)!).ChangeLanguageEvent += OnChangeLanguageEvent;
    }

    private void OnChangeLanguageEvent(object? sender, string cultureCode)
    {
        try
        {
            /*Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
            
            Application.Current.Resources.MergedDictionaries.Remove(local);
            local = new ResourceDictionary();
            local.Source = new Uri($"/locals/{cultureCode}.xaml", UriKind.RelativeOrAbsolute);

            Application.Current.Resources.MergedDictionaries.Add(local);

            InitializeComponent();*/
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при изменении локализации: {ex.Message}");
        }
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

    private void TextEditor_OnTextChanged(object? sender, EventArgs e)
    {
        if (!this.IsLoaded) return;
        ((this.DataContext as TextEditorViewModel)!).FileContents = textEditor.Text;
        if (((this.DataContext as TextEditorViewModel)!).Saved)
            ((this.DataContext as TextEditorViewModel)!).FileUnsaved();
    }
}