using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationGroup
{
    partial class HisRationGroupDelete : BusinessBase
    {
        internal HisRationGroupDelete()
            : base()
        {

        }

        internal HisRationGroupDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_RATION_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationGroupCheck checker = new HisRationGroupCheck(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRationGroupDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_RATION_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationGroupCheck checker = new HisRationGroupCheck(param);
                List<HIS_RATION_GROUP> listRaw = new List<HIS_RATION_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRationGroupDAO.DeleteList(listData);
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
