using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaSum
{
	public partial class HisRehaSumManager : BusinessBase
	{
		public HisRehaSumManager()
			: base()
		{

		}
		
		public HisRehaSumManager(CommonParam param)
			: base(param)
		{

		}
		
		[Logger]
		public ApiResultObject<List<HIS_REHA_SUM>> Get(HisRehaSumFilterQuery filter)
		{
			ApiResultObject<List<HIS_REHA_SUM>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_REHA_SUM> resultData = null;
				if (valid)
				{
					resultData = new HisRehaSumGet(param).Get(filter);
				}
				result = this.PackSuccess(resultData);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				param.HasException = true;
				result = null;
			}
			return result;
		}

		[Logger]
        public ApiResultObject<HIS_REHA_SUM> Create(HisRehaSumSDO data)
		{
			ApiResultObject<HIS_REHA_SUM> result = new ApiResultObject<HIS_REHA_SUM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_REHA_SUM resultData = null;
                if (valid)
				{
                    new HisRehaSumCreate(param).Create(data, ref resultData);
				}
				result = this.PackSingleResult(resultData);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				param.HasException = true;
			}
			return result;
		}

        [Logger]
        public ApiResultObject<HIS_REHA_SUM> Update(HIS_REHA_SUM data)
        {
            ApiResultObject<HIS_REHA_SUM> result = new ApiResultObject<HIS_REHA_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_SUM resultData = null;
                if (valid && new HisRehaSumUpdate(param).Update(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

		[Logger]
		public ApiResultObject<HIS_REHA_SUM> ChangeLock(HIS_REHA_SUM data)
		{
			ApiResultObject<HIS_REHA_SUM> result = new ApiResultObject<HIS_REHA_SUM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_REHA_SUM resultData = null;
				if (valid && new HisRehaSumLock(param).ChangeLock(data))
				{
					resultData = data;
				}
				result = this.PackSingleResult(resultData);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				param.HasException = true;
			}
			
			return result;
		}

		[Logger]
		public ApiResultObject<bool> Delete(long id)
		{
			ApiResultObject<bool> result = new ApiResultObject<bool>(false);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				bool resultData = false;
				if (valid)
				{
                    resultData = new HisRehaSumTruncate(param).Truncate(id);
				}
				result = this.PackSingleResult(resultData);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				param.HasException = true;
			}
			
			return result;
		}
	}
}
