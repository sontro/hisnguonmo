using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
	partial class HisSereServGet : GetBase
	{
		internal List<V_HIS_SERE_SERV_15> GetView15(HisSereServView15FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisSereServDAO.GetView15(filter.Query(), param);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

        internal List<V_HIS_SERE_SERV_15> GetView15ByIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisSereServView15FilterQuery filter = new HisSereServView15FilterQuery();
                    filter.IDs = ids;
                    return this.GetView15(filter);
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

		internal List<V_HIS_SERE_SERV_15> GetView15ByServiceReqId(long serviceReqId)
		{
			HisSereServView15FilterQuery filter = new HisSereServView15FilterQuery();
			filter.SERVICE_REQ_ID = serviceReqId;
			return new HisSereServGet().GetView15(filter);
		}

        internal List<V_HIS_SERE_SERV_15> GetView15ByServiceReqIdAndIsSpecimen(long serviceReqId, bool isSpecimen)
        {
            HisSereServView15FilterQuery filter = new HisSereServView15FilterQuery();
            filter.SERVICE_REQ_ID = serviceReqId;
            filter.IS_SPECIMEN = isSpecimen;
            return new HisSereServGet().GetView15(filter);
        }

        internal List<V_HIS_SERE_SERV_15> GetView15ByServiceReqIdsAndIsSpecimen(List<long> serviceReqIds, bool isSpecimen)
        {
            if (serviceReqIds != null)
            {
                HisSereServView15FilterQuery filter = new HisSereServView15FilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                filter.IS_SPECIMEN = isSpecimen;
                return new HisSereServGet().GetView15(filter);
            }
            return null;
        }

        internal List<V_HIS_SERE_SERV_15> GetView15ByTreatmentId(long treatmentId)
        {
            HisSereServView15FilterQuery filter = new HisSereServView15FilterQuery();
            filter.TREATMENT_ID = treatmentId;
            return new HisSereServGet().GetView15(filter);
        }
		
        internal V_HIS_SERE_SERV_15 GetView15ById(long id)
        {
            try
            {
                return GetView15ById(id, new HisSereServView15FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERE_SERV_15 GetView15ById(long id, HisSereServView15FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDAO.GetView15ById(id, filter.Query());
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
