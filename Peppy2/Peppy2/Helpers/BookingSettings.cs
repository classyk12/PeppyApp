using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy2.Helpers
{
   public class BookingSettings
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

        public static string BookingId
        {
            get => AppSettings.GetValueOrDefault("BookingId", SettingsDefault);

            set
            {
                AppSettings.AddOrUpdateValue("BookingId", value);
            }
        }

        public static string ServiceType
        {
            get => AppSettings.GetValueOrDefault("servicetype", SettingsDefault);
           
            set
            {
                AppSettings.AddOrUpdateValue("servicetype", value);
            }
        }

        public static string StartDate
        {
            get => AppSettings.GetValueOrDefault("startdate", SettingsDefault);

            set
            {
                AppSettings.AddOrUpdateValue("startdate" , value);
            }
        }
        public static string EndDate
        {
            get => AppSettings.GetValueOrDefault("enddate", SettingsDefault);

            set
            {
                AppSettings.AddOrUpdateValue("enddate", value);
            }
        }
        public static double ServiceDuration
        {
            get => AppSettings.GetValueOrDefault(nameof(ServiceDuration), 0.0);

            set
            {
                AppSettings.AddOrUpdateValue(nameof(ServiceDuration), value);
            }
        }
        public static string ServiceTime
        {
            get => AppSettings.GetValueOrDefault("servicetime", SettingsDefault);

            set
            {
                AppSettings.AddOrUpdateValue("servicetime", value);
            }
        }
        public static string ExtraDescription
        {
            get => AppSettings.GetValueOrDefault("extradescription", SettingsDefault);

            set
            {
                AppSettings.AddOrUpdateValue("extradescription", value);
            }
        }
        public static bool IsNeedIroning
        {
            get => AppSettings.GetValueOrDefault(nameof(IsNeedIroning), false);
            set
            {
                AppSettings.AddOrUpdateValue(nameof(IsNeedIroning), value);
            }
        }
        public static bool IsNeedCleaningMaterials
        {
            get => AppSettings.GetValueOrDefault(nameof(IsNeedCleaningMaterials), false);
            set
            {
                AppSettings.AddOrUpdateValue(nameof(IsNeedCleaningMaterials), value);
            }
        }
        public static bool IsHavePets
        {
            get => AppSettings.GetValueOrDefault(nameof(IsHavePets), false);
            set
            {
                AppSettings.AddOrUpdateValue(nameof(IsHavePets), value);
            }
        }
        public static string ModeOfEntry
   
     {
            get => AppSettings.GetValueOrDefault("entry", SettingsDefault);
            set
            {
                AppSettings.AddOrUpdateValue("entry", value);
            }
        }
        public static string PaymentMethod
        {
            get => AppSettings.GetValueOrDefault("paymentmethod", SettingsDefault);
            set
            {
                AppSettings.AddOrUpdateValue("paymentmethod", value);
            }
        }
        public static string Street


        {
            get => AppSettings.GetValueOrDefault("street", SettingsDefault);
            set
            {
                AppSettings.AddOrUpdateValue("street", value);
            }
        }
        public static string HomeNumber

        {
            get => AppSettings.GetValueOrDefault("homenumber", SettingsDefault);
            set
            {
                AppSettings.AddOrUpdateValue("homenumber", value);
            }
        }
        public static string City

        {

            get => AppSettings.GetValueOrDefault("city", SettingsDefault);
            set
            {
                AppSettings.AddOrUpdateValue("city", value);
            }
        }
        public static string DirectionsOrLandmarks

        {
            get => AppSettings.GetValueOrDefault("directions", SettingsDefault);
            set
            {
                AppSettings.AddOrUpdateValue("directions", value);
            }
        }
        public static string TotalCost

        {
            get => AppSettings.GetValueOrDefault("totalcost", SettingsDefault);
            set
            {
                AppSettings.AddOrUpdateValue("totalcost" , value);
            }
        }

        public static void ClearSettings()
        {
            AppSettings.Clear();
        }

    }
}

