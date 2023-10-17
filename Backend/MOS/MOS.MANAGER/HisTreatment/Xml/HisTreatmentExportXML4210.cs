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
    public class HisTreatmentExportXML4210 : BusinessBase
    {
        private static bool IS_SENDING = false;

        public static void Run()
        {
            try
            {
                if (IS_SENDING)
                {
                    LogSystem.Warn("Tien trinh tu dong exportXML4210 dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                IS_SENDING = true;

                HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                filter.OUT_TIME_FROM = Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000");
                filter.OUT_TIME_TO = Inventec.Common.DateTime.Get.Now();
                filter.IS_ACTIVE = Constant.IS_FALSE;
                filter.HAS_XML4210_URL = false;
                filter.IS_LOCK_HEIN = true;

                var treatments = new HisTreatmentGet().Get(filter);
                LogSystem.Info(string.Format("XmlJob has {0} treatments:", treatments.Count.ToString()));


                if (treatments != null && treatments.Count > 0)
                {
                    foreach (var treatment in treatments)
                    {
                        new ExportXml().ExportXML4210(treatment.ID, treatment.BRANCH_ID);
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
