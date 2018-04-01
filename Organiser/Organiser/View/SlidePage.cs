using Organiser.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Organiser.View
{
    public class SlidePage : CarouselPage
    {
        public SlidePage()
        {
            Title = "Organiser";
            Children.Add(new NotesPage() { Title = "Notes" });
            
            
            //Welcome user if new
            if (Settings.IsNewUser == true)
            {
                //New user

                Navigation.PushModalAsync(new WelcomePage());

                //set in welcome page
                //Settings.IsNewUser = false;
            }
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
