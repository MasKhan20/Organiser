using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Organiser.Views
{
	public class RootMasterPage : MasterDetailPage
	{
		public RootMasterPage()
		{
            Master = new MasterPage()
            {
                Title = "Organiser"
            };
            Detail = new SlidePage();
		}
	}
}