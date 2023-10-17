using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentResult
{
    class HisTreatmentResultUpdate : BusinessBase
    {
        internal HisTreatmentResultUpdate()
            : base()
        {

        }

        internal HisTreatmentResultUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentResultCheck checker = new HisTreatmentResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_TREATMENT_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TREATMENT_RESULT_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentResultDAO.Update(data);
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

        internal bool UpdateList(List<HIS_TREATMENT_RESULT> listData)
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
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TREATMENT_RESULT_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTreatmentResultDAO.UpdateList(listData);
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
