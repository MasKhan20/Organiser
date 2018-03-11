using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Organiser.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
		public MasterPage()
		{
			InitializeComponent();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InsightsPage());
        }
    }
}