using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment.Util;
using MOS.SDO;
using MOS.UTILITY;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.MediRecord
{
    class HisTreatmentRejectStore : BusinessBase
    {
        internal HisTreatmentRejectStore()
            : base()
        {

        }

        internal HisTreatmentRejectStore(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTreatmentRejectStoreSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.TreatmentId, ref raw);
                valid = valid && checker.HasNoDataStoreId(raw);
                valid = valid && checker.IsNotRejectStore(raw);

                if (valid)
                {
                    raw.APPROVAL_STORE_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__TU_CHOI;
                    raw.REJECT_STORE_REASON = data.RejectReason;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET APPROVAL_STORE_STT_ID = :param1, REJECT_STORE_REASON = :param2, MODIFIER = :param3  WHERE ID = :param4", raw.APPROVAL_STORE_STT_ID, raw.REJECT_STORE_REASON, loginname, raw.ID))
                    {
                        throw new Exception("Update HisTreatment that bai");
                    }
                    resultData = raw;
                    result = true;

                    // Ghi nhat ky tac dong
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTreatment_TuChoiDuyetBenhAn).TreatmentCode(raw.TREATMENT_CODE).PatientCode(raw.TDL_PATIENT_CODE).Run();
                    this.InitThreadNotify(raw);
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void InitThreadNotify(HIS_TREATMENT treatment)
        {
            try
            {
                if (IsNotNull(treatment) && HisTreatmentCFG.NOTIFY_APPROVE_MEDI_RECORD_WHEN_TREATMENT_FINISH)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.Notify));
                    thread.Priority = ThreadPriority.Lowest;
                    thread.Start(treatment.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Notify(object data)
        {
            try
            {
                long treatmentId = (long)data;
                new HisTreatmentNotify().RunRejectStore(treatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
