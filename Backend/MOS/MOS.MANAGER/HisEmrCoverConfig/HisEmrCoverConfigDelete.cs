using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigDelete : BusinessBase
    {
        internal HisEmrCoverConfigDelete()
            : base()
        {

        }

        internal HisEmrCoverConfigDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EMR_COVER_CONFIG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmrCoverConfigCheck checker = new HisEmrCoverConfigCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EMR_COVER_CONFIG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEmrCoverConfigDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EMR_COVER_CONFIG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmrCoverConfigCheck checker = new HisEmrCoverConfigCheck(param);
                List<HIS_EMR_COVER_CONFIG> listRaw = new List<HIS_EMR_COVER_CONFIG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEmrCoverConfigDAO.DeleteList(listData);
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
