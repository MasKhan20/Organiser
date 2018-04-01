using Organiser.Data;
using Organiser.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Organiser.ViewModel
{
    public class WelcomePageViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand SkipIntroCommand => new Command(SkipIntro_Command);
        public ICommand BackPageCommand => new Command(BackPage_Command);
        public ICommand NextPageCommand => new Command(NextPage_Command);
        public ICommand DoneCommand => new Command(Done_Command);
        #endregion

        #region Binding Properties
        private string _newUsername;
        public string NewUsername
        {
            get { return _newUsername; }
            set
            {
                _newUsername = value;
                RaisePropertyChanged(nameof(NewUsername));
            }
        }
        #endregion

        private int currentPage = 0;

        private INavigation Navigation;
        private WelcomePage Instance;
        public WelcomePageViewModel(INavigation navigation, WelcomePage instance)
        {
            Navigation = navigation;
            Instance = instance;
        }

        private void SkipIntro_Command()
        {
            Navigation.PopModalAsync();
        }

        private void BackPage_Command()
        {
            if (currentPage < -1)
                return;

            currentPage--;

            Instance.CurrentPage = Instance.Children[currentPage];
        }

        private void NextPage_Command()
        {
            if (currentPage > 4)
                return;

            currentPage++;

            Instance.CurrentPage = Instance.Children[currentPage];
        }

        private void Done_Command()
        {
            Navigation.PopModalAsync();
            Settings.Username = String.IsNullOrEmpty(NewUsername) || String.IsNullOrWhiteSpace(NewUsername) ? "Username" : NewUsername;
            Settings.IsNewUser = false;
        }
    }
}
