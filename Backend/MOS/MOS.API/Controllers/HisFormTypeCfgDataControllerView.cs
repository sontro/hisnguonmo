using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisFormTypeCfgData;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisFormTypeCfgDataController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisFormTypeCfgDataViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisFormTypeCfgDataViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_FORM_TYPE_CFG_DATA>> result = new ApiResultObject<List<V_HIS_FORM_TYPE_CFG_DATA>>(null);
                if (param != null)
                {
                    HisFormTypeCfgDataManager mng = new HisFormTypeCfgDataManager(param.CommonParam);
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
    }
}
