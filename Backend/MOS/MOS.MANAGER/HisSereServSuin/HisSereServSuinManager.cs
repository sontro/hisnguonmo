using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServSuin
{
    public partial class HisSereServSuinManager : BusinessBase
    {
        public HisSereServSuinManager()
            : base()
        {

        }
        
        public HisSereServSuinManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERE_SERV_SUIN>> Get(HisSereServSuinFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_SUIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV_SUIN> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServSuinGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SERE_SERV_SUIN>> GetView(HisSereServSuinViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_SUIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_SUIN> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServSuinGet(param).GetView(filter);
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
        public ApiResultObject<HIS_SERE_SERV_SUIN> Create(HIS_SERE_SERV_SUIN data)
        {
            ApiResultObject<HIS_SERE_SERV_SUIN> result = new ApiResultObject<HIS_SERE_SERV_SUIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_SUIN resultData = null;
                if (valid && new HisSereServSuinCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERE_SERV_SUIN> Update(HIS_SERE_SERV_SUIN data)
        {
            ApiResultObject<HIS_SERE_SERV_SUIN> result = new ApiResultObject<HIS_SERE_SERV_SUIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_SUIN resultData = null;
                if (valid && new HisSereServSuinUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERE_SERV_SUIN> ChangeLock(HIS_SERE_SERV_SUIN data)
        {
            ApiResultObject<HIS_SERE_SERV_SUIN> result = new ApiResultObject<HIS_SERE_SERV_SUIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_SUIN resultData = null;
                if (valid && new HisSereServSuinLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SERE_SERV_SUIN data)
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
                    resultData = new HisSereServSuinTruncate(param).Truncate(data);
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
