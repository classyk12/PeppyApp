using Peppy2.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy2.Helpers
{
    public static class Settings
    {
        /// <summary>
        /// This is the Settings static class that can be used in your Core solution or in any
        /// of your client applications. All settings are laid out the same exact way with getters
        /// and setters. 
        /// </summary>

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


        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault("username", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("username", value);
            }
        }

        public static string CleanerId
        {
            get
            {
                return AppSettings.GetValueOrDefault("cleanerId", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("cleanerId", value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("password", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("password", value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("accesstoken", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("accesstoken", value);
            }
        }

        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault("email", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("email", value);
            }
        }

        public static string Date
        {
            get
            {
                return AppSettings.GetValueOrDefault("Date", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Date", value);
            }
        }

        public static string ProfileImagePath
        {
            get
            {
                return AppSettings.GetValueOrDefault("profilepath", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("profilepath", value);
            }
        }

        public static string PhoneNumber
        {
            get
            {
                return AppSettings.GetValueOrDefault("phone", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("phone", value);
            }
        }

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault("userid", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("userid", value);
            }
        }

    }
}
