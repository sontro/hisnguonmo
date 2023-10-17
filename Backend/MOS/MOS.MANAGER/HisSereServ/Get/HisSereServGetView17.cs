using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    partial class HisSereServGet : GetBase
    {
        internal List<V_HIS_SERE_SERV_17> GetView17(HisSereServView17FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDAO.GetView17(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERE_SERV_17> GetView17ByIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisSereServView17FilterQuery filter = new HisSereServView17FilterQuery();
                    filter.IDs = ids;
                    return this.GetView17(filter);
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

        internal List<V_HIS_SERE_SERV_17> GetView17ByServiceReqId(long serviceReqId)
        {
            HisSereServView17FilterQuery filter = new HisSereServView17FilterQuery();
            filter.SERVICE_REQ_ID = serviceReqId;
            return new HisSereServGet().GetView17(filter);
        }

        internal List<V_HIS_SERE_SERV_17> GetView17ByServiceReqIdAndIsSpecimen(long serviceReqId, bool isSpecimen)
        {
            HisSereServView17FilterQuery filter = new HisSereServView17FilterQuery();
            filter.SERVICE_REQ_ID = serviceReqId;
            filter.IS_SPECIMEN = isSpecimen;
            return new HisSereServGet().GetView17(filter);
        }

        internal List<V_HIS_SERE_SERV_17> GetView17ByServiceReqIdsAndIsSpecimen(List<long> serviceReqIds, bool isSpecimen)
        {
            if (serviceReqIds != null)
            {
                HisSereServView17FilterQuery filter = new HisSereServView17FilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                filter.IS_SPECIMEN = isSpecimen;
                return new HisSereServGet().GetView17(filter);
            }
            return null;
        }

        internal List<V_HIS_SERE_SERV_17> GetView17ByTreatmentId(long treatmentId)
        {
            HisSereServView17FilterQuery filter = new HisSereServView17FilterQuery();
            filter.TDL_TREATMENT_ID = treatmentId;
            return new HisSereServGet().GetView17(filter);
        }

        internal V_HIS_SERE_SERV_17 GetView17ById(long id)
        {
            try
            {
                return GetView17ById(id, new HisSereServView17FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERE_SERV_17 GetView17ById(long id, HisSereServView17FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDAO.GetView17ById(id, filter.Query());
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