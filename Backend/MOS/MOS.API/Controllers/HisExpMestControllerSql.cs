using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpMestController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<DHisExpMestDetail1Filter>), "param")]
        [ActionName("GetExpMestDetail1")]
        public ApiResult GetExpMestDetail1(ApiParam<DHisExpMestDetail1Filter> param)
        {
            try
            {
                ApiResultObject<List<D_HIS_EXP_MEST_DETAIL_1>> result = new ApiResultObject<List<D_HIS_EXP_MEST_DETAIL_1>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.GetExpMestDetail1(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisExpMestBcsMoreInfoFilter>), "param")]
        [ActionName("GetBcsMoreInfo")]
        public ApiResult GetBcsMoreInfo(ApiParam<HisExpMestBcsMoreInfoFilter> param)
        {
            try
            {
                ApiResultObject<HisExpMestBcsMoreInfoSDO> result = new ApiResultObject<HisExpMestBcsMoreInfoSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.GetBcsMoreInfo(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisExpMestTutorialFilter>), "param")]
        [ActionName("GetExpMestTutorial")]
        public ApiResult GetExpMestTutorial(ApiParam<HisExpMestTutorialFilter> param)
        {
            try
            {
                ApiResultObject<List<ExpMestTutorialSDO>> result = new ApiResultObject<List<ExpMestTutorialSDO>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.GetExpMestTutorial(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisExpMestGroupByTreatmentFilter>), "param")]
        [ActionName("GetExpMestGroupByTreatment")]
        public ApiResult GetExpMestGroupByTreatment(ApiParam<HisExpMestGroupByTreatmentFilter> param)
        {
            try
            {
                ApiResultObject<List<HisExpMestGroupByTreatmentSDO>> result = new ApiResultObject<List<HisExpMestGroupByTreatmentSDO>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.GetExpMestGroupByTreatment(param.ApiData);
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
