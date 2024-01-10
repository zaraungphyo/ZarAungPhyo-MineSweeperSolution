using System;
using System.Configuration;
using System.Globalization;


namespace MineSweeperSolution.Utility
{
    public static class ConfigHelper
    {
        public static string ConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string ConfigValue(string key, string defaultValue)
        {
            string result = ConfigurationManager.AppSettings[key];
            if (result == null)
            {
                result = defaultValue;
            }

            return result;
        }

        public static int ConfigValue(string key, int defaultValue)
        {
            string result = ConfigurationManager.AppSettings[key];
            if (result == null)
            {
                return defaultValue;
            }

            int value;
            return int.TryParse(result, NumberStyles.Integer, CultureInfo.CurrentCulture, out value) ? value : defaultValue;
        }
    }

}
