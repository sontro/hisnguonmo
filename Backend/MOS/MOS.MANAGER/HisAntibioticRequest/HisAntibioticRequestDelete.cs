using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntibioticRequest
{
    partial class HisAntibioticRequestDelete : BusinessBase
    {
        internal HisAntibioticRequestDelete()
            : base()
        {

        }

        internal HisAntibioticRequestDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ANTIBIOTIC_REQUEST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticRequestCheck checker = new HisAntibioticRequestCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_REQUEST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAntibioticRequestDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ANTIBIOTIC_REQUEST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticRequestCheck checker = new HisAntibioticRequestCheck(param);
                List<HIS_ANTIBIOTIC_REQUEST> listRaw = new List<HIS_ANTIBIOTIC_REQUEST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAntibioticRequestDAO.DeleteList(listData);
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
