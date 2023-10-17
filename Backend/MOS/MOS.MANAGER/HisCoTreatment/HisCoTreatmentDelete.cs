using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCoTreatment
{
    partial class HisCoTreatmentDelete : BusinessBase
    {
        internal HisCoTreatmentDelete()
            : base()
        {

        }

        internal HisCoTreatmentDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CO_TREATMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCoTreatmentCheck checker = new HisCoTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CO_TREATMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisCoTreatmentDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CO_TREATMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCoTreatmentCheck checker = new HisCoTreatmentCheck(param);
                List<HIS_CO_TREATMENT> listRaw = new List<HIS_CO_TREATMENT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCoTreatmentDAO.DeleteList(listData);
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
