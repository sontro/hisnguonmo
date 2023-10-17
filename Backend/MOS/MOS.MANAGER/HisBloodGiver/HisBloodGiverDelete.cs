using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodGiver
{
    partial class HisBloodGiverDelete : BusinessBase
    {
        internal HisBloodGiverDelete()
            : base()
        {

        }

        internal HisBloodGiverDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BLOOD_GIVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodGiverCheck checker = new HisBloodGiverCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_GIVER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBloodGiverDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BLOOD_GIVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodGiverCheck checker = new HisBloodGiverCheck(param);
                List<HIS_BLOOD_GIVER> listRaw = new List<HIS_BLOOD_GIVER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBloodGiverDAO.DeleteList(listData);
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
