using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTestIndex;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTestIndexController : BaseApiController
    {
        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTestIndexFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisTestIndexFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_TEST_INDEX>> result = new ApiResultObject<List<HIS_TEST_INDEX>>(null);
                if (param != null)
                {
                    HisTestIndexManager mng = new HisTestIndexManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTestIndexViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisTestIndexViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TEST_INDEX>> result = new ApiResultObject<List<V_HIS_TEST_INDEX>>(null);
                if (param != null)
                {
                    HisTestIndexManager mng = new HisTestIndexManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_TEST_INDEX> param)
        {
            try
            {
                ApiResultObject<HIS_TEST_INDEX> result = new ApiResultObject<HIS_TEST_INDEX>(null);
                if (param != null)
                {
                    HisTestIndexManager mng = new HisTestIndexManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_TEST_INDEX> param)
        {
            try
            {
                ApiResultObject<HIS_TEST_INDEX> result = new ApiResultObject<HIS_TEST_INDEX>(null);
                if (param != null)
                {
                    HisTestIndexManager mng = new HisTestIndexManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HIS_TEST_INDEX> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTestIndexManager mng = new HisTestIndexManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult Lock(ApiParam<HIS_TEST_INDEX> param)
        {
            try
            {
                ApiResultObject<HIS_TEST_INDEX> result = new ApiResultObject<HIS_TEST_INDEX>(null);
                if (param != null)
                {
                    HisTestIndexManager mng = new HisTestIndexManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_TEST_INDEX>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TEST_INDEX>> result = new ApiResultObject<List<HIS_TEST_INDEX>>(null);
                if (param != null)
                {
                    HisTestIndexManager mng = new HisTestIndexManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
