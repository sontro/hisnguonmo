using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMety
{
    partial class HisServiceReqMetyGet : BusinessBase
    {
        internal HisServiceReqMetyGet()
            : base()
        {

        }

        internal HisServiceReqMetyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_REQ_METY> Get(HisServiceReqMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqMetyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_REQ_METY GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceReqMetyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_REQ_METY GetById(long id, HisServiceReqMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqMetyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERVICE_REQ_METY> GetByServiceReqId(long serviceReqId)
        {
            HisServiceReqMetyFilterQuery filter = new HisServiceReqMetyFilterQuery();
            filter.SERVICE_REQ_ID = serviceReqId;
            return this.Get(filter);
        }

        internal List<HIS_SERVICE_REQ_METY> GetByServiceReqIds(List<long> serviceReqIds)
        {
            if (serviceReqIds != null)
            {
                HisServiceReqMetyFilterQuery filter = new HisServiceReqMetyFilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                return this.Get(filter);
            }
            return null;
        }
    }
}
