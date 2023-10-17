using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Common.LocalStorage.SdaConfig;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.LocalData;

namespace HIS.Desktop.Plugins.ExaminationReqEdit.Base
{
    class GlobalStore
    {
        public const string HIS_SERE_SERV_6_GETVIEW = "api/HisSereServ/GetView6";
        public const string HIS_SERVICE_REQ_UPDATE_SERE_SERV = "api/HisServiceReq/UpdateSereServ";
        public const string HIS_SERVICE_REQ_CHANGEROOM = "/api/HisServiceReq/ChangeRoom";
        public const string HIS_TREATMENT_GET = "/api/HisTreatment/Get";
        public const string HIS_SERE_SERV_GETVIEW_12 = "api/HisSereServ/GetView12";
        public const string HIS_SERE_SERV_GET = "api/HisSereServ/Get";
        public const string HIS_SERE_SERV__GET_SERE_SERV_BHYT_OUT_PATIENT_EXAM = "api/HisSereServ/GetSereServBhytOutpatientExam";
        public const string CONFIG_KEY__WARNING_OVER_EXAM__BHYT = "HIS.Desktop.WarningOverExamBhyt";
        public static long HAS_PRIORITY = 1;

        private static string heinLevelCodeCurrent;
        public static string HEIN_LEVEL_CODE__CURRENT
        {
            get
            {
                try
                {
                    var branch = BackendDataWorker.Get<HIS_BRANCH>().FirstOrDefault(o => o.ID == WorkPlace.GetBranchId());
                    if (branch != null)
                    {
                        heinLevelCodeCurrent = branch.HEIN_LEVEL_CODE;
                    }
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                return heinLevelCodeCurrent;
            }
            set
            {
                heinLevelCodeCurrent = value;
            }
        }
    }
}