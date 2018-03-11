using Organiser.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Organiser
{
	public partial class App : Application
	{
        static NoteDataBase noteDataBase;

		public App()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new Organiser.Views.RootMasterPage() { Title = "Organiser" });
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static NoteDataBase NoteDataBase
        {
            get
            {
                if (noteDataBase == null)
                {
                    noteDataBase = new NoteDataBase(
                        DependencyService.Get<IFileHelper>().GetLocalFilePath("NoteSQLite.db3"));
                }
                return noteDataBase;
            }
        }
	}
}
