using Organiser.Data;
using Organiser.Model;
using Plugin.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Organiser.ViewModel
{
    public class AddNotePageViewModel : BaseViewModel
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

        private bool _noteComplete = false;
        /// <summary>
        /// Old note complete status
        /// </summary>
        public bool NoteComplete
        {
            get { return _noteComplete; }
            set
            {
                _noteComplete = value;
                RaisePropertyChanged(nameof(NoteComplete));
            }
        }

        private TimeSpan _reminderTimeOfDay;
        /// <summary>
        /// Time for reminder notification
        /// </summary>
        public TimeSpan ReminderTimeOfDay
        {
            get { return _reminderTimeOfDay; }
            set
            {
                _reminderTimeOfDay = value;
                RaisePropertyChanged(nameof(ReminderTimeOfDay));
            }
        }

        private DateTime _reminderDate;
        /// <summary>
        /// Date for reminder notification
        /// </summary>
        public DateTime ReminderDate
        {
            get { return _reminderDate; }
            set
            {
                _reminderDate = value;
                RaisePropertyChanged(nameof(ReminderDate));
            }
        }

        private DateTime _minimumDate;
        /// <summary>
        /// Current date to prevent backwards notification
        /// </summary>
        public DateTime MinimumDate
        {
            get { return _minimumDate; }
            set
            {
                if (_minimumDate == value)
                    return;

                _minimumDate = value;
                RaisePropertyChanged(nameof(MinimumDate));
            }
        }
        #endregion

        private string pageTitle;
        private Note UpdatedNote;
        private int NoteIndex;
        public ObservableCollection<Note> Collection;
        private INavigation Navigation;
        public AddNotePageViewModel(INavigation navigation, ObservableCollection<Note> collection, int noteIndex = -1)
        {
            MinimumDate = DateTime.Today;
            ReminderTimeOfDay = DateTime.Now.TimeOfDay;

            Collection = collection;
            Navigation = navigation;
            NoteIndex = noteIndex;
            pageTitle = noteIndex == -1 ? "New Note" : "Edit Note";
            PageTitle = pageTitle;

            if (noteIndex != -1) // editing note, else new
            {
                UpdatedNote = collection[noteIndex];
                NoteTitle = collection[noteIndex].Title;
                NoteDescription = collection[noteIndex].Description;
            }
        }

        public void NoteTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(NoteTitle) || String.IsNullOrWhiteSpace(NoteTitle))
            {
                PageTitle = pageTitle;
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
            // notification
            var timespan = ReminderDate.Add(ReminderTimeOfDay).Subtract(DateTime.Now);
            if (timespan.TotalSeconds <= 0)
            {
                MessagingCenter.Send(this, "AlertBox", ("Error", "Notification time has already gone past"));
                return;
            }

            if (UpdatedNote == null)
            {
                Note note = new Note()
                {
                    Title = NoteTitle,
                    Description = NoteDescription,
                    Complete = false,
                    ReminderTimeOfDay = ReminderTimeOfDay,
                    ReminderDate = ReminderDate,
                };

                await App.NoteDataBase.SaveNoteAsync(note);

                UpdatedNote = note;
                Collection.Add(note);
            }
            else
            {
                UpdatedNote.Title = NoteTitle;
                UpdatedNote.Description = NoteDescription;
                UpdatedNote.ReminderTimeOfDay = ReminderTimeOfDay;
                UpdatedNote.ReminderDate = ReminderDate;

                await App.NoteDataBase.SaveNoteAsync(UpdatedNote);
                Collection[NoteIndex].Title = NoteTitle;
                Collection[NoteIndex].Description = NoteDescription;

                // notification
                //try
                //{
                //    await CrossNotifications.Current.Cancel(UpdatedNote.ID);
                //}
                //catch (Exception exc)
                //{
                //    MessagingCenter.Send(this, "AlertBox", ("Debug Exception", exc.ToString()));
                //}
            }

            try
            {
                CrossNotifications.Current.Vibrate(1000);

                await CrossNotifications.Current.Send(new Notification()
                {
                    Title = "Reminder from Organiser",
                    Message = UpdatedNote.Title,
                    Vibrate = true,
                    When = timespan,
                });
            }
            catch (Exception exc)
            {
                MessagingCenter.Send(this, "AlertBox", ("Debug Exception", exc.ToString()));
            }



            try
            {
                DependencyService.Get<IMessenger>().LongAlert("Note has been saved");
            }
            catch (Exception)
            {
                MessagingCenter.Send(this, "AlertBox", ("Success", "Note has been saved. "));
            }

            await Navigation.PopToRootAsync();


            if (Collection.Count % 2 == 0)
            {
                // Test interstitial = "ca-app-pub-3940256099942544/1033173712"
                DependencyService.Get<IAdmobInterstitial>().Show("ca-app-pub-3940256099942544/1033173712");
                    //("ca -app-pub-7755924906506170/2472444583"); 
            }
        }
    }
}
