using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHospitalizeReason
{
    partial class HisHospitalizeReasonDelete : BusinessBase
    {
        internal HisHospitalizeReasonDelete()
            : base()
        {

        }

        internal HisHospitalizeReasonDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_HOSPITALIZE_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHospitalizeReasonCheck checker = new HisHospitalizeReasonCheck(param);
                valid = valid && IsNotNull(data);
                HIS_HOSPITALIZE_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisHospitalizeReasonDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_HOSPITALIZE_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHospitalizeReasonCheck checker = new HisHospitalizeReasonCheck(param);
                List<HIS_HOSPITALIZE_REASON> listRaw = new List<HIS_HOSPITALIZE_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisHospitalizeReasonDAO.DeleteList(listData);
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
