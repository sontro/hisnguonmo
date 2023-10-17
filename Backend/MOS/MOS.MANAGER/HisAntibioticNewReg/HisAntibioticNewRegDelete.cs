using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegDelete : BusinessBase
    {
        internal HisAntibioticNewRegDelete()
            : base()
        {

        }

        internal HisAntibioticNewRegDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ANTIBIOTIC_NEW_REG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticNewRegCheck checker = new HisAntibioticNewRegCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_NEW_REG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAntibioticNewRegDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ANTIBIOTIC_NEW_REG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticNewRegCheck checker = new HisAntibioticNewRegCheck(param);
                List<HIS_ANTIBIOTIC_NEW_REG> listRaw = new List<HIS_ANTIBIOTIC_NEW_REG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAntibioticNewRegDAO.DeleteList(listData);
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
