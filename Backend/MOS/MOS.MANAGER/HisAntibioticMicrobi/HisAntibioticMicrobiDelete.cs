using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiDelete : BusinessBase
    {
        internal HisAntibioticMicrobiDelete()
            : base()
        {

        }

        internal HisAntibioticMicrobiDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ANTIBIOTIC_MICROBI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticMicrobiCheck checker = new HisAntibioticMicrobiCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_MICROBI raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAntibioticMicrobiDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ANTIBIOTIC_MICROBI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticMicrobiCheck checker = new HisAntibioticMicrobiCheck(param);
                List<HIS_ANTIBIOTIC_MICROBI> listRaw = new List<HIS_ANTIBIOTIC_MICROBI>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAntibioticMicrobiDAO.DeleteList(listData);
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
