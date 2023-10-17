using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttGroupBest
{
    partial class HisPtttGroupBestDelete : BusinessBase
    {
        internal HisPtttGroupBestDelete()
            : base()
        {

        }

        internal HisPtttGroupBestDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PTTT_GROUP_BEST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttGroupBestCheck checker = new HisPtttGroupBestCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_GROUP_BEST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPtttGroupBestDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PTTT_GROUP_BEST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttGroupBestCheck checker = new HisPtttGroupBestCheck(param);
                List<HIS_PTTT_GROUP_BEST> listRaw = new List<HIS_PTTT_GROUP_BEST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPtttGroupBestDAO.DeleteList(listData);
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
