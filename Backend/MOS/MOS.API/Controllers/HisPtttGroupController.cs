using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPtttGroup;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisPtttGroupController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPtttGroupFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPtttGroupFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PTTT_GROUP>> result = new ApiResultObject<List<HIS_PTTT_GROUP>>(null);
                if (param != null)
                {
                    HisPtttGroupManager mng = new HisPtttGroupManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisPtttGroupSDO> param)
        {
            try
            {
                ApiResultObject<HisPtttGroupSDO> result = new ApiResultObject<HisPtttGroupSDO>(null);
                if (param != null)
                {
                    HisPtttGroupManager mng = new HisPtttGroupManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisPtttGroupSDO> param)
        {
            try
            {
                ApiResultObject<HisPtttGroupSDO> result = new ApiResultObject<HisPtttGroupSDO>(null);
                if (param != null)
                {
                    HisPtttGroupManager mng = new HisPtttGroupManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_PTTT_GROUP> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisPtttGroupManager mng = new HisPtttGroupManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_PTTT_GROUP> param)
        {
            try
            {
                ApiResultObject<HIS_PTTT_GROUP> result = new ApiResultObject<HIS_PTTT_GROUP>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPtttGroupManager mng = new HisPtttGroupManager(param.CommonParam);
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
