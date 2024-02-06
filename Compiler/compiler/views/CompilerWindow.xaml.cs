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
using ICSharpCode.AvalonEdit.Folding;

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
            compilerViewModel.ExitButtonClicked += OnExitButtonClicked;
        }

        private void OnExitButtonClicked()
        {
            Application.Current.Shutdown();
        }
    }
}