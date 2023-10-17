using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBhytWhitelist
{
    partial class HisBhytWhitelistDelete : BusinessBase
    {
        internal HisBhytWhitelistDelete()
            : base()
        {

        }

        internal HisBhytWhitelistDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BHYT_WHITELIST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBhytWhitelistCheck checker = new HisBhytWhitelistCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BHYT_WHITELIST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBhytWhitelistDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BHYT_WHITELIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBhytWhitelistCheck checker = new HisBhytWhitelistCheck(param);
                List<HIS_BHYT_WHITELIST> listRaw = new List<HIS_BHYT_WHITELIST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBhytWhitelistDAO.DeleteList(listData);
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
