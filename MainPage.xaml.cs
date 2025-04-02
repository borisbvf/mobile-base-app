using System.Windows.Input;

namespace BaseMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public ICommand SettingsCommand => new Command(GoToSettings);
        private async void GoToSettings()
        {
            await Shell.Current.GoToAsync($"{Constants.SettingsPageRoute}");
        }
    }

}
