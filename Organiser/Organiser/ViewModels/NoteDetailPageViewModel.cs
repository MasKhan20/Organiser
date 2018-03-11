using Organiser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Organiser.ViewModels
{
    public class NoteDetailPageViewModel : INotifyPropertyChanged
    {
        #region Binding Commands
        //public ICommand DeleteNoteCommand => new Command(DeleteNote_Command);
        #endregion

        #region Binding Properties
        private string _completeNote;
        public string CompleteNote
        {
            get { return _completeNote; }
            set
            {
                _completeNote = value;
                RaisePropertyChanged(nameof(CompleteNote));
            }
        }

        private bool _noteComplete;
        public bool NoteComplete
        {
            get { return _noteComplete; }
            set
            {
                _noteComplete = value;
                RaisePropertyChanged(nameof(NoteComplete));
            }
        }

        private Note _note;
        public Note Note
        {
            get { return _note; }
            set
            {
                _note = value;
                RaisePropertyChanged(nameof(Note));
            }
        }
        #endregion

        public NoteDetailPageViewModel(Note note)
        {
            Note = note;
            NoteComplete = Note.Complete;
            UpdateStatus(Note.Complete);
        }

        public void CompleteSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            UpdateStatus(e.Value);
        }

        private void UpdateStatus(bool done)
        {
            if (Note == null)
            {
                CompleteNote = "Note not found";
                return;
            }

            CompleteNote = done ? "Task Complete" : "Task Incomplete";

            Note.Complete = done;

            App.NoteDataBase.SaveNoteAsync(Note);
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
