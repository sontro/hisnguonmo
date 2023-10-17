using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.LocalStorage.SdaConfig;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqList.Base
{
    class ConfigKey
    {
        internal const string PrintNow_NumCopy = "CONFIG_KEY__HIS_DESKTOP_PRINT_NOW__NUM_COPY";

        internal const string HIS_DEPOSIT__DEFAULT_PRICE_FOR_BHYT_OUT_PATIENT = "HIS_RS.HIS_DEPOSIT.DEFAULT_PRICE_FOR_BHYT_OUT_PATIENT";
        //tính tiền dịch vụ cần tạm ứng(ct MOS.LibraryHein.Bhyt.BhytPriceCalculator.DefaultPriceForBhytOutPatient) ở chức năng tạm ứng dịch vụ theo dịch vụ.

        internal const string Filter_Type_For_Treatment_Patient = "HIS.Desktop.Plugins.ServiceReqList.Filter_Type_For_Treatment_Patient";

        internal const string HIS_Desktop_Plugins_AssignServicePrintTEST = "HIS.Desktop.Plugins.AssignServicePrintTEST";

        internal const string PATIENT_TYPE_ID__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";

        internal const string MpsTotalToBordereau = "HIS.Desktop.Plugins.ServiceReqList.MpsTotalToBordereau";//dùng cho thành phố in bảng kê trên mẫu tổng hợp, cho phép in thuốc trên mẫu in mps37

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
