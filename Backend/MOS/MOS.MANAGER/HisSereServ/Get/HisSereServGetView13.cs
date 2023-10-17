using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
	partial class HisSereServGet : GetBase
	{
		internal List<V_HIS_SERE_SERV_13> GetView13(HisSereServView13FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisSereServDAO.GetView13(filter.Query(), param);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

		internal List<V_HIS_SERE_SERV_13> GetView13ByIds(List<long> ids)
		{
			try
			{
				if (ids != null)
				{
					HisSereServView13FilterQuery filter = new HisSereServView13FilterQuery();
					filter.IDs = ids;
					return this.GetView13(filter);
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

		internal List<V_HIS_SERE_SERV_13> GetView13ByServiceReqId(long serviceReqId)
		{
			HisSereServView13FilterQuery filter = new HisSereServView13FilterQuery();
			filter.SERVICE_REQ_ID = serviceReqId;
			return new HisSereServGet().GetView13(filter);
		}

		internal List<V_HIS_SERE_SERV_13> GetView13ByServiceReqIdAndIsSpecimen(long serviceReqId, bool isSpecimen)
		{
			HisSereServView13FilterQuery filter = new HisSereServView13FilterQuery();
			filter.SERVICE_REQ_ID = serviceReqId;
			filter.IS_SPECIMEN = isSpecimen;
			return new HisSereServGet().GetView13(filter);
		}

		internal List<V_HIS_SERE_SERV_13> GetView13ByServiceReqIdsAndIsSpecimen(List<long> serviceReqIds, bool isSpecimen)
		{
			if (serviceReqIds != null)
			{
				HisSereServView13FilterQuery filter = new HisSereServView13FilterQuery();
				filter.SERVICE_REQ_IDs = serviceReqIds;
				filter.IS_SPECIMEN = isSpecimen;
				return new HisSereServGet().GetView13(filter);
			}
			return null;
		}
		
		internal V_HIS_SERE_SERV_13 GetView13ById(long id)
		{
			try
			{
				return GetView13ById(id, new HisSereServView13FilterQuery());
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

		internal V_HIS_SERE_SERV_13 GetView13ById(long id, HisSereServView13FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisSereServDAO.GetView13ById(id, filter.Query());
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
