using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBltyVolume;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisBltyVolumeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBltyVolumeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisBltyVolumeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BLTY_VOLUME>> result = new ApiResultObject<List<V_HIS_BLTY_VOLUME>>(null);
                if (param != null)
                {
                    HisBltyVolumeManager mng = new HisBltyVolumeManager(param.CommonParam);
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
