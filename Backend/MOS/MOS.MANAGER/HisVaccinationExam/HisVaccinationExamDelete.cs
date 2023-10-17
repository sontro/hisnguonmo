using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationExam
{
    partial class HisVaccinationExamDelete : BusinessBase
    {
        internal HisVaccinationExamDelete()
            : base()
        {

        }

        internal HisVaccinationExamDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACCINATION_EXAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationExamCheck checker = new HisVaccinationExamCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_EXAM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationExamDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACCINATION_EXAM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationExamCheck checker = new HisVaccinationExamCheck(param);
                List<HIS_VACCINATION_EXAM> listRaw = new List<HIS_VACCINATION_EXAM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationExamDAO.DeleteList(listData);
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
