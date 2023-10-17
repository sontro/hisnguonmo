using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiReason
{
    class HisTranPatiReasonUpdate : BusinessBase
    {
        internal HisTranPatiReasonUpdate()
            : base()
        {

        }

        internal HisTranPatiReasonUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRAN_PATI_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiReasonCheck checker = new HisTranPatiReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data);
                valid = valid && checker.ExistsCode(data.TRAN_PATI_REASON_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTranPatiReasonDAO.Update(data);
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

        internal bool UpdateList(List<HIS_TRAN_PATI_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTranPatiReasonCheck checker = new HisTranPatiReasonCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.ExistsCode(data.TRAN_PATI_REASON_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTranPatiReasonDAO.UpdateList(listData);
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
