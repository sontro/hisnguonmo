using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediReact;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMediReactController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediReactFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMediReactFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_REACT>> result = new ApiResultObject<List<HIS_MEDI_REACT>>(null);
                if (param != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMediReactViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMediReactViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_REACT>> result = new ApiResultObject<List<V_HIS_MEDI_REACT>>(null);
                if (param != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEDI_REACT> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
                if (param != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEDI_REACT> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
                if (param != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
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
        [ActionName("Execute")]
        public ApiResult Execute(ApiParam<HIS_MEDI_REACT> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
                if (param != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
                    result = mng.Execute(param.ApiData);
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
        [ActionName("UnExecute")]
        public ApiResult UnExecute(ApiParam<HIS_MEDI_REACT> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
                if (param != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
                    result = mng.UnExecute(param.ApiData);
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
        [ActionName("Check")]
        public ApiResult Check(ApiParam<HIS_MEDI_REACT> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
                if (param != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
                    result = mng.Check(param.ApiData);
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
        [ActionName("UnCheck")]
        public ApiResult UnCheck(ApiParam<HIS_MEDI_REACT> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
                if (param != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
                    result = mng.UnCheck(param.ApiData);
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
        public ApiResult Delete(ApiParam<HIS_MEDI_REACT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_MEDI_REACT> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT> result = new ApiResultObject<HIS_MEDI_REACT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMediReactManager mng = new HisMediReactManager(param.CommonParam);
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
