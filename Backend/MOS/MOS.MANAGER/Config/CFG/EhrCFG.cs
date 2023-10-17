using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class EhrCFG
    {
        private const string XML4210_FOLDER_PATH_CFG = "MOS.EHR.XML_4210_FOLDER_PATH";

        private static string xml4210FolderPath;
        public static string XML4210_FOLDER_PATH
        {
            get
            {
                if (xml4210FolderPath == null)
                {
                    xml4210FolderPath = ConfigUtil.GetStrConfig(XML4210_FOLDER_PATH_CFG);
                }
                return xml4210FolderPath;
            }
        }

        public static void Reload()
        {
            xml4210FolderPath = ConfigUtil.GetStrConfig(XML4210_FOLDER_PATH_CFG);
        }
    }
}
