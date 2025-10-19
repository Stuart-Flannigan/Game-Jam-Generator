using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game_Jam_Generator.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _labelText = "Theme Here";

        public string LabelText
        {
            get => _labelText;
            set
            {
                if (_labelText != value)
                {
                    _labelText = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ButtonCommand { get; }

        public MainViewModel()
        {
            ButtonCommand = new RelayCommand(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            string[] themes = {"Space", "Mission Impossible", "Time Travel", "Underwater", "The Realm" };
            Random rand = new Random();
            LabelText = themes[rand.Next(themes.Length)];
            Console.WriteLine("Button clicked!");
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }
        public void Execute(object? parameter)
        {
            _execute();
        }
        public event EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
