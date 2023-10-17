using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqGet : GetBase
    {
        internal V_HIS_SERVICE_REQ_10 GetView10ById(long id)
        {
            try
            {
                return GetView10ById(id, new HisServiceReqView10FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_REQ_10 GetView10ById(long id, HisServiceReqView10FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView10ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_10> GetView10(HisServiceReqView10FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView10(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_10> GetView10ByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisServiceReqView10FilterQuery filter = new HisServiceReqView10FilterQuery();
                    filter.IDs = ids;
                    return this.GetView10(filter);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_10> GetView10ByTreatmentId(long id)
        {
            try
            {
                HisServiceReqView10FilterQuery filter = new HisServiceReqView10FilterQuery();
                filter.TREATMENT_ID = id;
                return this.GetView10(filter);
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
