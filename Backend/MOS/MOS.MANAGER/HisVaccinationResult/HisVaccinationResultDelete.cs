using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationResult
{
    partial class HisVaccinationResultDelete : BusinessBase
    {
        internal HisVaccinationResultDelete()
            : base()
        {

        }

        internal HisVaccinationResultDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACCINATION_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationResultCheck checker = new HisVaccinationResultCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationResultDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACCINATION_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationResultCheck checker = new HisVaccinationResultCheck(param);
                List<HIS_VACCINATION_RESULT> listRaw = new List<HIS_VACCINATION_RESULT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationResultDAO.DeleteList(listData);
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
