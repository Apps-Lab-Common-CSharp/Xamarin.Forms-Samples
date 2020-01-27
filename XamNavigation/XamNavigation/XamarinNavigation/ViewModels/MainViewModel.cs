using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinNavigation.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            PressMeCommand = new Command(async obj => await PressMe(obj));
        }

        public Command PressMeCommand { get; }

        private async Task PressMe(object obj)
        {
            await Shell.Current.GoToAsync("other");
        }
    }
}
