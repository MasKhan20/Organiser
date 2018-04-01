using Organiser.Data;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Organiser.ViewModel
{
    public class SettingsPageViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand TakeImgCommand => new Command(TakeImg_Command);
        public ICommand ChangeUserImgCommand => new Command(ChangeUserImg_Command);
        public ICommand ChangeBGImgCommand => new Command(ChangeBGImg_Command);
        public ICommand ResetAllCommand => new Command(ResetAll_Command);
        public ICommand RemoveAdsCommand => new Command(RemoveAds_Command);
        #endregion

        #region MyRegion
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                Instance.UserName = value;
                Settings.Username = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        private string _userImgLabel;
        public string UserImgLabel
        {
            get { return _userImgLabel; }
            set
            {
                _userImgLabel = value;
                RaisePropertyChanged(nameof(UserImgLabel));
            }
        }

        private string _bGImgLabel;
        public string BGImgLabel
        {
            get { return _bGImgLabel; }
            set
            {
                _bGImgLabel = value;
                RaisePropertyChanged(nameof(BGImgLabel));
            }
        }

        private string _purchaseId;
        /// <summary>
        /// Sets the purchase Id 
        /// </summary>
        public string PurchaseId
        {
            get { return _purchaseId; }
            set
            {
                _purchaseId = value;
                RaisePropertyChanged(nameof(PurchaseId));
            }
        }

        private string _purchaseToken;
        /// <summary>
        /// Set purchase token
        /// </summary>
        public string PurchaseToken
        {
            get { return _purchaseToken; }
            set
            {
                _purchaseToken = value;
                RaisePropertyChanged(nameof(PurchaseToken));
            }
        }

        private string _purchaseState;
        /// <summary>
        /// Set purchace state
        /// </summary>
        public string PurchaceState
        {
            get { return _purchaseState; }
            set
            {
                _purchaseState = value;
                RaisePropertyChanged(nameof(PurchaceState));
            }
        }

        private string _purchaseTime;
        public string PurchaseTime
        {
            get { return _purchaseTime; }
            set
            {
                _purchaseTime = value;
                RaisePropertyChanged(nameof(PurchaseTime));
            }
        }
        #endregion

        private const string productId = "removeads";
        private MasterPageViewModel Instance;
        public SettingsPageViewModel(MasterPageViewModel instance)
        {
            Instance = instance;
            InitSettings();

            //Purchases
            PurchaseId = Settings.PurchaseId == String.Empty ? String.Empty : Settings.PurchaseId;
            PurchaseToken = Settings.PurchaseToken == String.Empty ? String.Empty : Settings.PurchaseToken;
            PurchaseTime = Settings.PurchaseTime == String.Empty ? String.Empty : Settings.PurchaseTime;

            PurchaceState = Settings.PurchaseToken == String.Empty ? "Not Purchased" : "Purchased";
        }

        private void InitSettings()
        {
            UserName = Settings.Username;
            UserImgLabel = $"Photo: {Path.GetFileName(Settings.UserImg)}";
            BGImgLabel = $"Background: {Path.GetFileName(Settings.AccountBG)}";
            Instance.UserName = Settings.Username;
            Instance.UserImg = Settings.UserImg;
            Instance.AccountBG = Settings.AccountBG;
        }

        private void DisplayAlert(string title, string message)
        {
            MessagingCenter.Send(this, "AlertBox", (title, message));
        }

        private async void TakeImg_Command()
        {
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    DisplayAlert("Error", "No Camera" + Environment.NewLine + "Taking pictures is not supported");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    Directory = "Pictures",
                    Name = "test.jpg"
                });

                if (file == null)
                {
                    return;
                }

                DisplayAlert("File location", file.Path);
            }
            catch (Exception exc)
            {
                DisplayAlert("Debug Exception", exc.ToString());
            }

            //DisplayAlert("Error", "Taking pictures is not supported");
        }

        private async void ChangeUserImg_Command()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            var img = await CrossMedia.Current.PickPhotoAsync();

            if (img == null)
            {
                return;
            }

            DisplayAlert("Success", "User image has been changed");
            Instance.UserImg = img.Path;
            Settings.UserImg = img.Path;
            UserImgLabel = $"Photo: {Path.GetFileName(Settings.UserImg)}";
        }

        private async void ChangeBGImg_Command()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            var img = await CrossMedia.Current.PickPhotoAsync();

            if (img == null)
            {
                return;
            }

            DisplayAlert("Success", "Background image has been changed");
            Instance.AccountBG = img.Path;
            Settings.AccountBG = img.Path;
            BGImgLabel = $"Background: {Path.GetFileName(Settings.AccountBG)}";
        }

        private void ResetAll_Command()
        {
            Settings.ResetAll();
            InitSettings();

            //DisplayAlert("Values",
            //    $"Username:   {Settings.Username}" + Environment.NewLine +
            //    $"User Image: {Settings.UserImg}" + Environment.NewLine +
            //    $"Account BG: {Settings.AccountBG}");

            DisplayAlert("Success", "Account settings have been reset");
        }

        private async void RemoveAds_Command()
        {
            //DisplayAlert("Stay tuned", "This feature is yet to be implemented");

            //checkif already purchased
            //DisplayAlert("Already Purchased", "You have already payed for Organiser Pro");
            
            if (!CrossInAppBilling.IsSupported)
            {
                DisplayAlert("Error", "In app purchases are not supported");
                return;
            }

            bool? purchased = await WasItemPurchased("removeads");
            if (purchased == null)
            {
                DisplayAlert("Error", "Could not comment" + Environment.NewLine + "Have you connected a Google account?");
                return;
            }
            else if (purchased == true)
            {
                DisplayAlert("Error", "You have aready purchased this");
                return;
            }

            var billing = CrossInAppBilling.Current;
            try
            {
                //

                var connected = await billing.ConnectAsync();

                if (!connected)
                {
                    //Couldn't connect to billing, could be offline, alert user
                    DisplayAlert("Error", "Could not connect" + Environment.NewLine + "Have you connected a Google account?");
                    return;
                }

                //try to purchase item
                var purchase = await CrossInAppBilling.Current.PurchaseAsync(productId, ItemType.InAppPurchase, "removeads");

                if (purchase == null)
                {
                    //Not purchased, alert the user
                }
                else
                {
                    //Purchased, save this information
                    PurchaseId = purchase.Id;
                    PurchaseToken = purchase.PurchaseToken;
                    PurchaceState = "Purchased";
                    PurchaseTime = purchase.TransactionDateUtc.ToLongDateString();

                    //Save settings
                    Settings.PurchaseId = purchase.Id;
                    Settings.PurchaseToken = purchase.PurchaseToken;
                    Settings.PurchaseTime = purchase.TransactionDateUtc.ToLongDateString();
                }
            }
            catch (Exception exc)
            {
                DisplayAlert("Debug Exception", exc.ToString());
            }
            finally
            {
                //Disconnect, it is okay if we never connected
                await CrossInAppBilling.Current.DisconnectAsync();
            }
        }

        #region Billing Methods
        public async Task<bool?> WasItemPurchased(string productId)
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync();

                if (!connected)
                {
                    DisplayAlert("Error", "Could not connect");
                    return null;
                }

                //check purchases
                var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);

                //check for null just incase
                if (purchases?.Any(p => p.ProductId == productId) ?? false)
                {
                    //Purchase restored
                    return true;
                }
                else
                {
                    //no purchases found
                    return false;
                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
                DisplayAlert("Debig Exception", purchaseEx.ToString());
            }
            catch (Exception exc)
            {
                //Something has gone wrong
                DisplayAlert("Debug Exception", exc.ToString());
            }
            finally
            {
                await billing.DisconnectAsync();
            }

            return null;
        }

        public async Task<bool?> PurchaseItem(string productId, string payload)
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync();
                if (!connected)
                {
                    DisplayAlert("Error", "Cannot connect");
                    return null;
                }

                //check purchases
                var purchase = await billing.PurchaseAsync(productId, ItemType.InAppPurchase, payload);

                //possibility that a null came through.
                if (purchase == null)
                {
                    //did not purchase
                }
                else if (purchase.State == PurchaseState.Purchased)
                {
                    //purchased!

                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
                Debug.WriteLine("Error: " + purchaseEx);
            }
            catch (Exception ex)
            {
                //Something else has gone wrong, log it
                Debug.WriteLine("Issue connecting: " + ex);
            }
            finally
            {
                await billing.DisconnectAsync();
            }

            return false;
        }
        #endregion
    }
}
