using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Compiler
{
    /// <summary>
    /// Interaction logic for CompilerWindow.xaml
    /// </summary>
    public partial class CompilerWindow : Window
    {
        public CompilerWindow()
        {
            InitializeComponent();
        }

        public CompilerWindow(CompilerViewModel compilerViewModel) : base()
        {
            this.DataContext = compilerViewModel;

            compilerViewModel.SelectAllButtonClicked += OnSelectAllButtonClicked;
            compilerViewModel.ExitButtonClicked += OnExitButtonClicked;
            compilerViewModel.CutButtonClicked += OnCutButtonClicked;
            compilerViewModel.DeleteButtonClicked += OnDeleteButtonClicked;
            compilerViewModel.CopyButtonClicked += OnCopyButtonClicked;
            compilerViewModel.PutButtonClicked += OnPasteButtonClicked;
            compilerViewModel.CancelButtonClicked += OnCancelButtonClicked;
            compilerViewModel.RepeatButtonClicked += OnRepeatButtonClicked;
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

        private void OnExitButtonClicked()
        {
            Application.Current.Shutdown();
        }
    }
}