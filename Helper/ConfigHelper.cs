using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MioBot.Helper
{
    internal class ConfigHelper
    {
        public static String ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key]!;
            return result;
        }
    }
}