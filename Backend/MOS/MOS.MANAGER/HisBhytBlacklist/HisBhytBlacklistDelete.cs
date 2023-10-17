using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBhytBlacklist
{
    partial class HisBhytBlacklistDelete : BusinessBase
    {
        internal HisBhytBlacklistDelete()
            : base()
        {

        }

        internal HisBhytBlacklistDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BHYT_BLACKLIST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBhytBlacklistCheck checker = new HisBhytBlacklistCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BHYT_BLACKLIST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBhytBlacklistDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BHYT_BLACKLIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBhytBlacklistCheck checker = new HisBhytBlacklistCheck(param);
                List<HIS_BHYT_BLACKLIST> listRaw = new List<HIS_BHYT_BLACKLIST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBhytBlacklistDAO.DeleteList(listData);
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
