using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Organiser.Droid;
using Organiser.View;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdMobViewView), typeof(AdMobViewViewRenderer))]
namespace Organiser.Droid
{
    public class AdMobViewViewRenderer : ViewRenderer<AdMobViewView, AdView>
    {
        public AdMobViewViewRenderer(Context context) : base(context) { }

        private void CreateView()
        {
            var adView = new AdView(Context)
            {
                AdSize = AdSize.SmartBanner,
                /* Test id = ca-app-pub-3940256099942544/6300978111 */
                AdUnitId = "ca-app-pub-3940256099942544/6300978111" //"ca-app-pub-7755924906506170/9448066416"
            };

            var requestBuilder = new AdRequest.Builder().AddTestDevice(AdRequest.DeviceIdEmulator);
            adView.LoadAd(requestBuilder.Build());

            SetNativeControl(adView);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdMobViewView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                CreateView();
            }
        }
    }
}