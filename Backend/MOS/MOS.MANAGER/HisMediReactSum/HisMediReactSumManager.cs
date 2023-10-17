using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediReactSum
{
    public partial class HisMediReactSumManager : BusinessBase
    {
        public HisMediReactSumManager()
            : base()
        {

        }
        
        public HisMediReactSumManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDI_REACT_SUM>> Get(HisMediReactSumFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_REACT_SUM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_REACT_SUM> resultData = null;
                if (valid)
                {
                    resultData = new HisMediReactSumGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDI_REACT_SUM> Create(HIS_MEDI_REACT_SUM data)
        {
            ApiResultObject<HIS_MEDI_REACT_SUM> result = new ApiResultObject<HIS_MEDI_REACT_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT_SUM resultData = null;
                if (valid && new HisMediReactSumCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEDI_REACT_SUM> Update(HIS_MEDI_REACT_SUM data)
        {
            ApiResultObject<HIS_MEDI_REACT_SUM> result = new ApiResultObject<HIS_MEDI_REACT_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT_SUM resultData = null;
                if (valid && new HisMediReactSumUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDI_REACT_SUM> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDI_REACT_SUM> result = new ApiResultObject<HIS_MEDI_REACT_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_REACT_SUM resultData = null;
                if (valid)
                {
                    new HisMediReactSumLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_REACT_SUM> Lock(long id)
        {
            ApiResultObject<HIS_MEDI_REACT_SUM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_REACT_SUM resultData = null;
                if (valid)
                {
                    new HisMediReactSumLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_REACT_SUM> Unlock(long id)
        {
            ApiResultObject<HIS_MEDI_REACT_SUM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_REACT_SUM resultData = null;
                if (valid)
                {
                    new HisMediReactSumLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMediReactSumTruncate(param).Truncate(id);
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
