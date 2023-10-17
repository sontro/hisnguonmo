using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSuimIndex
{
	public class HisSuimIndexManager : MOS.MANAGER.Base.BusinessBase
	{
		public HisSuimIndexManager()
			: base()
		{

		}

		public HisSuimIndexManager(CommonParam param)
			: base(param)
		{

		}

		[Logger]
		public ApiResultObject<List<HIS_SUIM_INDEX>> Get(HisSuimIndexFilterQuery filter)
		{
			ApiResultObject<List<HIS_SUIM_INDEX>> result = null;
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
                List<HIS_SUIM_INDEX> resultData = null;
				if (valid)
				{
                    resultData = new HisSuimIndexGet(param).Get(filter);
				}
                result = this.PackSuccess(resultData);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				result = null;
			}

			return result;
		}

		[Logger]
		public ApiResultObject<List<V_HIS_SUIM_INDEX>> GetView(HisSuimIndexViewFilterQuery filter)
		{
			ApiResultObject<List<V_HIS_SUIM_INDEX>> result = null;
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
                List<V_HIS_SUIM_INDEX> resultData = null;
				if (valid)
				{
                    resultData = new HisSuimIndexGet(param).GetView(filter);
				}
                result = this.PackSuccess(resultData);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
				result = null;
			}

			return result;
		}

        [Logger]
        public ApiResultObject<List<HisSuimIndexTDO>> GetTDO()
        {
            ApiResultObject<List<HisSuimIndexTDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisSuimIndexTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisSuimIndexGet(param).GetTDO();
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

		[Logger]
		public ApiResultObject<HIS_SUIM_INDEX> Create(HIS_SUIM_INDEX data)
		{
			ApiResultObject<HIS_SUIM_INDEX> result = new ApiResultObject<HIS_SUIM_INDEX>(null);
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
                HIS_SUIM_INDEX resultData = null;
				if (valid && new HisSuimIndexCreate(param).Create(data))
				{
                    resultData = data;
				}
                result = this.PackSingleResult(resultData);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
			}

			return result;
		}

		[Logger]
		public ApiResultObject<HIS_SUIM_INDEX> Update(HIS_SUIM_INDEX data)
		{
			ApiResultObject<HIS_SUIM_INDEX> result = new ApiResultObject<HIS_SUIM_INDEX>(null);
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
                HIS_SUIM_INDEX resultData = null;
				if (valid && new HisSuimIndexUpdate(param).Update(data))
				{
                    resultData = data;
				}
                result = this.PackSingleResult(resultData);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
			}

			return result;
		}

		[Logger]
        public ApiResultObject<HIS_SUIM_INDEX> ChangeLock(HIS_SUIM_INDEX data)
		{
            ApiResultObject<HIS_SUIM_INDEX> result = new ApiResultObject<HIS_SUIM_INDEX>(null);
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SUIM_INDEX resultData = null;
				if (valid && new HisSuimIndexLock(param).ChangeLock(data))
				{
                    resultData = data;
				}
                result = this.PackSingleResult(resultData);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
			}

			return result;
		}

		[Logger]
		public ApiResultObject<bool> Delete(HIS_SUIM_INDEX data)
		{
			ApiResultObject<bool> result = new ApiResultObject<bool>(false);
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				if (valid)
				{
                    HisSuimIndexTruncate deleteConcrete = new HisSuimIndexTruncate(param);
                    result = deleteConcrete.PackSingleResult(deleteConcrete.Truncate(data));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				param.HasException = true;
			}

			return result;
		}
	}
}
