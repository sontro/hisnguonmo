using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServReha
{
    public partial class HisSereServRehaManager : BusinessBase
    {
        public HisSereServRehaManager()
            : base()
        {

        }
        
        public HisSereServRehaManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERE_SERV_REHA>> Get(HisSereServRehaFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_REHA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV_REHA> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServRehaGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SERE_SERV_REHA>> GetView(HisSereServRehaViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_REHA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_REHA> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServRehaGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_SERE_SERV_REHA>> GetViewByRehaSumId(long rehaSumId)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_REHA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_SERE_SERV_REHA> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServRehaGet(param).GetViewByRehaSumId(rehaSumId);
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
        public ApiResultObject<HIS_SERE_SERV_REHA> Create(HIS_SERE_SERV_REHA data)
        {
            ApiResultObject<HIS_SERE_SERV_REHA> result = new ApiResultObject<HIS_SERE_SERV_REHA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_REHA resultData = null;
                if (valid && new HisSereServRehaCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_SERE_SERV_REHA>> Create(HisSereServRehaSDO data)
        {
            ApiResultObject<List<HIS_SERE_SERV_REHA>> result = new ApiResultObject<List<HIS_SERE_SERV_REHA>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERE_SERV_REHA> resultData = null;
                if (valid)
                {
                    new HisSereServRehaCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_REHA> Update(HIS_SERE_SERV_REHA data)
        {
            ApiResultObject<HIS_SERE_SERV_REHA> result = new ApiResultObject<HIS_SERE_SERV_REHA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_REHA resultData = null;
                if (valid && new HisSereServRehaUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERE_SERV_REHA> ChangeLock(HIS_SERE_SERV_REHA data)
        {
            ApiResultObject<HIS_SERE_SERV_REHA> result = new ApiResultObject<HIS_SERE_SERV_REHA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_REHA resultData = null;
                if (valid && new HisSereServRehaLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SERE_SERV_REHA data)
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
                    resultData = new HisSereServRehaTruncate(param).Truncate(data);
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
