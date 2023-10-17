using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentFile
{
    partial class HisTreatmentFileDelete : BusinessBase
    {
        internal HisTreatmentFileDelete()
            : base()
        {

        }

        internal HisTreatmentFileDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TREATMENT_FILE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentFileCheck checker = new HisTreatmentFileCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_FILE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentFileDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TREATMENT_FILE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentFileCheck checker = new HisTreatmentFileCheck(param);
                List<HIS_TREATMENT_FILE> listRaw = new List<HIS_TREATMENT_FILE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentFileDAO.DeleteList(listData);
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
