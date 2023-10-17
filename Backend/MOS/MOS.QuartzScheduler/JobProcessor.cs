using MOS.QuartzScheduler.The;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler
{
    public class JobProcessor
    {
        private static bool isAdd = false;
        public static bool AddJob()
        {
            try
            {
                if (!isAdd)
                {
                    Credentials.ScanTimeOutTrigger.AddJob();
                    EventLog.ScanTrigger.AddJob();
                    Lis.ReadResultTrigger.AddJob();
                    Lis.SendRequestTrigger.AddJob();
                    MediStock.RefreshStatusTrigger.AddJob();
                    Pacs.SendRequestTrigger.AddJob();
                    RoomCounter.RefreshTrigger.AddJob();
                    DownloadImageFromCosTrigger.AddJob();
                    Emr.UploadEmrTrigger.AddJob();
                    MediStock.MediStockPeriodTrigger.AddJob();
                    Erx.UploadErxTrigger.AddJob();
                    NotifySubclinicalResultSms.SendSmsTrigger.AddJob();
                    BiinTestResult.SendTestResultTrigger.AddJob();
                    MediStock.AutoSetIsNotTakenTrigger.AddJob();
                    Pacs.ReadResultTrigger.AddJob();
                    Xml.XmlTrigger.AddJob();
                    Xml.ExportXmlFhirSurehisTrigger.AddJob();
                    AutoEndExamTreatment.AutoEndExamTreatmentTrigger.AddJob();
                    Browser.BrowserTrigger.AddJob();
                    AutoCreateRation.AutoCreateRationTrigger.AddJob();
                    AutoAddRationSum.AutoAddRationSumTrigger.AddJob();
                    AutoGetSereServAndUpdateGatherData.AutoGetSereServAndUpdateGatherDataTrigger.AddJob();
                    PubSub.PubSubTrigger.AddJob();
                    License.LicenseTrigger.AddJob();
                    The.NotifyAppointmentTrigger.AddJob();
                    isAdd = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                isAdd = false;
            }
            return isAdd;
        }
    }
}
