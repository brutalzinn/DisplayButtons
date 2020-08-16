using DisplayButtons.Backend.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    public static class EventXml
    {
        private static AppSettings settings;

        public static AppSettings Settings
        {
            get
            {
                return settings;
            }
        }

        private const string SETTINGS_FILE = "events.xml";

        public static void LoadSettings()
        {
            try
            {
                if (File.Exists(SETTINGS_FILE))
                {
                    settings = XMLUtils.FromXML<AppSettings>(File.ReadAllText(SETTINGS_FILE));
                    return;
                }
            }
            catch (Exception)
            {
                //An error occured while loading the file.
                //Trying to delete the file.

                try
                {
                    File.Delete(SETTINGS_FILE);
                }
                catch (Exception)
                {
                    //Unable to delete file.
                    //Giving up on humanity.
                }
            }

            settings = new AppSettings();

        }

        public static void SaveSettings()
        {
            File.WriteAllText(SETTINGS_FILE, XMLUtils.ToXML(settings));
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



        public AbstractAction abstractAction;

        public AbstractTrigger abstractTrigger;

        public KeyInfoAppSettingsGlobal keyMainFolder { get; set; } = new KeyInfoAppSettingsGlobal();
        public KeyInfoAppSettingsGlobal keyBackFolder { get; set; } = new KeyInfoAppSettingsGlobal();
    }
}
