using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//using Microcharts;
//using Microcharts.Forms;
//using SkiaSharp;

namespace Organiser.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InsightsPage : ContentPage
	{
		public InsightsPage()
		{
            InitializeComponent();

            //var entries = new[]
            //{
            //    new  Microcharts.Entry(200)
            //    {
            //        Label = "January",
            //        ValueLabel = "200",
            //        Color = SKColor.Parse("#266489")
            //    },
            //    new Microcharts.Entry(400)
            //    {
            //        Label = "February",
            //        ValueLabel = "400",
            //        Color = SKColor.Parse("#68B9C0")
            //    },
            //    new Microcharts.Entry(-100)
            //    {
            //        Label = "March",
            //        ValueLabel = "-100",
            //        Color = SKColor.Parse("#90D585")
            //    }
            //};

            //chartView.Chart = new DonutChart()
            //{
            //    Entries = entries,
            //    HoleRadius = 50,

            //};
        }
	}
}