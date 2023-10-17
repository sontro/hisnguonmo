using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepartment
{
    public partial class HisDepartmentManager : BusinessBase
    {
        public HisDepartmentManager()
            : base()
        {

        }
        
        public HisDepartmentManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_DEPARTMENT>> Get(HisDepartmentFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEPARTMENT>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEPARTMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisDepartmentGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEPARTMENT> Create(HIS_DEPARTMENT data)
        {
            ApiResultObject<HIS_DEPARTMENT> result = new ApiResultObject<HIS_DEPARTMENT>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPARTMENT resultData = null;
                if (valid && new HisDepartmentCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_DEPARTMENT>> CreateList(List<HIS_DEPARTMENT> data)
        {
            ApiResultObject<List<HIS_DEPARTMENT>> result = new ApiResultObject<List<HIS_DEPARTMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_DEPARTMENT> resultData = null;
                if (valid && new HisDepartmentCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_DEPARTMENT> Update(HIS_DEPARTMENT data)
        {
            ApiResultObject<HIS_DEPARTMENT> result = new ApiResultObject<HIS_DEPARTMENT>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPARTMENT resultData = null;
                if (valid && new HisDepartmentUpdate(param).Update(data))
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
        public ApiResultObject<HIS_DEPARTMENT> ChangeLock(HIS_DEPARTMENT data)
        {
            ApiResultObject<HIS_DEPARTMENT> result = new ApiResultObject<HIS_DEPARTMENT>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_DEPARTMENT resultData = null;
                if (valid && new HisDepartmentLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_DEPARTMENT data)
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
                    resultData = new HisDepartmentTruncate(param).Truncate(data);
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
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisDepartmentTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HisDeparmentTDO>> GetTdo()
        {
            ApiResultObject<List<HisDeparmentTDO>> result = null;

            try
            {
                bool valid = true;
                List<HisDeparmentTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisDepartmentGet(param).GetTdo();
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
    }
}
