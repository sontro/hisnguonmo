using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpMestController : BaseApiController
    {
        [HttpPost]
        [ActionName("ChmsCreate")]
        public ApiResult ChmsCreate(ApiParam<HisExpMestChmsSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.ChmsCreate(param.ApiData);
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
        [ActionName("ChmsUpdate")]
        public ApiResult ChmsUpdate(ApiParam<HisExpMestChmsSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.ChmsUpdate(param.ApiData);
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
        [ActionName("BcsCreate")]
        public ApiResult BcsCreate(ApiParam<HisExpMestBcsSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BcsCreate(param.ApiData);
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
        [ActionName("ChmsCreateList")]
        public ApiResult ChmsCreateList(ApiParam<HisExpMestChmsListSDO> param)
        {
            try
            {
                ApiResultObject<List<HisExpMestResultSDO>> result = new ApiResultObject<List<HisExpMestResultSDO>>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.ChmsCreateList(param.ApiData);
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
