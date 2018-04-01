using Organiser.Model;
using SkiaSharp;
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
	public partial class InsightsPage : ContentPage
	{
        public InsightsPage(List<Note> notes)
        {
            InitializeComponent();

            Title = "Insights";

            CreateChart(notes);
        }

        private void CreateChart(List<Note> notes)
        {
            var total = notes.Count;
            var completed = 0;

            foreach (Note note in notes)
            {
                if (note.Complete == true)
                {
                    completed++;
                }
            }

            var entries = new[]
            {
                new  Microcharts.Entry(completed)
                {
                    Label = "Tasks completed",
                    ValueLabel = $"{completed} of {total}",
                    Color = SKColor.Parse("#00B2EE")//("#32CD32")
                },
            };

            chartView.Chart = new Microcharts.RadialGaugeChart()
            {
                Entries = entries,
                LineAreaAlpha = 10,

                //IsAnimated = true,
                //AnimationDuration = TimeSpan.FromSeconds(1),
                LabelTextSize = 40,
                MaxValue = total,
            };
        }
    }
}