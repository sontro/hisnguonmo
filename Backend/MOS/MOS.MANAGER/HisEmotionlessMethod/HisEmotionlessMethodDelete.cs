using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmotionlessMethod
{
    partial class HisEmotionlessMethodDelete : BusinessBase
    {
        internal HisEmotionlessMethodDelete()
            : base()
        {

        }

        internal HisEmotionlessMethodDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EMOTIONLESS_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmotionlessMethodCheck checker = new HisEmotionlessMethodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EMOTIONLESS_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEmotionlessMethodDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EMOTIONLESS_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmotionlessMethodCheck checker = new HisEmotionlessMethodCheck(param);
                List<HIS_EMOTIONLESS_METHOD> listRaw = new List<HIS_EMOTIONLESS_METHOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEmotionlessMethodDAO.DeleteList(listData);
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
