using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmotionlessResult
{
    partial class HisEmotionlessResultDelete : BusinessBase
    {
        internal HisEmotionlessResultDelete()
            : base()
        {

        }

        internal HisEmotionlessResultDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EMOTIONLESS_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmotionlessResultCheck checker = new HisEmotionlessResultCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EMOTIONLESS_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEmotionlessResultDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EMOTIONLESS_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmotionlessResultCheck checker = new HisEmotionlessResultCheck(param);
                List<HIS_EMOTIONLESS_RESULT> listRaw = new List<HIS_EMOTIONLESS_RESULT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEmotionlessResultDAO.DeleteList(listData);
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
