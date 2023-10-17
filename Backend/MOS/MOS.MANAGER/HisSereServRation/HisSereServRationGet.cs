using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServRation
{
    partial class HisSereServRationGet : BusinessBase
    {
        internal HisSereServRationGet()
            : base()
        {

        }

        internal HisSereServRationGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERE_SERV_RATION> Get(HisSereServRationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServRationDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_RATION GetById(long id)
        {
            try
            {
                return GetById(id, new HisSereServRationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_RATION GetById(long id, HisSereServRationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServRationDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV_RATION> GetByServiceReqId(long serviceReqId)
        {
            try
            {
                HisSereServRationFilterQuery filter = new HisSereServRationFilterQuery();
                filter.SERVICE_REQ_ID = serviceReqId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }

        internal List<HIS_SERE_SERV_RATION> GetByServiceReqIds(List<long> serviceReqIds)
        {
            try
            {
                if (serviceReqIds != null)
                {
                    HisSereServRationFilterQuery filter = new HisSereServRationFilterQuery();
                    filter.SERVICE_REQ_IDs = serviceReqIds;
                    return this.Get(filter);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }
    }
}
