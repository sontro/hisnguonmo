using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Camera
{
    public class RegistryConstant
    {
        public const string SOFTWARE_FOLDER = "SOFTWARE";
        public const string COMPANY_FOLDER = "INVENTEC";
        public static readonly string APP_FOLDER = ConfigurationManager.AppSettings["Inventec.Desktop.ApplicationCode"];

        public const string CONNECTION_TYPE_KEY = "CaptureConnectionDeviceType";
    }
}
