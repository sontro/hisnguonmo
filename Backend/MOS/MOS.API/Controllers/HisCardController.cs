using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCard;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisCardController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCardFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisCardFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARD>> result = new ApiResultObject<List<HIS_CARD>>(null);
                if (param != null)
                {
                    HisCardManager mng = new HisCardManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisCardViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisCardViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CARD>> result = new ApiResultObject<List<V_HIS_CARD>>(null);
                if (param != null)
                {
                    HisCardManager mng = new HisCardManager(param.CommonParam);
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
        [ActionName("CreateOrUpdate")]
        public ApiResult CreateOrUpdate(ApiParam<HIS_CARD> param)
        {
            try
            {
                ApiResultObject<HIS_CARD> result = new ApiResultObject<HIS_CARD>(null);
                if (param != null)
                {
                    HisCardManager mng = new HisCardManager(param.CommonParam);
                    result = mng.CreateOrUpdate(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetCardSdoByCode")]
        public ApiResult GetCardSdoByCode(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<HisCardSDO> result = new ApiResultObject<HisCardSDO>(null);
                if (param != null)
                {
                    HisCardManager mng = new HisCardManager(param.CommonParam);
                    result = mng.GetCardSdoByCode(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetViewByCode")]
        public ApiResult GetViewByCode(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<V_HIS_CARD> result = new ApiResultObject<V_HIS_CARD>(null);
                if (param != null)
                {
                    HisCardManager mng = new HisCardManager(param.CommonParam);
                    result = mng.GetCardByCode(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetCardSdoByPhone")]
        public ApiResult GetCardSdoByPhone(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<List<HisCardSDO>> result = new ApiResultObject<List<HisCardSDO>>(null);
                if (param != null)
                {
                    HisCardManager mng = new HisCardManager(param.CommonParam);
                    result = mng.GetCardSdoByPhone(param.ApiData);
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
