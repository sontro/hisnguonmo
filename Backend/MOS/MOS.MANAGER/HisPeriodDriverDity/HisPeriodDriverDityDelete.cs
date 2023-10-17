using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPeriodDriverDity
{
    partial class HisPeriodDriverDityDelete : BusinessBase
    {
        internal HisPeriodDriverDityDelete()
            : base()
        {

        }

        internal HisPeriodDriverDityDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PERIOD_DRIVER_DITY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPeriodDriverDityCheck checker = new HisPeriodDriverDityCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PERIOD_DRIVER_DITY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPeriodDriverDityDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PERIOD_DRIVER_DITY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPeriodDriverDityCheck checker = new HisPeriodDriverDityCheck(param);
                List<HIS_PERIOD_DRIVER_DITY> listRaw = new List<HIS_PERIOD_DRIVER_DITY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPeriodDriverDityDAO.DeleteList(listData);
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
