using System;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Controls.Notifications;

public abstract class BaseCommand : ICommand, INotifyPropertyChanged
{
    public abstract string Header {get;}

    public event EventHandler? CanExecuteChanged;
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChange(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public virtual bool CanExecute(object? parameter) => true;

    public abstract void Execute(object? parameter);
}