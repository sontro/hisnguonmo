using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class FhirCFG
    {
        private const string FHIR_XML4210_FOLDER_PATH_CFG = "MOS.FHIR.SUREHIS.XML_4210_FOLDER_PATH";

        private static string fhirXml4210FolderPath;
        public static string FHIR_XML4210_FOLDER_PATH
        {
            get
            {
                if (fhirXml4210FolderPath == null)
                {
                    fhirXml4210FolderPath = ConfigUtil.GetStrConfig(FHIR_XML4210_FOLDER_PATH_CFG);
                }
                return fhirXml4210FolderPath;
            }
        }

        public static void Reload()
        {
            fhirXml4210FolderPath = ConfigUtil.GetStrConfig(FHIR_XML4210_FOLDER_PATH_CFG);
        }
    }
}
