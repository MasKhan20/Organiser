using Organiser.Data;
using Organiser.Model;
using Organiser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Organiser.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotesPage : ContentPage
	{
        private NotesPageViewModel viewmodel;
        public NotesPage()
        {
            InitializeComponent();
            viewmodel = new NotesPageViewModel(Navigation);

            BindingContext = viewmodel;

            MessagingCenter.Subscribe<NotesPageViewModel, (string title, string message)>(viewmodel, "AlertBox",
                (sender, args) =>
                {
                    DisplayAlert(args.title, args.message, "OK");
                });
        }

        private void MenuView_Clicked(object sender, EventArgs e)
        {
            var s = (MenuItem)sender;

            Navigation.PushAsync(new NoteDetailPage(viewmodel.NotesList, viewmodel.NotesList.IndexOf((Note)s.CommandParameter)));
        }

        private void MenuEdit_Clicked(object sender, EventArgs e)
        {
            var s = (MenuItem)sender;

            Navigation.PushAsync(new AddNotePage(viewmodel.NotesList, viewmodel.NotesList.IndexOf((Note)s.CommandParameter)));
        }

        private async void MenuDelete_Clicked(object sender, EventArgs e)
        {
            var s = (MenuItem)sender;
            var confirm = await DisplayAlert("Warning", "Are you sure you want to delete this note", "YES", "NO");
            if (confirm == false)
            {
                return;
            }

            await App.NoteDataBase.DeleteNoteAsync((Note)s.CommandParameter);

            viewmodel.UpdateListView();

            try
            {
                DependencyService.Get<IMessenger>().LongAlert("Note deleted");
            }
            catch (Exception)
            {
                await DisplayAlert("Success", "Note deleted", "OK");
            }
        }

        private void NotesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            Navigation.PushAsync(new NoteDetailPage(viewmodel.NotesList,
                viewmodel.NotesList.IndexOf((Note)e.SelectedItem)));

            notesList.SelectedItem = null;
        }
    }
}