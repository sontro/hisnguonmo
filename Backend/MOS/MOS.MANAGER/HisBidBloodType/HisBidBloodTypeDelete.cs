using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBidBloodType
{
    partial class HisBidBloodTypeDelete : BusinessBase
    {
        internal HisBidBloodTypeDelete()
            : base()
        {

        }

        internal HisBidBloodTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BID_BLOOD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidBloodTypeCheck checker = new HisBidBloodTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BID_BLOOD_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBidBloodTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BID_BLOOD_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidBloodTypeCheck checker = new HisBidBloodTypeCheck(param);
                List<HIS_BID_BLOOD_TYPE> listRaw = new List<HIS_BID_BLOOD_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBidBloodTypeDAO.DeleteList(listData);
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
