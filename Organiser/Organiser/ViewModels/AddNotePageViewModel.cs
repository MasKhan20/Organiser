using Organiser.Models;
using Organiser.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Organiser.ViewModels
{
    public class AddNotePageViewModel : INotifyPropertyChanged
    {
        #region Binding Commands
        public ICommand SaveNoteCommand => new Command(AddNote_Command);
        #endregion

        #region Binding Properties
        private string _pageTitle;
        /// <summary>
        /// Page title changes as notes title is changed
        /// </summary>
        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                _pageTitle = value;
                RaisePropertyChanged(nameof(PageTitle));
            }
        }

        private string _noteTitle;
        /// <summary>
        /// New note title
        /// </summary>
        public string NoteTitle
        {
            get { return _noteTitle; }
            set
            {
                _noteTitle = value;
                RaisePropertyChanged(nameof(NoteTitle));
            }
        }

        private string _noteDescription;
        /// <summary>
        /// New note description
        /// </summary>
        public string NoteDescription
        {
            get { return _noteDescription; }
            set
            {
                _noteDescription = value;
                RaisePropertyChanged(nameof(NoteDescription));
            }
        }
        #endregion

        private string pageTitle;

        public ObservableCollection<Note> Collection;
        private INavigation Navigation;
        public AddNotePageViewModel(INavigation navigation, ObservableCollection<Note> collection, Note note)
        {
            Collection = collection;
            Navigation = navigation;

            pageTitle = note == null ? "New Note" : "Edit Note";
            PageTitle = pageTitle;

            if (note != null)
            {
                note.Title = NoteTitle;
                note.Description = NoteDescription;
            }
        }

        public void NoteTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(NoteTitle) || String.IsNullOrWhiteSpace(NoteTitle))
            {
                PageTitle = "New Note";
                return;
            }

            PageTitle = NoteTitle;
        }

        /// <summary>
        /// Validates input and saves note
        /// </summary>
        private async void AddNote_Command()
        {
            if (String.IsNullOrEmpty(NoteTitle) || String.IsNullOrWhiteSpace(NoteTitle))
            {
                MessagingCenter.Send(this, "AlertBox", ("Error", "Please enter a title. "));
                return;
            }
            if (String.IsNullOrEmpty(NoteDescription) || String.IsNullOrWhiteSpace(NoteDescription))
            {
                MessagingCenter.Send(this, "AlertBox", ("Error", "Please enter a description. "));
                return;
            }

            Note note = new Note()
            {
                Title = NoteTitle,
                Description = NoteDescription,
            };

            await App.NoteDataBase.SaveNoteAsync(note);

            Collection.Add(note);

            MessagingCenter.Send(this, "AlertBox", ("Success", "Note has been saved. "));

            await Navigation.PopToRootAsync();
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
