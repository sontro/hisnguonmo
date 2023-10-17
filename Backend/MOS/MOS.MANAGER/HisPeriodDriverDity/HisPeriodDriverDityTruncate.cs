using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPeriodDriverDity
{
    partial class HisPeriodDriverDityTruncate : BusinessBase
    {
        internal HisPeriodDriverDityTruncate()
            : base()
        {

        }

        internal HisPeriodDriverDityTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPeriodDriverDityCheck checker = new HisPeriodDriverDityCheck(param);
                HIS_PERIOD_DRIVER_DITY raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisPeriodDriverDityDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_PERIOD_DRIVER_DITY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPeriodDriverDityCheck checker = new HisPeriodDriverDityCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisPeriodDriverDityDAO.TruncateList(listData);
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

        internal bool TruncateByKskPeriodDriverId(long kskPeriodDriverId)
        {
            bool result = true;
            try
            {
                List<HIS_PERIOD_DRIVER_DITY> periodDriverDitys = new HisPeriodDriverDityGet().GetByKskPeriodDriverId(kskPeriodDriverId);
                if (IsNotNullOrEmpty(periodDriverDitys))
                {
                    result = this.TruncateList(periodDriverDitys);
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
