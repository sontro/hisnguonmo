using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationVrpl
{
    partial class HisVaccinationVrplDelete : BusinessBase
    {
        internal HisVaccinationVrplDelete()
            : base()
        {

        }

        internal HisVaccinationVrplDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACCINATION_VRPL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationVrplCheck checker = new HisVaccinationVrplCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_VRPL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationVrplDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACCINATION_VRPL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationVrplCheck checker = new HisVaccinationVrplCheck(param);
                List<HIS_VACCINATION_VRPL> listRaw = new List<HIS_VACCINATION_VRPL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationVrplDAO.DeleteList(listData);
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
