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
        internal V_HIS_SERVICE_REQ_11 GetView11ById(long id)
        {
            try
            {
                return GetView11ById(id, new HisServiceReqView11FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_REQ_11 GetView11ById(long id, HisServiceReqView11FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView11ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_11> GetView11(HisServiceReqView11FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView11(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_11> GetView11ByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisServiceReqView11FilterQuery filter = new HisServiceReqView11FilterQuery();
                    filter.IDs = ids;
                    return this.GetView11(filter);
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

        internal List<V_HIS_SERVICE_REQ_11> GetView11ByTreatmentId(long id)
        {
            try
            {
                HisServiceReqView11FilterQuery filter = new HisServiceReqView11FilterQuery();
                filter.TREATMENT_ID = id;
                return this.GetView11(filter);
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
