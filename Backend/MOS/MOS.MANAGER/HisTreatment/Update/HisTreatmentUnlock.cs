using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.MANAGER.HisFinancePeriod;

namespace MOS.MANAGER.HisTreatment
{
    class HisTreatmentUnlock : BusinessBase
    {
        internal HisTreatmentUnlock()
            : base()
        {

        }

        internal HisTreatmentUnlock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        public bool Run(HisTreatmentLockSDO sdo, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                WorkPlaceSDO workPlace = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                HisFinancePeriodCheck financePeriodChecker = new HisFinancePeriodCheck(param);
                valid = valid && this.HasWorkPlaceInfo(sdo.RequestRoomId, ref workPlace);
                valid = valid && checker.VerifyId(sdo.TreatmentId, ref raw);
                valid = valid && checker.HasNoHeinApproval(sdo.TreatmentId);//da co ho so giam dinh, ko cho mo khoa vien phi
                valid = valid && checker.IsUnLockHein(raw);
                valid = valid && checker.IsLock(raw);
                valid = valid && financePeriodChecker.HasNotFinancePeriod(workPlace.BranchId, raw.FEE_LOCK_TIME.Value);
                if (valid)
                {
                    raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                    raw.FEE_LOCK_TIME = null;
                    raw.FEE_LOCK_DEPARTMENT_ID = null;
                    raw.FEE_LOCK_ROOM_ID = null;
                    raw.IS_TEMPORARY_LOCK = null;
                    raw.FEE_LOCK_USERNAME = null;
                    raw.FEE_LOCK_LOGINNAME = null;

                    if (DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        result = true;
                        resultData = raw;

                        new EventLogGenerator(EventLog.Enum.HisTreatment_HuyDuyetKhoaVienPhi)
                            .TreatmentCode(resultData.TREATMENT_CODE).Run();
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
