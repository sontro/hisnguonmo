using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediReactType
{
    public partial class HisMediReactTypeManager : BusinessBase
    {
        public HisMediReactTypeManager()
            : base()
        {

        }
        
        public HisMediReactTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDI_REACT_TYPE>> Get(HisMediReactTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_REACT_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_REACT_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisMediReactTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDI_REACT_TYPE> Create(HIS_MEDI_REACT_TYPE data)
        {
            ApiResultObject<HIS_MEDI_REACT_TYPE> result = new ApiResultObject<HIS_MEDI_REACT_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT_TYPE resultData = null;
                if (valid && new HisMediReactTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEDI_REACT_TYPE> Update(HIS_MEDI_REACT_TYPE data)
        {
            ApiResultObject<HIS_MEDI_REACT_TYPE> result = new ApiResultObject<HIS_MEDI_REACT_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT_TYPE resultData = null;
                if (valid && new HisMediReactTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDI_REACT_TYPE> ChangeLock(HIS_MEDI_REACT_TYPE data)
        {
            ApiResultObject<HIS_MEDI_REACT_TYPE> result = new ApiResultObject<HIS_MEDI_REACT_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT_TYPE resultData = null;
                if (valid && new HisMediReactTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MEDI_REACT_TYPE data)
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
                    resultData = new HisMediReactTypeTruncate(param).Truncate(data);
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
