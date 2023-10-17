using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSereServPttt;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisSereServPtttController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServPtttFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSereServPtttFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV_PTTT>> result = new ApiResultObject<List<HIS_SERE_SERV_PTTT>>(null);
                if (param != null)
                {
                    HisSereServPtttManager mng = new HisSereServPtttManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisSereServPtttViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisSereServPtttViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV_PTTT>> result = new ApiResultObject<List<V_HIS_SERE_SERV_PTTT>>(null);
                if (param != null)
                {
                    HisSereServPtttManager mng = new HisSereServPtttManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SERE_SERV_PTTT> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_PTTT> result = new ApiResultObject<HIS_SERE_SERV_PTTT>(null);
                if (param != null)
                {
                    HisSereServPtttManager mng = new HisSereServPtttManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERE_SERV_PTTT> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_PTTT> result = new ApiResultObject<HIS_SERE_SERV_PTTT>(null);
                if (param != null)
                {
                    HisSereServPtttManager mng = new HisSereServPtttManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SERE_SERV_PTTT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSereServPtttManager mng = new HisSereServPtttManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_SERE_SERV_PTTT> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_PTTT> result = new ApiResultObject<HIS_SERE_SERV_PTTT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSereServPtttManager mng = new HisSereServPtttManager(param.CommonParam);
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
