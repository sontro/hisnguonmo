using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisFinancePeriod;
using MOS.MANAGER.HisTreatment;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHeinApproval
{
    partial class HisHeinApprovalTruncate : BusinessBase
    {
        internal HisHeinApprovalTruncate()
            : base()
        {
        }

        internal HisHeinApprovalTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {
        }

        //Luu y: ko can update sere_serv do truong hein_approval_id trong sere_serv DB 
        //da de tu dong set ve null trong truong hop xoa hein_approval
        internal bool Truncate(long heinApprovalId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHeinApprovalCheck checker = new HisHeinApprovalCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisFinancePeriodCheck financePeriodChecker = new HisFinancePeriodCheck(param);

                HIS_HEIN_APPROVAL raw = null;
                HIS_TREATMENT treatment = null;
                valid = valid && checker.VerifyId(heinApprovalId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && treatmentChecker.VerifyId(raw.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);

                if (valid)
                {
                    V_HIS_CASHIER_ROOM room = HisCashierRoomCFG.DATA.Where(o => o.ID == raw.CASHIER_ROOM_ID).FirstOrDefault();
                    valid = valid && financePeriodChecker.HasNotFinancePeriod(room.BRANCH_ID, raw.EXECUTE_TIME.Value);

                    if (valid)
                    {
                        result = DAOWorker.HisHeinApprovalDAO.Truncate(raw);
                    }
                }

                if (result)
                {
                    string sql = "UPDATE HIS_TREATMENT SET XML4210_URL = null WHERE ID = {0}";

                    string query = String.Format(sql, treatment.ID);

                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Error("Cap nhat XML4210 URL cho HIS_TREATMENT that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
