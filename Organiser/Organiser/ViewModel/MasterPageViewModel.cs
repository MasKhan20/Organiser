using Organiser.Data;
using Organiser.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Organiser.ViewModel
{
    public class MasterPageViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand ShowInsightsCommand => new Command(ShowInsights_Command);
        public ICommand SettingsCommand => new Command(Settings_Command);
        #endregion

        #region Binding Properties
        private string _userName = Settings.Username;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        private string _userImg = Settings.UserImg;
        public string UserImg
        {
            get { return _userImg; }
            set
            {
                _userImg = value;
                RaisePropertyChanged(nameof(UserImg));
            }
        }

        private string _accountBG = Settings.AccountBG;
        public string AccountBG
        {
            get { return _accountBG; }
            set
            {
                _accountBG = value;
                RaisePropertyChanged(nameof(AccountBG));
            }
        }
        #endregion

        private INavigation Navigation;
        public MasterPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        private async void ShowInsights_Command()
        {
            var notes = await App.NoteDataBase.GetNotesAsync();

            await Navigation.PushAsync(new InsightsPage(notes));
        }

        private async void Settings_Command()
        {
            await Navigation.PushAsync(new SettingsPage(this));
        }
    }
}
