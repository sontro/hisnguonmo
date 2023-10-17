using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServPttt
{
    public partial class HisSereServPtttManager : BusinessBase
    {
        public HisSereServPtttManager()
            : base()
        {

        }
        
        public HisSereServPtttManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERE_SERV_PTTT>> Get(HisSereServPtttFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_PTTT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV_PTTT> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServPtttGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SERE_SERV_PTTT>> GetView(HisSereServPtttViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_PTTT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_PTTT> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServPtttGet(param).GetView(filter);
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
        public ApiResultObject<HIS_SERE_SERV_PTTT> Create(HIS_SERE_SERV_PTTT data)
        {
            ApiResultObject<HIS_SERE_SERV_PTTT> result = new ApiResultObject<HIS_SERE_SERV_PTTT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_PTTT resultData = null;
                if (valid && new HisSereServPtttCreate(param).Create(data))
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

        public ApiResultObject<HIS_SERE_SERV_PTTT> Update(HIS_SERE_SERV_PTTT data)
        {
            ApiResultObject<HIS_SERE_SERV_PTTT> result = new ApiResultObject<HIS_SERE_SERV_PTTT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_PTTT resultData = null;
                if (valid && new HisSereServPtttUpdate(param).Update(data))
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

        public ApiResultObject<HIS_SERE_SERV_PTTT> ChangeLock(HIS_SERE_SERV_PTTT data)
        {
            ApiResultObject<HIS_SERE_SERV_PTTT> result = new ApiResultObject<HIS_SERE_SERV_PTTT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_PTTT resultData = null;
                if (valid && new HisSereServPtttLock(param).ChangeLock(data))
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

        public ApiResultObject<bool> Delete(HIS_SERE_SERV_PTTT data)
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
                    resultData = new HisSereServPtttTruncate(param).Truncate(data);
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
