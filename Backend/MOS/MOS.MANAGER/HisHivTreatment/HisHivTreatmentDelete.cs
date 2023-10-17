using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHivTreatment
{
    partial class HisHivTreatmentDelete : BusinessBase
    {
        internal HisHivTreatmentDelete()
            : base()
        {

        }

        internal HisHivTreatmentDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_HIV_TREATMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHivTreatmentCheck checker = new HisHivTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                HIS_HIV_TREATMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisHivTreatmentDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_HIV_TREATMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHivTreatmentCheck checker = new HisHivTreatmentCheck(param);
                List<HIS_HIV_TREATMENT> listRaw = new List<HIS_HIV_TREATMENT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisHivTreatmentDAO.DeleteList(listData);
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
