using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class HisTransactionLog
    {
        internal static void Run(HIS_TRANSACTION recieptTran, HIS_TRANSACTION invoiceTran, HIS_TREATMENT treatment, EventLog.Enum logEnum)
        {
            try
            {
                List<VaccinationData> vaccinationList = new List<VaccinationData>();
                List<string> mess = new List<string>();
                if (recieptTran != null)
                {
                    string bl = LogCommonUtil.GetEventLogContent(EventLog.Enum.BienLaiVienPhi);
                    string st = LogCommonUtil.GetEventLogContent(EventLog.Enum.SoTien);
                    mess.Add(String.Format("{0}( TRANSACTION_CODE:{1}. {2}: {3})", bl ?? "", recieptTran.TRANSACTION_CODE, st ?? "", recieptTran.AMOUNT));
                }

                if (invoiceTran != null)
                {
                    string hd = LogCommonUtil.GetEventLogContent(EventLog.Enum.HoaDonDichVu);
                    string st = LogCommonUtil.GetEventLogContent(EventLog.Enum.SoTien);
                    mess.Add(String.Format("{0}( TRANSACTION_CODE:{1}. {2}: {3})", hd ?? "", invoiceTran.TRANSACTION_CODE, st ?? "", invoiceTran.AMOUNT));
                }
                string message = String.Join(". ", mess);
                new EventLogGenerator(logEnum, message)
                    .TreatmentCode(treatment.TREATMENT_CODE)
                    .Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal static void LogDelete(HisTransactionDeleteSDO sdo, HIS_TRANSACTION data, V_HIS_ACCOUNT_BOOK accountBook, EventLog.Enum logEnum)
        {
            try
            {
                List<string> logs = new List<string>();
                if (!String.IsNullOrWhiteSpace(data.TDL_TREATMENT_CODE))
                {
                    logs.Add(String.Format("{0}:{1}", SimpleEventKey.TREATMENT_CODE, data.TDL_TREATMENT_CODE));
                }
                logs.Add(String.Format("{0}:{1}", "ACCOUNT_BOOK_CODE", accountBook.ACCOUNT_BOOK_CODE));

                string sochungtu = LogCommonUtil.GetEventLogContent(EventLog.Enum.SoChungTu);
                logs.Add(String.Format("{0}:{1}", sochungtu, data.NUM_ORDER));

                string sotien = LogCommonUtil.GetEventLogContent(EventLog.Enum.SoTien);
                logs.Add(String.Format("{0}:{1}", sotien, data.AMOUNT));

                if (!String.IsNullOrWhiteSpace(data.TDL_PATIENT_NAME))
                {
                    string tenBN = LogCommonUtil.GetEventLogContent(EventLog.Enum.TenBenhNhan);
                    logs.Add(String.Format("{0}:{1}", tenBN, data.TDL_PATIENT_NAME));
                }
                if (!String.IsNullOrWhiteSpace(sdo.DeleteReason))
                {
                    string lydoxoa = LogCommonUtil.GetEventLogContent(EventLog.Enum.LyDoXoa);
                    logs.Add(String.Format("{0}:{1}", lydoxoa, sdo.DeleteReason));
                }
                string message = String.Join(". ", logs);
                new EventLogGenerator(logEnum, message)
                    .TransactionCode(data.TRANSACTION_CODE)
                    .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
