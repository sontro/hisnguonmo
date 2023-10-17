using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentResult
{
    class HisTreatmentResultTruncate : BusinessBase
    {
        internal HisTreatmentResultTruncate()
            : base()
        {

        }

        internal HisTreatmentResultTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_TREATMENT_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentResultCheck checker = new HisTreatmentResultCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                HIS_TREATMENT_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw.ID);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentResultDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_TREATMENT_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentResultCheck checker = new HisTreatmentResultCheck(param);
                List<HIS_TREATMENT_RESULT> listRaw = new List<HIS_TREATMENT_RESULT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentResultDAO.TruncateList(listData);
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
