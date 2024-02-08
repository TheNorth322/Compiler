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
        private CompilerViewModel _vm;    
        public CompilerWindow()
        {
            InitializeComponent();
        }

        public CompilerWindow(CompilerViewModel compilerViewModel) : this()
        {
            _vm = compilerViewModel;
            this.DataContext = _vm;
            _vm.ExitButtonClicked += OnExitButtonClicked;
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