﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackendAPI.Utils
{

    public static class ApplicationSettingsManager
    {
        private static AppSettings settings;

        public static AppSettings Settings {
            get {
                return settings;
            }
        }

        private const string SETTINGS_FILE = "settings.xml";

        public static void LoadSettings()
        {
            try {
                if (File.Exists(SETTINGS_FILE)) {
                    settings = XMLUtils.SettingsFromXML<AppSettings>(File.ReadAllText(SETTINGS_FILE));
                    return;
                }
            } catch (Exception) {
                //An error occured while loading the file.
                //Trying to delete the file.

                try {
                    File.Delete(SETTINGS_FILE);
                } catch (Exception) {
                    //Unable to delete file.
                    //Giving up on humanity.
                }
            }

            settings = new AppSettings();

        }

        public static void SaveSettings()
        {
            File.WriteAllText(SETTINGS_FILE, XMLUtils.SettingsToXML(settings));
        }

        public static void ReplaceAppSettings(AppSettings newSettings)
        {
            Settings.DeviceName = newSettings.DeviceName;
            Settings.FirstRun = newSettings.FirstRun;
            Settings.OBSPluginNagged = newSettings.OBSPluginNagged;
            Settings.Theme = newSettings.Theme;
        }


    }

    [Serializable]
    public class AppSettings
    {
        public class KeyInfoAppSettingsGlobal
        {
            public KeyInfoAppSettingsGlobal()
            {
            }
            public KeyInfoAppSettingsGlobal(Keys[] modifierKeys, Keys[] keys)
            {
                ModifierKeys = modifierKeys;
                Keys = keys;
            }

            public Keys[] ModifierKeys { get; set; } = new Keys[] { };
            public Keys[] Keys { get; set; } = new Keys[] { };
        }
        /// <summary>
        /// Called to signal to subscribers that the theme was changed.
        /// </summary>
        public event EventHandler ColorSchemeChanged;
        protected virtual void OnColorSchemeChanged(EventArgs e)
        {
            EventHandler eh = ColorSchemeChanged;

            eh?.Invoke(this, e);
        }

        public enum AppTheme
        {
            Neptune,
            DarkSide,
            PinkNanda,
            KindaGreen
        }

        public AppSettings()
        {
            Theme = AppTheme.Neptune;
            FirstRun = true;
            DeviceName = "";
            IFTTTAPIKey = "";
        }

        public AppTheme Theme { get; set; }

        public bool FirstRun { get; set; }

        public string DeviceName { get; set; }
        public int PORT { get; set; } = 5095;
        public bool OBSPluginNagged { get; set; }
        public bool isDevelopermode { get; set; } = false;
        public bool isAutoMinimizer { get; set; } = true;
        public string IFTTTAPIKey { get; set; }
        public string Language { get; set; }
        public string CurrentProfile { get; set; }
        public bool isFolderBrowserEnabled { get; set; }
        public KeyInfoAppSettingsGlobal keyMainFolder { get; set; } = new KeyInfoAppSettingsGlobal();
        public KeyInfoAppSettingsGlobal keyBackFolder { get; set; } = new KeyInfoAppSettingsGlobal();
    }
}
