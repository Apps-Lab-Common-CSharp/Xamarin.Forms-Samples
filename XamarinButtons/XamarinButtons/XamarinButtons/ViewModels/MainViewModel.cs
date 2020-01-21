using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace XamarinButtons.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string text;

        public MainViewModel()
        {
            ButtonCommand = new Command<string>(Button);
        }

        public string Text
        {
            get => text;
            set
            {
                if (Equals(text, value)) return;
                text = value;
                OnPropertyChanged();
            }
        }

        public Command<string> ButtonCommand { get; }

        private void Button(string parameter)
        {
            Text = parameter;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}