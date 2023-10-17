using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
	partial class HisSereServGet : GetBase
	{
		internal List<V_HIS_SERE_SERV_14> GetView14(HisSereServView14FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisSereServDAO.GetView14(filter.Query(), param);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

		internal List<V_HIS_SERE_SERV_14> GetView14ByIds(List<long> ids)
		{
			try
			{
				if (ids != null)
				{
					HisSereServView14FilterQuery filter = new HisSereServView14FilterQuery();
					filter.IDs = ids;
					return this.GetView14(filter);
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

		internal V_HIS_SERE_SERV_14 GetView14ById(long id)
		{
			try
			{
				return GetView14ById(id, new HisSereServView14FilterQuery());
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				return null;
			}
		}

		internal V_HIS_SERE_SERV_14 GetView14ById(long id, HisSereServView14FilterQuery filter)
		{
			try
			{
				return DAOWorker.HisSereServDAO.GetView14ById(id, filter.Query());
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
