using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
	partial class HisSereServGet : GetBase
	{
		internal List<V_HIS_SERE_SERV_16> GetView16(HisSereServView16FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisSereServDAO.GetView16(filter.Query(), param);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

        internal List<V_HIS_SERE_SERV_16> GetView16ByIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisSereServView16FilterQuery filter = new HisSereServView16FilterQuery();
                    filter.IDs = ids;
                    return this.GetView16(filter);
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

		internal List<V_HIS_SERE_SERV_16> GetView16ByServiceReqId(long serviceReqId)
		{
			HisSereServView16FilterQuery filter = new HisSereServView16FilterQuery();
			filter.SERVICE_REQ_ID = serviceReqId;
			return new HisSereServGet().GetView16(filter);
		}

        internal List<V_HIS_SERE_SERV_16> GetView16ByServiceReqIdAndIsSpecimen(long serviceReqId, bool isSpecimen)
        {
            HisSereServView16FilterQuery filter = new HisSereServView16FilterQuery();
            filter.SERVICE_REQ_ID = serviceReqId;
            filter.IS_SPECIMEN = isSpecimen;
            return new HisSereServGet().GetView16(filter);
        }

        internal List<V_HIS_SERE_SERV_16> GetView16ByServiceReqIdsAndIsSpecimen(List<long> serviceReqIds, bool isSpecimen)
        {
            if (serviceReqIds != null)
            {
                HisSereServView16FilterQuery filter = new HisSereServView16FilterQuery();
                filter.SERVICE_REQ_IDs = serviceReqIds;
                filter.IS_SPECIMEN = isSpecimen;
                return new HisSereServGet().GetView16(filter);
            }
            return null;
        }

        internal List<V_HIS_SERE_SERV_16> GetView16ByTreatmentId(long treatmentId)
        {
            HisSereServView16FilterQuery filter = new HisSereServView16FilterQuery();
            filter.TREATMENT_ID = treatmentId;
            return new HisSereServGet().GetView16(filter);
        }
		
        internal V_HIS_SERE_SERV_16 GetView16ById(long id)
        {
            try
            {
                return GetView16ById(id, new HisSereServView16FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERE_SERV_16 GetView16ById(long id, HisSereServView16FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDAO.GetView16ById(id, filter.Query());
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
