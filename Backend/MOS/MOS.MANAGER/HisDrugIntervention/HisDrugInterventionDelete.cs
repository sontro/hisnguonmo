using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDrugIntervention
{
    partial class HisDrugInterventionDelete : BusinessBase
    {
        internal HisDrugInterventionDelete()
            : base()
        {

        }

        internal HisDrugInterventionDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DRUG_INTERVENTION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDrugInterventionCheck checker = new HisDrugInterventionCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DRUG_INTERVENTION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDrugInterventionDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DRUG_INTERVENTION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDrugInterventionCheck checker = new HisDrugInterventionCheck(param);
                List<HIS_DRUG_INTERVENTION> listRaw = new List<HIS_DRUG_INTERVENTION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDrugInterventionDAO.DeleteList(listData);
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
