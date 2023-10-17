using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodGroup
{
    partial class HisBloodGroupDelete : BusinessBase
    {
        internal HisBloodGroupDelete()
            : base()
        {

        }

        internal HisBloodGroupDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BLOOD_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodGroupCheck checker = new HisBloodGroupCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBloodGroupDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BLOOD_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodGroupCheck checker = new HisBloodGroupCheck(param);
                List<HIS_BLOOD_GROUP> listRaw = new List<HIS_BLOOD_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBloodGroupDAO.DeleteList(listData);
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
