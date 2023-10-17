using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisAccountBook;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisAccountBookController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAccountBookFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAccountBookFilterQuery> param)
        {
            try
            {
                Inventec.Token.Core.TokenData data = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenData();
                ApiResultObject<List<HIS_ACCOUNT_BOOK>> result = new ApiResultObject<List<HIS_ACCOUNT_BOOK>>(null);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAccountBookViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisAccountBookViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ACCOUNT_BOOK>> result = new ApiResultObject<List<V_HIS_ACCOUNT_BOOK>>(null);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_ACCOUNT_BOOK> param)
        {
            try
            {
                ApiResultObject<HIS_ACCOUNT_BOOK> result = new ApiResultObject<HIS_ACCOUNT_BOOK>(null);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_ACCOUNT_BOOK>> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACCOUNT_BOOK>> result = new ApiResultObject<List<HIS_ACCOUNT_BOOK>>(null);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_ACCOUNT_BOOK> param)
        {
            try
            {
                ApiResultObject<HIS_ACCOUNT_BOOK> result = new ApiResultObject<HIS_ACCOUNT_BOOK>(null);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_ACCOUNT_BOOK> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_ACCOUNT_BOOK> param)
        {
            ApiResultObject<HIS_ACCOUNT_BOOK> result = new ApiResultObject<HIS_ACCOUNT_BOOK>(null);
            if (param != null && param.ApiData != null)
            {
                HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                result = mng.ChangeLock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAccountBookGeneralInfoFilter>), "param")]
        [ActionName("GetGeneralInfo")]
        public ApiResult GetGeneralInfo(ApiParam<HisAccountBookGeneralInfoFilter> param)
        {
            try
            {
                Inventec.Token.Core.TokenData data = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenData();
                ApiResultObject<List<HisAccountBookGeneralInfoSDO>> result = new ApiResultObject<List<HisAccountBookGeneralInfoSDO>>(null);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                    result = mng.GetGeneralInfo(param.ApiData);
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
