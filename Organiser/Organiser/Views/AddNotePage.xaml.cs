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
	public partial class AddNotePage : ContentPage
	{
		public AddNotePage(ObservableCollection<Note> collection, int noteIndex)
		{
			InitializeComponent();
            var viewmodel = new AddNotePageViewModel(Navigation, collection, noteIndex);
            noteTitle.TextChanged += viewmodel.NoteTitle_TextChanged;
            BindingContext = viewmodel;

            MessagingCenter.Subscribe<AddNotePageViewModel, (string title, string message)>(viewmodel, "AlertBox",
                (sender, args) =>
                {
                    DisplayAlert(args.title, args.message, "OK");
                });
		}

        private bool CheckReturn()
        {
            return DisplayAlert("Warning", "Are you sure you want to leave?\nAll changes will be lost. ", "YES", "NO").Result;
        }

        protected override bool OnBackButtonPressed()
        {
            var confirm = CheckReturn();

            if (confirm == true)
            {
                return base.OnBackButtonPressed();
            }

            return false;
        }
    }
}