using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.XmlRamMonitor
{
    class XmlRamMonitorConfig
    {
        private static string dataConfigFilePath;
        internal static string DATA_CONFIG_FILE_PATH
        {
            get
            {
                if (dataConfigFilePath == null)
                {
                    try
                    {
                        dataConfigFilePath = ConfigurationManager.AppSettings["HIS.Desktop.XmlRamMonitor.XmlRamMonitorConfig.DataConfigFilePath"];
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Error(ex);
                        dataConfigFilePath = "";
                    }
                }
                return dataConfigFilePath;
            }
        }
    }
}
