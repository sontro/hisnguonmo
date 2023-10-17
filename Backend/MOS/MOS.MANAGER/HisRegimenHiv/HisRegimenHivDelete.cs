using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRegimenHiv
{
    partial class HisRegimenHivDelete : BusinessBase
    {
        internal HisRegimenHivDelete()
            : base()
        {

        }

        internal HisRegimenHivDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_REGIMEN_HIV data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegimenHivCheck checker = new HisRegimenHivCheck(param);
                valid = valid && IsNotNull(data);
                HIS_REGIMEN_HIV raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRegimenHivDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_REGIMEN_HIV> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRegimenHivCheck checker = new HisRegimenHivCheck(param);
                List<HIS_REGIMEN_HIV> listRaw = new List<HIS_REGIMEN_HIV>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRegimenHivDAO.DeleteList(listData);
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
