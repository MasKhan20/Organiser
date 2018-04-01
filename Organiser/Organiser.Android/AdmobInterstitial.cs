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
using Organiser.Data;
using Organiser.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdmobInterstitial))]

namespace Organiser.Droid
{
    public class AdmobInterstitial : IAdmobInterstitial
    {
        InterstitialAd _ad;

        public void Show(string adUnit)
        {
            var context = Android.App.Application.Context;
            _ad = new InterstitialAd(context)
            {
                AdUnitId = adUnit
            };

            var intlistener = new InterstitialAdListener(_ad);
            intlistener.OnAdLoaded();
            _ad.AdListener = intlistener;

            var requestbuilder = new AdRequest.Builder();
            _ad.LoadAd(requestbuilder.Build());
        }
    }
}