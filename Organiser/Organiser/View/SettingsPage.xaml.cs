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
	public partial class SettingsPage : ContentPage
	{
        public SettingsPage(MasterPageViewModel instance)
        {
            InitializeComponent();

            Title = "Settings";

            var viewmodel = new SettingsPageViewModel(instance);
            BindingContext = viewmodel;

            MessagingCenter.Subscribe<SettingsPageViewModel, (string title, string message)>(viewmodel, "AlertBox",
                (s, args) =>
                {
                    DisplayAlert(args.title, args.message, "OK");
                });
        }
    }
}