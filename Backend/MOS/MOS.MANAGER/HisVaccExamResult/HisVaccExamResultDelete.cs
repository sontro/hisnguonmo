using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccExamResult
{
    partial class HisVaccExamResultDelete : BusinessBase
    {
        internal HisVaccExamResultDelete()
            : base()
        {

        }

        internal HisVaccExamResultDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACC_EXAM_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccExamResultCheck checker = new HisVaccExamResultCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_EXAM_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccExamResultDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACC_EXAM_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccExamResultCheck checker = new HisVaccExamResultCheck(param);
                List<HIS_VACC_EXAM_RESULT> listRaw = new List<HIS_VACC_EXAM_RESULT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccExamResultDAO.DeleteList(listData);
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
