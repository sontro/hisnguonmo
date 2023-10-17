using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisImpMestController : BaseApiController
    {

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestManuViewFilterQuery>), "param")]
        [ActionName("GetManuView")]
        public ApiResult GetManuView(ApiParam<HisImpMestManuViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_MANU>> result = new ApiResultObject<List<V_HIS_IMP_MEST_MANU>>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.GetManuView(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisImpMestView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisImpMestView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_1>> result = new ApiResultObject<List<V_HIS_IMP_MEST_1>>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisImpMestView2FilterQuery>), "param")]
        [ActionName("GetView2")]
        public ApiResult GetView2(ApiParam<HisImpMestView2FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_2>> result = new ApiResultObject<List<V_HIS_IMP_MEST_2>>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.GetView2(param.ApiData);
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
