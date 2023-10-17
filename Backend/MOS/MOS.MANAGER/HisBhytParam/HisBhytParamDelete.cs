using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBhytParam
{
    partial class HisBhytParamDelete : BusinessBase
    {
        internal HisBhytParamDelete()
            : base()
        {

        }

        internal HisBhytParamDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BHYT_PARAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBhytParamCheck checker = new HisBhytParamCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BHYT_PARAM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBhytParamDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BHYT_PARAM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBhytParamCheck checker = new HisBhytParamCheck(param);
                List<HIS_BHYT_PARAM> listRaw = new List<HIS_BHYT_PARAM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBhytParamDAO.DeleteList(listData);
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
