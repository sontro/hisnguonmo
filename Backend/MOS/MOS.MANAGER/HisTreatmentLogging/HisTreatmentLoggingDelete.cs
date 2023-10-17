using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentLogging
{
    partial class HisTreatmentLoggingDelete : BusinessBase
    {
        internal HisTreatmentLoggingDelete()
            : base()
        {

        }

        internal HisTreatmentLoggingDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TREATMENT_LOGGING data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentLoggingCheck checker = new HisTreatmentLoggingCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_LOGGING raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentLoggingDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TREATMENT_LOGGING> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentLoggingCheck checker = new HisTreatmentLoggingCheck(param);
                List<HIS_TREATMENT_LOGGING> listRaw = new List<HIS_TREATMENT_LOGGING>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentLoggingDAO.DeleteList(listData);
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
