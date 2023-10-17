using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintSarFormData
{
    class PrintMps000289
    {
        MPS.Processor.Mps000288.PDO.Mps000288PDO mps000289RDO { get; set; }

        public PrintMps000289(string printTypeCode, string fileName, ref bool result, List<SAR_FORM_DATA> _sarFormDatas, bool _printNow, long? roomId)
        {
            try
            {
                if (_sarFormDatas != null && _sarFormDatas.Count > 0)
                {
                    mps000289RDO = new MPS.Processor.Mps000288.PDO.Mps000288PDO(
                      _sarFormDatas);

                    result = Print.RunPrint(printTypeCode, fileName, mps000289RDO, (Inventec.Common.FlexCelPrint.DelegateEventLog)EventLogPrint, result, _printNow, roomId);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void EventLogPrint()
        {
            try
            {
                string message = "In phiếu kiểm điểm tử vong. Mã in : Mps000289" + "  TREATMENT_CODE: " + EmrDataStore.treatmentCode + "  Thời gian in: " + Inventec.Common.DateTime.Convert.SystemDateTimeToTimeSeparateString(DateTime.Now) + "  Người in: " + Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                His.EventLog.Logger.Log(GlobalVariables.APPLICATION_CODE, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName(), message, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginAddress());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
