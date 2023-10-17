using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UploadFileAUP
{
    internal class RegistryConstant
    {
        internal const string SOFTWARE_FOLDER = "SOFTWARE";
        internal const string COMPANY_FOLDER = "INVENTEC";
        internal static readonly string APP_FOLDER = ConfigurationManager.AppSettings["Inventec.Desktop.ApplicationCode"];

        internal const string DATA_KEY = "WorkingUploadFilePath";
    }
}
