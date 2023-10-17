using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationStt
{
    partial class HisVaccinationSttDelete : BusinessBase
    {
        internal HisVaccinationSttDelete()
            : base()
        {

        }

        internal HisVaccinationSttDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACCINATION_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationSttCheck checker = new HisVaccinationSttCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_STT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationSttDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACCINATION_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationSttCheck checker = new HisVaccinationSttCheck(param);
                List<HIS_VACCINATION_STT> listRaw = new List<HIS_VACCINATION_STT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationSttDAO.DeleteList(listData);
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
