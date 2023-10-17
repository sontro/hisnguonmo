using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMaty
{
    partial class HisServiceReqMatyGet : BusinessBase
    {
        internal HisServiceReqMatyGet()
            : base()
        {

        }

        internal HisServiceReqMatyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_REQ_MATY> Get(HisServiceReqMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqMatyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ_MATY> GetByServiceReqId(long serviceReqId)
        {
            HisServiceReqMatyFilterQuery filter = new HisServiceReqMatyFilterQuery();
            filter.SERVICE_REQ_ID = serviceReqId;
            return this.Get(filter);
        }

        internal HIS_SERVICE_REQ_MATY GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceReqMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_REQ_MATY GetById(long id, HisServiceReqMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqMatyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ_MATY> GetByServiceReqIds(List<long> serviceReqIds)
        {
            if (serviceReqIds!=null)
            {
                HisServiceReqMatyFilterQuery filter = new HisServiceReqMatyFilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                return this.Get(filter);
            }
            return null;
        }

    }
}
