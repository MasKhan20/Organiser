using Organiser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Organiser.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
        public MasterPage()
        {
            InitializeComponent();

            var viewmodel = new MasterPageViewModel(Navigation);

            BindingContext = viewmodel;
        }
    }
}