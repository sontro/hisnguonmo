using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSereServReha;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisSereServRehaController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServRehaFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSereServRehaFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV_REHA>> result = new ApiResultObject<List<HIS_SERE_SERV_REHA>>(null);
                if (param != null)
                {
                    HisSereServRehaManager mng = new HisSereServRehaManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisSereServRehaViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisSereServRehaViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV_REHA>> result = new ApiResultObject<List<V_HIS_SERE_SERV_REHA>>(null);
                if (param != null)
                {
                    HisSereServRehaManager mng = new HisSereServRehaManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewByRehaSumId")]
        public ApiResult GetViewByRehaSumId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV_REHA>> result = new ApiResultObject<List<V_HIS_SERE_SERV_REHA>>(null);
                if (param != null)
                {
                    HisSereServRehaManager mng = new HisSereServRehaManager(param.CommonParam);
                    result = mng.GetViewByRehaSumId(param.ApiData);
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
        public ApiResult Create(ApiParam<HisSereServRehaSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV_REHA>> result = new ApiResultObject<List<HIS_SERE_SERV_REHA>>(null);
                if (param != null)
                {
                    HisSereServRehaManager mng = new HisSereServRehaManager(param.CommonParam);
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
        [ActionName("CreateSingle")]
        public ApiResult CreateSingle(ApiParam<HIS_SERE_SERV_REHA> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_REHA> result = new ApiResultObject<HIS_SERE_SERV_REHA>(null);
                if (param != null)
                {
                    HisSereServRehaManager mng = new HisSereServRehaManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERE_SERV_REHA> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_REHA> result = new ApiResultObject<HIS_SERE_SERV_REHA>(null);
                if (param != null)
                {
                    HisSereServRehaManager mng = new HisSereServRehaManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SERE_SERV_REHA> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSereServRehaManager mng = new HisSereServRehaManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_SERE_SERV_REHA> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_REHA> result = new ApiResultObject<HIS_SERE_SERV_REHA>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSereServRehaManager mng = new HisSereServRehaManager(param.CommonParam);
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
