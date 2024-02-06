using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Compiler.utils;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ViewModelBase()
    {
    }
    
       
    protected virtual void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void Dispose()
    {
    }
}