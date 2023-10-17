using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceRati
{
	partial class HisServiceRatiTruncate : BusinessBase
	{
		internal HisServiceRatiTruncate()
			: base()
		{

		}

		internal HisServiceRatiTruncate(CommonParam paramTruncate)
			: base(paramTruncate)
		{

		}

		internal bool Truncate(long id)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
				HIS_SERVICE_RATI raw = null;
				valid = valid && checker.VerifyId(id, ref raw);
				valid = valid && checker.IsUnLock(raw);
				valid = valid && checker.CheckConstraint(id);
				if (valid)
				{
					result = DAOWorker.HisServiceRatiDAO.Truncate(raw);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				result = false;
			}
			return result;
		}

		internal bool TruncateList(List<HIS_SERVICE_RATI> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
				foreach (var data in listData)
				{
					valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
					valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
				}
				if (valid)
				{
					result = DAOWorker.HisServiceRatiDAO.TruncateList(listData);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				result = false;
			}
			return result;
		}

		internal bool TruncateList(List<long> ids)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(ids);
				HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
				List<HIS_SERVICE_RATI> listRaw = new List<HIS_SERVICE_RATI>();
				valid = valid && checker.VerifyIds(ids, listRaw);
				if (valid)
				{
					result = this.TruncateList(listRaw);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				result = false;
			}
			return result;
		}

        internal bool TruncateByServiceId(long serviceId)
        {
            bool result = true;
            try
            {
                List<HIS_SERVICE_RATI> listData = new HisServiceRatiGet().GetByServiceId(serviceId);
                if (IsNotNullOrEmpty(listData))
                {
                    result = this.TruncateList(listData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
	}
}
