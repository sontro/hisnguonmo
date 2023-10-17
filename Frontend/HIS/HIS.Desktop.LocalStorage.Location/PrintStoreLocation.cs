
using System;
using System.Configuration;
namespace HIS.Desktop.LocalStorage.Location
{
    public class PrintStoreLocation
    {
        public static string ROOT_PATH = ConfigurationManager.AppSettings["HIS.Desktop.PrintTemplate"];
        static string _rootpath;
        public static string PrintTemplatePath
        {
            get
            {
                if (String.IsNullOrEmpty(_rootpath))
                {
                    _rootpath = System.IO.Path.GetFullPath(System.IO.Path.Combine(ApplicationStoreLocation.ApplicationStartupPath, ROOT_PATH));
                }
                return _rootpath;
            }
            set
            {
                _rootpath = value;
            }
        }
    }
}
