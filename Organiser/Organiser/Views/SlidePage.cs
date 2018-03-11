using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Organiser.Views
{
	public class SlidePage : CarouselPage
	{
		public SlidePage()
		{
            Children.Add(new NotesPage() { Title = "Notes" });
		}

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}