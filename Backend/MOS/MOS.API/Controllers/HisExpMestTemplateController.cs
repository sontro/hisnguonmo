using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestTemplate;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisExpMestTemplateController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestTemplateFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExpMestTemplateFilterQuery> param)
        {
            try
            {
				ApiResultObject<List<HIS_EXP_MEST_TEMPLATE>> result = new ApiResultObject<List<HIS_EXP_MEST_TEMPLATE>>(null);
				if (param != null)
				{
					HisExpMestTemplateManager mng = new HisExpMestTemplateManager(param.CommonParam);
					result = mng.Get(param.ApiData);
				}
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HisExpMestTemplateSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestTemplateSDO> result = new ApiResultObject<HisExpMestTemplateSDO>(null);
                if (param != null)
                {
					HisExpMestTemplateManager mng = new HisExpMestTemplateManager(param.CommonParam);
                    result = mng.Create(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HisExpMestTemplateSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestTemplateSDO> result = new ApiResultObject<HisExpMestTemplateSDO>(null);
                if (param != null)
                {
					HisExpMestTemplateManager mng = new HisExpMestTemplateManager(param.CommonParam);
                    result = mng.Update(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExpMestTemplateManager mng = new HisExpMestTemplateManager(param.CommonParam);
                    result = mng.Delete(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
        
        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult Lock(ApiParam<HIS_EXP_MEST_TEMPLATE> param)
        {
			try
            {
				ApiResultObject<HIS_EXP_MEST_TEMPLATE> result = new ApiResultObject<HIS_EXP_MEST_TEMPLATE>(null);
				if (param != null && param.ApiData != null)
				{
					HisExpMestTemplateManager mng = new HisExpMestTemplateManager(param.CommonParam);
					result = mng.ChangeLock(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
