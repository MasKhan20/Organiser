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
    public class NotesPageViewModel : INotifyPropertyChanged
    {
        #region Binding Commands
        public ICommand AddNewNoteCommand => new Command(AddNewNote_Command);
        public ICommand RefreshCommand => new Command(Refresh_Command);
        //public ICommand ViewNoteCommand => new Command(ViewNote_Command);
        //public ICommand DeleteNoteCommand => new Command(DeleteNote_Command);
        
        #endregion

        #region Binding Properties

        private bool _isRefreshing;
        /// <summary>
        /// Sets refresh animation on ListView
        /// </summary>
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(nameof(IsRefreshing));
            }
        }

        //private Note _selectedNote;
        ///// <summary>
        ///// The selected item in the ListView
        ///// </summary>
        //public Note SelectedNote
        //{
        //    get { return _selectedNote; }
        //    set
        //    {
        //        _selectedNote = value;
        //        RaisePropertyChanged(nameof(SelectedNote));

        //        //only works in code-behind
        //        //HandleSelection();
        //    }
        //}

        private string _listNote;
        /// <summary>
        /// Displays text to tell user ListView is empty
        /// </summary>
        public string ListNote
        {
            get { return _listNote; }
            set
            {
                _listNote = value;
                RaisePropertyChanged(nameof(ListNote));
            }
        }

        /// <summary>
        /// Stores list of notes for ListView
        /// </summary>
        public ObservableCollection<Note> NotesList { get; set; } = new ObservableCollection<Note>();
        #endregion

        private INavigation Navigation;
        public NotesPageViewModel(INavigation navigation)
        {
            Navigation = navigation;

            UpdateListView();
        }

        /// <summary>
        /// Updates contents of ListView
        /// </summary>
        public async void UpdateListView()
        {
            NotesList.Clear();

            List<Note> notes = await App.NoteDataBase.GetNotesAsync();

            if (notes == null || notes.Count == 0)
            {
                ListNote = "No notes created";
                return;
            }

            foreach (Note note in notes)
            {
                NotesList.Add(note);
            }

            ListNote = "";
        }

        /// <summary>
        /// Opens new page to add a note
        /// </summary>
        private async void AddNewNote_Command()
        {
            await Navigation.PushAsync(new AddNotePage(NotesList));

            UpdateListView();
        }

        /// <summary>
        /// Simulates the refreshing of the ListView
        /// </summary>
        private void Refresh_Command()
        {
            IsRefreshing = true;

            UpdateListView();

            IsRefreshing = false;
        }

        //private void ViewNote_Command()
        //{
        //    if (SelectedNote == null)
        //    {
        //        MessagingCenter.Send(this, "AlertBox", ("Error", "No note selected. "));
        //        return;
        //    }

        //    Navigation.PushAsync(new NoteDetailPage(SelectedNote));
        //}

        //private void DeleteNote_Command()
        //{
          
        //    if (SelectedNote == null)
        //    {
        //        MessagingCenter.Send(this, "AlertBox", ("Error", "No note selected. "));
        //        return;
        //    }

        //    App.NoteDataBase.DeleteNoteAsync(SelectedNote);
        //}

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        #endregion
    }
}
