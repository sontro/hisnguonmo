using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediReact
{
	public partial class HisMediReactManager : BusinessBase
	{
		public HisMediReactManager()
			: base()
		{

		}
		
		public HisMediReactManager(CommonParam param)
			: base(param)
		{

		}
		
		[Logger]
		public ApiResultObject<List<HIS_MEDI_REACT>> Get(HisMediReactFilterQuery filter)
		{
			ApiResultObject<List<HIS_MEDI_REACT>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_MEDI_REACT> resultData = null;
				if (valid)
				{
					resultData = new HisMediReactGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_MEDI_REACT>> GetView(HisMediReactViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_REACT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_REACT> resultData = null;
                if (valid)
                {
                    resultData = new HisMediReactGet(param).GetView(filter);
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
		public ApiResultObject<HIS_MEDI_REACT> Create(HIS_MEDI_REACT data)
		{
			ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_MEDI_REACT resultData = null;
				if (valid && new HisMediReactCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEDI_REACT> Update(HIS_MEDI_REACT data)
        {
            ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT resultData = null;
                if (valid && new HisMediReactUpdate(param).Update(data))
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
		public ApiResultObject<HIS_MEDI_REACT> Check(HIS_MEDI_REACT data)
		{
			ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_MEDI_REACT resultData = null;
                if (valid)
				{
                    new HisMediReactUpdate(param).Check(data, ref resultData);
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
        public ApiResultObject<HIS_MEDI_REACT> UnCheck(HIS_MEDI_REACT data)
        {
            ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT resultData = null;
                if (valid)
                {
                    new HisMediReactUpdate(param).UnCheck(data, ref resultData);
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
        public ApiResultObject<HIS_MEDI_REACT> Execute(HIS_MEDI_REACT data)
        {
            ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT resultData = null;
                if (valid)
                {
                    new HisMediReactUpdate(param).Execute(data, ref resultData);
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
        public ApiResultObject<HIS_MEDI_REACT> UnExecute(HIS_MEDI_REACT data)
        {
            ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT resultData = null;
                if (valid)
                {
                    new HisMediReactUpdate(param).UnExecute(data, ref resultData);
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
		public ApiResultObject<HIS_MEDI_REACT> ChangeLock(HIS_MEDI_REACT data)
		{
			ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_MEDI_REACT resultData = null;
				if (valid && new HisMediReactLock(param).ChangeLock(data))
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
		public ApiResultObject<bool> Delete(HIS_MEDI_REACT data)
		{
			ApiResultObject<bool> result = new ApiResultObject<bool>(false);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				bool resultData = false;
				if (valid)
				{
                    resultData = new HisMediReactTruncate(param).Truncate(data);
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
