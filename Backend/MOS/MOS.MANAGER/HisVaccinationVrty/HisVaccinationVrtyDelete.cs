using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationVrty
{
    partial class HisVaccinationVrtyDelete : BusinessBase
    {
        internal HisVaccinationVrtyDelete()
            : base()
        {

        }

        internal HisVaccinationVrtyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACCINATION_VRTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationVrtyCheck checker = new HisVaccinationVrtyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_VRTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationVrtyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACCINATION_VRTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationVrtyCheck checker = new HisVaccinationVrtyCheck(param);
                List<HIS_VACCINATION_VRTY> listRaw = new List<HIS_VACCINATION_VRTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationVrtyDAO.DeleteList(listData);
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
