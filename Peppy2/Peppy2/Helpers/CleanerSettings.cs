using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy2.Helpers
{
    public static class CleanerSettings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string CleanerName
        {
            get
            {
                return AppSettings.GetValueOrDefault("cleanername", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("cleanername", value);
            }
        }

        public static int CleanerId
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(CleanerId),0);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(CleanerId), value);
            }
        }

        public static string Location
        {
            get
            {
                return AppSettings.GetValueOrDefault("location", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("location", value);
            }
        }

        public static string PhoneNumber
        {
            get
            {
                return AppSettings.GetValueOrDefault("phonenumber", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("phonenumber", value);
            }
        }

     

        public static string Path
        {
            get
            {
                return AppSettings.GetValueOrDefault("Imagepath", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Imagepath", value);
            }
        }

        public static void  ClearAll()
        {
            AppSettings.Clear();
        }
           
    }
}
