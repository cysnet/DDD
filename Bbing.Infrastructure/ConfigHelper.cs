using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.Infrastructure
{
    public static class ConfigHelper
    {
        public static string GetAppSetting(this string key)
        {
            string indexConfigPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (!File.Exists(indexConfigPath + "\\bin\\Global.config"))
            {
                if (!File.Exists(indexConfigPath + "\\Global.config"))
                {
                    throw new Exception($"配置文件不存在：{indexConfigPath}");
                }
                else
                {
                    indexConfigPath += "\\Global.config";
                }
            }
            else
            {
                indexConfigPath += "\\bin\\Global.config";
            }

            ExeConfigurationFileMap ecf = new ExeConfigurationFileMap
            {
                ExeConfigFilename = indexConfigPath
            };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
            return config.AppSettings.Settings[key].Value;
        }
    }
}
