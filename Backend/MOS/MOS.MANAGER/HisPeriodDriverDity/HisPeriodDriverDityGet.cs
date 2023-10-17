using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPeriodDriverDity
{
    partial class HisPeriodDriverDityGet : BusinessBase
    {
        internal HisPeriodDriverDityGet()
            : base()
        {

        }

        internal HisPeriodDriverDityGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PERIOD_DRIVER_DITY> Get(HisPeriodDriverDityFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPeriodDriverDityDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PERIOD_DRIVER_DITY GetById(long id)
        {
            try
            {
                return GetById(id, new HisPeriodDriverDityFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PERIOD_DRIVER_DITY GetById(long id, HisPeriodDriverDityFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPeriodDriverDityDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_PERIOD_DRIVER_DITY> GetByKskPeriodDriverId(long kskPeriodDriverId)
        {
            try
            {
                HisPeriodDriverDityFilterQuery filter = new HisPeriodDriverDityFilterQuery();
                filter.KSK_PERIOD_DRIVER_ID = kskPeriodDriverId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
