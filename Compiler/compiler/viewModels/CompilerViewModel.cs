using System.Collections.ObjectModel;
using System.Windows.Controls;
using Compiler.domain.useCases;
using Compiler.utils;

namespace Compiler.compiler.viewModels;

public class CompilerViewModel : ViewModelBase
{
    private ObservableCollection<CompilationErrorViewModel> _compilationErrors;
    
    private FileUseCase _fileUseCase;
    private EditUseCase _editUseCase;
    private TextUseCase _textUseCase;
    private CompilerUseCase _compilerUseCase;
    private ReferenceUseCase _referenceUseCase;
    
    public CompilerViewModel(FileUseCase fileUseCase, EditUseCase editUseCase, TextUseCase textUseCase,
        CompilerUseCase compilerUseCase, ReferenceUseCase referenceUseCase)
    {
        _fileUseCase = fileUseCase;
        _editUseCase = editUseCase;
        _textUseCase = textUseCase;
        _compilerUseCase = compilerUseCase;
        _referenceUseCase = referenceUseCase;
        
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