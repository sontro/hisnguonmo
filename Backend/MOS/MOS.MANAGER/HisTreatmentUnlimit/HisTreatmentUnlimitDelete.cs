using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentUnlimit
{
    partial class HisTreatmentUnlimitDelete : BusinessBase
    {
        internal HisTreatmentUnlimitDelete()
            : base()
        {

        }

        internal HisTreatmentUnlimitDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TREATMENT_UNLIMIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentUnlimitCheck checker = new HisTreatmentUnlimitCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_UNLIMIT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentUnlimitDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TREATMENT_UNLIMIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentUnlimitCheck checker = new HisTreatmentUnlimitCheck(param);
                List<HIS_TREATMENT_UNLIMIT> listRaw = new List<HIS_TREATMENT_UNLIMIT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentUnlimitDAO.DeleteList(listData);
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
