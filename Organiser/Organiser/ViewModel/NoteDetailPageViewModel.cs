using Organiser.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Organiser.ViewModel
{
    public class NoteDetailPageViewModel : BaseViewModel
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

        private string _noteReminderTimeOfDay;
        /// <summary>
        /// Displays reminder time
        /// </summary>
        public string NoteReminderTimeOfDay
        {
            get { return _noteReminderTimeOfDay; }
            set
            {
                _noteReminderTimeOfDay = value;
                RaisePropertyChanged(nameof(NoteReminderTimeOfDay));
            }
        }

        private string _noteReminderDate;
        /// <summary>
        /// Displays reminder date
        /// </summary>
        public string NoteReminderDate
        {
            get { return _noteReminderDate; }
            set
            {
                _noteReminderDate = value;
                RaisePropertyChanged(nameof(NoteReminderDate));
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

            NoteReminderDate = Note.ReminderDate.ToShortDateString();
            NoteReminderTimeOfDay = Note.ReminderTimeOfDay.ToString();
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
    }
}
