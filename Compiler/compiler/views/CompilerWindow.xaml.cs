using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Compiler.compiler.viewModels;
using Compiler.utils;
using ICSharpCode.AvalonEdit.Folding;

namespace Compiler
{
    /// <summary>
    /// Interaction logic for CompilerWindow.xaml
    /// </summary>
    public partial class CompilerWindow : Window
    {
        private CompilerViewModel _vm;
        private ResourceDictionary local;

        public CompilerWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

            local = new ResourceDictionary();
            local.Source = new Uri($"/locals/ru-RU.xaml", UriKind.RelativeOrAbsolute);

            Application.Current.Resources.MergedDictionaries.Add(local);
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _vm.ExitCommand.Execute(null);
            base.OnClosing(e);
        }

        public CompilerWindow(CompilerViewModel compilerViewModel) : this()
        {
            _vm = compilerViewModel;
            this.DataContext = _vm;
            _vm.ExitButtonClicked += OnExitButtonClicked;
            _vm.ShowWantToSaveMessageBox += OnShowWantToSaveMessageBox;
            _vm.ChangeLanguageAction += OnChangeLanguageEvent;
        }

        private void OnChangeLanguageEvent(object? sender, string cultureCode)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

                Application.Current.Resources.MergedDictionaries.Remove(local);
                local = new ResourceDictionary();
                local.Source = new Uri($"/locals/{cultureCode}.xaml", UriKind.RelativeOrAbsolute);

                Application.Current.Resources.MergedDictionaries.Add(local);

                InvalidateVisual();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении локализации: {ex.Message}");
            }
        }

        private void OnShowWantToSaveMessageBox()
        {
            MessageBoxResult result =
                MessageBox.Show("Хотите сохранить изменения?", "Сохранение", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                if (_vm.SelectedTextEditorViewModel.FilePath == "")
                    _vm.SaveAsExecute(this);
                else
                    _vm.SaveExecute(this);
            }
        }

        private void OnExitButtonClicked()
        {
            Application.Current.Shutdown();
        }

        private void Grid_OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void Grid_OnDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void Grid_OnDragLeave(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
        }

        private void Grid_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string file = files[0];
                _vm.OpenFile(file);
            }
        }
    }
}