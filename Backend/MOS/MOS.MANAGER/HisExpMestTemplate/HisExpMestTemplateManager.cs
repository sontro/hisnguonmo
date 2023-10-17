using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestTemplate
{
    public class HisExpMestTemplateManager : BusinessBase
    {
        public HisExpMestTemplateManager()
            : base()
        {

        }
		
		public HisExpMestTemplateManager(CommonParam param)
            : base(param)
        {

        }
		
        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST_TEMPLATE>> Get(HisExpMestTemplateFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST_TEMPLATE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
				List<HIS_EXP_MEST_TEMPLATE> resultData = null;
                if (valid)
                {
					resultData = new HisExpMestTemplateGet(param).Get(filter);
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
        public ApiResultObject<HisExpMestTemplateSDO> Create(HisExpMestTemplateSDO data)
        {
            ApiResultObject<HisExpMestTemplateSDO> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisExpMestTemplateSDO resultData = null;
                if (valid && new HisExpMestTemplateCreate(param).Create(data))
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
        public ApiResultObject<HisExpMestTemplateSDO> Update(HisExpMestTemplateSDO data)
        {
            ApiResultObject<HisExpMestTemplateSDO> result = new ApiResultObject<HisExpMestTemplateSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.ExpMestTemplate);
                HisExpMestTemplateSDO resultData = null;
                if (valid && new HisExpMestTemplateUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXP_MEST_TEMPLATE> ChangeLock(HIS_EXP_MEST_TEMPLATE data)
        {
            ApiResultObject<HIS_EXP_MEST_TEMPLATE> result = new ApiResultObject<HIS_EXP_MEST_TEMPLATE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
				HIS_EXP_MEST_TEMPLATE resultData = null;
                if (valid && new HisExpMestTemplateLock(param).ChangeLock(data))
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
                    resultData = new HisExpMestTemplateTruncate(param).Truncate(id);
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
