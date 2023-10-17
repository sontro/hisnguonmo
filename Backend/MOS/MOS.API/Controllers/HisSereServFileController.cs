using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSereServFile;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisSereServFileController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServFileFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSereServFileFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV_FILE>> result = new ApiResultObject<List<HIS_SERE_SERV_FILE>>(null);
                if (param != null)
                {
                    HisSereServFileManager mng = new HisSereServFileManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetFile")]
        public ApiResult GetFile(ApiParam<long> param)
        {
            try
            {
                HisSereServFileManager mng = new HisSereServFileManager(param.CommonParam);
                FileHolder result = param != null ? mng.GetFile(param.ApiData) : null;
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
        public ApiResult Create(ApiParam<HIS_SERE_SERV_FILE> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_FILE> result = new ApiResultObject<HIS_SERE_SERV_FILE>(null);
                if (param != null)
                {
                    HisSereServFileManager mng = new HisSereServFileManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERE_SERV_FILE> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_FILE> result = new ApiResultObject<HIS_SERE_SERV_FILE>(null);
                if (param != null)
                {
                    HisSereServFileManager mng = new HisSereServFileManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SERE_SERV_FILE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSereServFileManager mng = new HisSereServFileManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_SERE_SERV_FILE> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_FILE> result = new ApiResultObject<HIS_SERE_SERV_FILE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSereServFileManager mng = new HisSereServFileManager(param.CommonParam);
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
