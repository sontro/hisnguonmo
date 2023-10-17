using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareSum
{
	public partial class HisCareSumManager : BusinessBase
	{
		public HisCareSumManager()
			: base()
		{

		}
		
		public HisCareSumManager(CommonParam param)
			: base(param)
		{

		}
		
		[Logger]
		public ApiResultObject<List<HIS_CARE_SUM>> Get(HisCareSumFilterQuery filter)
		{
			ApiResultObject<List<HIS_CARE_SUM>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_CARE_SUM> resultData = null;
				if (valid)
				{
					resultData = new HisCareSumGet(param).Get(filter);
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
        public ApiResultObject<HIS_CARE_SUM> Create(HisCareSumSDO data)
		{
			ApiResultObject<HIS_CARE_SUM> result = new ApiResultObject<HIS_CARE_SUM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_CARE_SUM resultData = null;
                if (valid)
				{
                    new HisCareSumCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_CARE_SUM> Create(HIS_CARE_SUM data)
        {
            ApiResultObject<HIS_CARE_SUM> result = new ApiResultObject<HIS_CARE_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_SUM resultData = null;
                if (valid && new HisCareSumCreate(param).Create(data))
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
        public ApiResultObject<HIS_CARE_SUM> Update(HIS_CARE_SUM data)
        {
            ApiResultObject<HIS_CARE_SUM> result = new ApiResultObject<HIS_CARE_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_SUM resultData = null;
                if (valid && new HisCareSumUpdate(param).Update(data))
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
		public ApiResultObject<HIS_CARE_SUM> ChangeLock(HIS_CARE_SUM data)
		{
			ApiResultObject<HIS_CARE_SUM> result = new ApiResultObject<HIS_CARE_SUM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_CARE_SUM resultData = null;
				if (valid && new HisCareSumLock(param).ChangeLock(data))
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
                    resultData = new HisCareSumTruncate(param).Truncate(id);
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
