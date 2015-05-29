using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ClaudiuHostFactory.Conf
{
    public static class ConfigurationHelpers
    {

        public static T ConfigSetting<T>(string settingName)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[settingName], typeof(T));
        }

        public static T GetAppSettingsValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (ConfigurationManager.AppSettings[Key] != null)
            {
                value = (T)Convert.ChangeType(ConfigurationManager.AppSettings[Key], typeof(T));
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }
            return value;
        }
    }
}
