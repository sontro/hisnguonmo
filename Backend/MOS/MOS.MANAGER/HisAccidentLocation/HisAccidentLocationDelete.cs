using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentLocation
{
    partial class HisAccidentLocationDelete : BusinessBase
    {
        internal HisAccidentLocationDelete()
            : base()
        {

        }

        internal HisAccidentLocationDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ACCIDENT_LOCATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentLocationCheck checker = new HisAccidentLocationCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_LOCATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentLocationDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ACCIDENT_LOCATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentLocationCheck checker = new HisAccidentLocationCheck(param);
                List<HIS_ACCIDENT_LOCATION> listRaw = new List<HIS_ACCIDENT_LOCATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentLocationDAO.DeleteList(listData);
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
