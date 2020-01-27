using Xamarin.Forms;
using XamarinNavigation.Views;

namespace XamarinNavigation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("other", typeof(OtherView));
        }
    }
}
