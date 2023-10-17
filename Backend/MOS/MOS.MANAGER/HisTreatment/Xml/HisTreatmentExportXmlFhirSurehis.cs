using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.UTILITY;
using Inventec.Fss.Utility;
using His.ExportXml.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Xml
{
    public class HisTreatmentExportXmlFhirSurehis : BusinessBase
    {
        private static bool IS_SENDING = false;

        public static void Run()
        {
            try
            {
                if (IS_SENDING)
                {
                    LogSystem.Warn("Tien trinh ExportXmlFhirSurehis dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                if (string.IsNullOrWhiteSpace(FhirCFG.FHIR_XML4210_FOLDER_PATH))
                {
                    LogSystem.Warn("MOS.FHIR.SUREHIS.XML_4210_FOLDER_PATH chua cau hinh thu muc xuat xml");
                    return;
                }

                IS_SENDING = true;

                HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                filter.OUT_DATE = Convert.ToInt64(DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "000000");

                var treatments = new HisTreatmentGet().Get(filter);
                LogSystem.Info(string.Format("ExportXmlFhirSurehisJob has {0} treatments:", treatments.Count.ToString()));


                if (treatments != null && treatments.Count > 0)
                {
                    foreach (var treatment in treatments)
                    {
                        new ExportXml().FhirExportXML4210(treatment.ID, treatment.BRANCH_ID);
                    }
                }

                IS_SENDING = false;
            }
            catch (Exception ex)
            {
                IS_SENDING = false;
                LogSystem.Error(ex);
            }
        }
    }
}
