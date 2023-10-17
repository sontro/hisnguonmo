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
        internal V_HIS_SERVICE_REQ_8 GetView8ById(long id)
        {
            try
            {
                return GetView8ById(id, new HisServiceReqView8FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_REQ_8 GetView8ById(long id, HisServiceReqView8FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView8ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_8> GetView8(HisServiceReqView8FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView8(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_8> GetView8ByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisServiceReqView8FilterQuery filter = new HisServiceReqView8FilterQuery();
                    filter.IDs = ids;
                    return this.GetView8(filter);
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

        internal List<V_HIS_SERVICE_REQ_8> GetView8ByTreatmentId(long id)
        {
            try
            {
                HisServiceReqView8FilterQuery filter = new HisServiceReqView8FilterQuery();
                filter.TREATMENT_ID = id;
                return this.GetView8(filter);
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
