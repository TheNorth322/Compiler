using System.Collections.ObjectModel;
using System.Windows.Controls;
using Compiler.utils;

namespace Compiler.compiler.viewModels;

public class CompilerViewModel : ViewModelBase
{
   private ObservableCollection<CompilationErrorViewModel> _compilationErrors;

   public CompilerViewModel()
   {
      _compilationErrors = new ObservableCollection<CompilationErrorViewModel>();
   }

   public ObservableCollection<CompilationErrorViewModel> CompilationErrors
   {
      get => _compilationErrors;
      set
      {
         _compilationErrors = value;
         OnPropertyChanged(nameof(CompilationErrors));
      }
   }
}