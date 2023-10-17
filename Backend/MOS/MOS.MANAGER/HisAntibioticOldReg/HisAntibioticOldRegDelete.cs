using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegDelete : BusinessBase
    {
        internal HisAntibioticOldRegDelete()
            : base()
        {

        }

        internal HisAntibioticOldRegDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ANTIBIOTIC_OLD_REG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticOldRegCheck checker = new HisAntibioticOldRegCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_OLD_REG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAntibioticOldRegDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticOldRegCheck checker = new HisAntibioticOldRegCheck(param);
                List<HIS_ANTIBIOTIC_OLD_REG> listRaw = new List<HIS_ANTIBIOTIC_OLD_REG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAntibioticOldRegDAO.DeleteList(listData);
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
