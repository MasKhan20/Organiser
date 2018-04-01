using Plugin.InAppBilling.Abstractions;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organiser.Data
{
    public class Settings
    {
        private const string settingsFile = "settings";

        private static ISettings AppSettings
        {
            get
            {
                if (CrossSettings.IsSupported)
                    return CrossSettings.Current;

                return null;
            }
        }

        public static string Username
        {
            get => AppSettings.GetValueOrDefault(nameof(Username), "Username", settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(Username), value, settingsFile);
        }

        public static string UserImg
        {
            get => AppSettings.GetValueOrDefault(nameof(UserImg), "defaultuser.png", settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(UserImg), value, settingsFile);
        }

        public static string AccountBG
        {
            get => AppSettings.GetValueOrDefault(nameof(AccountBG), "accountbg.jpg", settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(AccountBG), value, settingsFile);
        }

        public static bool IsNewUser
        {
            get => AppSettings.GetValueOrDefault(nameof(IsNewUser), true, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(IsNewUser), value, settingsFile);
        }

        #region Purchase Settings
        public static string PurchaseId
        {
            get => AppSettings.GetValueOrDefault(nameof(PurchaseId), String.Empty, settingsFile);
            set => AppSettings.GetValueOrDefault(nameof(PurchaseId), value, settingsFile);
        }

        public static string PurchaseToken
        {
            get => AppSettings.GetValueOrDefault(nameof(PurchaseToken), String.Empty, settingsFile);
            set => AppSettings.GetValueOrDefault(nameof(PurchaseToken), value, settingsFile);
        }

        public static string PurchaseTime
        {
            get => AppSettings.GetValueOrDefault(nameof(PurchaseTime), String.Empty, settingsFile);
            set => AppSettings.GetValueOrDefault(nameof(PurchaseTime), value, settingsFile);
        }
        #endregion

        public static void ResetAll()
        {
            Username = "Username";
            UserImg = "defaultuser.png";
            AccountBG = "accountbg.jpg";
            IsNewUser = true;

            //Cannot use this or will lose payments info
            //AppSettings.Clear(settingsFile);
        }
    }
}
