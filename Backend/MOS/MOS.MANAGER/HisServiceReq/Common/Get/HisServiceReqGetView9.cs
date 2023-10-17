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
        internal V_HIS_SERVICE_REQ_9 GetView9ById(long id)
        {
            try
            {
                return GetView9ById(id, new HisServiceReqView9FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_REQ_9 GetView9ById(long id, HisServiceReqView9FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView9ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_9> GetView9(HisServiceReqView9FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView9(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_9> GetView9ByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisServiceReqView9FilterQuery filter = new HisServiceReqView9FilterQuery();
                    filter.IDs = ids;
                    return this.GetView9(filter);
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

        internal List<V_HIS_SERVICE_REQ_9> GetView9ByTreatmentId(long id)
        {
            try
            {
                HisServiceReqView9FilterQuery filter = new HisServiceReqView9FilterQuery();
                filter.TREATMENT_ID = id;
                return this.GetView9(filter);
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
