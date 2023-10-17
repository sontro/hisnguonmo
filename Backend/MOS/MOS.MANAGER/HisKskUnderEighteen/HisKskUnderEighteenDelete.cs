using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskUnderEighteen
{
    partial class HisKskUnderEighteenDelete : BusinessBase
    {
        internal HisKskUnderEighteenDelete()
            : base()
        {

        }

        internal HisKskUnderEighteenDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_KSK_UNDER_EIGHTEEN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskUnderEighteenCheck checker = new HisKskUnderEighteenCheck(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_UNDER_EIGHTEEN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisKskUnderEighteenDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_KSK_UNDER_EIGHTEEN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskUnderEighteenCheck checker = new HisKskUnderEighteenCheck(param);
                List<HIS_KSK_UNDER_EIGHTEEN> listRaw = new List<HIS_KSK_UNDER_EIGHTEEN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisKskUnderEighteenDAO.DeleteList(listData);
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
