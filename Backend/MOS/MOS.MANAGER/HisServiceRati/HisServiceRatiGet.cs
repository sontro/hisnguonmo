using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRati
{
    partial class HisServiceRatiGet : BusinessBase
    {
        internal HisServiceRatiGet()
            : base()
        {

        }

        internal HisServiceRatiGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_RATI> Get(HisServiceRatiFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceRatiDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_RATI GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceRatiFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_RATI> GetByServiceId(List<long> serviceIds)
        {
            if (IsNotNullOrEmpty(serviceIds))
            {
                HisServiceRatiFilterQuery filter = new HisServiceRatiFilterQuery();
                filter.SERVICE_IDs = serviceIds;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERVICE_RATI> GetByServiceId(long serviceId)
        {
            HisServiceRatiFilterQuery filter = new HisServiceRatiFilterQuery();
            filter.SERVICE_ID = serviceId;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_RATI> GetByRationTimeId(List<long> rationTimeIds)
        {
            if (IsNotNullOrEmpty(rationTimeIds))
            {
                HisServiceRatiFilterQuery filter = new HisServiceRatiFilterQuery();
                filter.RATION_TIME_IDs = rationTimeIds;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERVICE_RATI> GetByRationTimeId(long rationTimeId)
        {
            HisServiceRatiFilterQuery filter = new HisServiceRatiFilterQuery();
            filter.RATION_TIME_ID = rationTimeId;
            return this.Get(filter);
        }

        internal HIS_SERVICE_RATI GetById(long id, HisServiceRatiFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceRatiDAO.GetById(id, filter.Query());
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
