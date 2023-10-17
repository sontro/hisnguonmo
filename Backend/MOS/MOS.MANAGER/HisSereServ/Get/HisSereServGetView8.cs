using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
	partial class HisSereServGet : GetBase
	{
		internal List<V_HIS_SERE_SERV_8> GetView8(HisSereServView8FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisSereServDAO.GetView8(filter.Query(), param);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

		internal List<V_HIS_SERE_SERV_8> GetView8ByIds(List<long> ids)
		{
			try
			{
				if (ids != null)
				{
					HisSereServView8FilterQuery filter = new HisSereServView8FilterQuery();
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

		internal V_HIS_SERE_SERV_8 GetView8ById(long id)
		{
			try
			{
				return GetView8ById(id, new HisSereServView8FilterQuery());
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

		internal V_HIS_SERE_SERV_8 GetView8ById(long id, HisSereServView8FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisSereServDAO.GetView8ById(id, filter.Query());
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
