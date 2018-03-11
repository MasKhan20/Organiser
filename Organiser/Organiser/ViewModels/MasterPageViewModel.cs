using Organiser.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Organiser.ViewModels
{
    public class MasterPageViewModel : INotifyPropertyChanged
    {
        #region Binding Commands
        public ICommand ShowInsightsCommand => new Command(ShowInsights_Command);
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

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        #endregion
    }
}
