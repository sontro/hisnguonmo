using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTrackingTemp
{
    partial class HisTrackingTempDelete : BusinessBase
    {
        internal HisTrackingTempDelete()
            : base()
        {

        }

        internal HisTrackingTempDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TRACKING_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTrackingTempCheck checker = new HisTrackingTempCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TRACKING_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTrackingTempDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TRACKING_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTrackingTempCheck checker = new HisTrackingTempCheck(param);
                List<HIS_TRACKING_TEMP> listRaw = new List<HIS_TRACKING_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTrackingTempDAO.DeleteList(listData);
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
