using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskOverEighteen
{
    partial class HisKskOverEighteenDelete : BusinessBase
    {
        internal HisKskOverEighteenDelete()
            : base()
        {

        }

        internal HisKskOverEighteenDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_KSK_OVER_EIGHTEEN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskOverEighteenCheck checker = new HisKskOverEighteenCheck(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_OVER_EIGHTEEN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisKskOverEighteenDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_KSK_OVER_EIGHTEEN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskOverEighteenCheck checker = new HisKskOverEighteenCheck(param);
                List<HIS_KSK_OVER_EIGHTEEN> listRaw = new List<HIS_KSK_OVER_EIGHTEEN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisKskOverEighteenDAO.DeleteList(listData);
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
