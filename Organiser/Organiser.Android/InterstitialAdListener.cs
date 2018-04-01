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

namespace Organiser.Droid
{
    public class InterstitialAdListener : AdListener
    {
        readonly InterstitialAd _ad;

        public InterstitialAdListener(InterstitialAd ad)
        {
            _ad = ad;
        }

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();

            if (_ad.IsLoaded)
                _ad.Show();
        }
    }
}