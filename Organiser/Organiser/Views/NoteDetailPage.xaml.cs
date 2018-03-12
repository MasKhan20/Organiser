using Organiser.Models;
using Organiser.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Organiser.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NoteDetailPage : ContentPage
	{
        private ObservableCollection<Note> Collection;
        private Note Note;
        private int NoteIndex;
		public NoteDetailPage(ObservableCollection<Note> collection, int noteIndex)
		{
			InitializeComponent();

            Title = noteIndex == -1 ? "Error, note not found" : collection[noteIndex].Title;
            Collection = collection;
            Note = collection[noteIndex];
            NoteIndex = noteIndex;
            var viewmodel = new NoteDetailPageViewModel(collection[noteIndex]);
            completeSwitch.Toggled += viewmodel.CompleteSwitch_Toggled;
            BindingContext = viewmodel;
        }

        private void EditNote_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNotePage(Collection, NoteIndex));
        }

        private async void DeleteNote_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Warning", "Are you sure you want to delete this note", "YES", "NO");
            if (confirm == false)
            {
                return;
            }

            await App.NoteDataBase.DeleteNoteAsync(Note);
            Collection.Remove(Note);

            await Navigation.PopAsync();
            await DisplayAlert("Success", "Note deleted", "OK");
        }
    }
}