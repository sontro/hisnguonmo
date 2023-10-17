using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAcinInteractive
{
    public partial class HisAcinInteractiveManager : BusinessBase
    {
        public HisAcinInteractiveManager()
            : base()
        {

        }
        
        public HisAcinInteractiveManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ACIN_INTERACTIVE>> Get(HisAcinInteractiveFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACIN_INTERACTIVE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACIN_INTERACTIVE> resultData = null;
                if (valid)
                {
                    resultData = new HisAcinInteractiveGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_ACIN_INTERACTIVE>> GetView(HisAcinInteractiveViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ACIN_INTERACTIVE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ACIN_INTERACTIVE> resultData = null;
                if (valid)
                {
                    resultData = new HisAcinInteractiveGet(param).GetView(filter);
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
        public ApiResultObject<HIS_ACIN_INTERACTIVE> Create(HIS_ACIN_INTERACTIVE data)
        {
            ApiResultObject<HIS_ACIN_INTERACTIVE> result = new ApiResultObject<HIS_ACIN_INTERACTIVE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACIN_INTERACTIVE resultData = null;
                if (valid && new HisAcinInteractiveCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_ACIN_INTERACTIVE>> CreateList(List<HIS_ACIN_INTERACTIVE> data)
        {
            ApiResultObject<List<HIS_ACIN_INTERACTIVE>> result = new ApiResultObject<List<HIS_ACIN_INTERACTIVE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_ACIN_INTERACTIVE> resultData = null;
                if (valid && new HisAcinInteractiveCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_ACIN_INTERACTIVE> Update(HIS_ACIN_INTERACTIVE data)
        {
            ApiResultObject<HIS_ACIN_INTERACTIVE> result = new ApiResultObject<HIS_ACIN_INTERACTIVE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACIN_INTERACTIVE resultData = null;
                if (valid && new HisAcinInteractiveUpdate(param).Update(data))
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
        public ApiResultObject<List<HIS_ACIN_INTERACTIVE>> UpdateList(List<HIS_ACIN_INTERACTIVE> data)
        {
            ApiResultObject<List<HIS_ACIN_INTERACTIVE>> result = new ApiResultObject<List<HIS_ACIN_INTERACTIVE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_ACIN_INTERACTIVE> resultData = null;
                if (valid && new HisAcinInteractiveUpdate(param).UpdateList(data))
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
        public ApiResultObject<HIS_ACIN_INTERACTIVE> ChangeLock(HIS_ACIN_INTERACTIVE data)
        {
            ApiResultObject<HIS_ACIN_INTERACTIVE> result = new ApiResultObject<HIS_ACIN_INTERACTIVE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_ACIN_INTERACTIVE resultData = null;
                if (valid && new HisAcinInteractiveLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ACIN_INTERACTIVE data)
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
                    resultData = new HisAcinInteractiveTruncate(param).Truncate(data);
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
                    resultData = new HisAcinInteractiveTruncate(param).TruncateList(ids);
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
    }
}
