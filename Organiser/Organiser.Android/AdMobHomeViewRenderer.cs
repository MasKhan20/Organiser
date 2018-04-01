using Android.Content;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Organiser.View;
using Organiser.Droid;

[assembly: ExportRenderer(typeof(AdMobHomeView), typeof(AdMobHomeViewRenderer))]
namespace Organiser.Droid
{
    public class AdMobHomeViewRenderer : ViewRenderer<AdMobHomeView, AdView>
    {
        public AdMobHomeViewRenderer(Context context) : base(context) { }

        private void CreateView()
        {
            var adView = new AdView(Context)
            {
                AdSize = AdSize.SmartBanner,
                /* Test id = ca-app-pub-3940256099942544/6300978111 */
                AdUnitId = "ca-app-pub-3940256099942544/6300978111" // "ca-app-pub-7755924906506170/4994780118"
            };

            var requestBuilder = new AdRequest.Builder().AddTestDevice(AdRequest.DeviceIdEmulator);
            adView.LoadAd(requestBuilder.Build());

            SetNativeControl(adView);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdMobHomeView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                CreateView();
            }
        }
    }
}