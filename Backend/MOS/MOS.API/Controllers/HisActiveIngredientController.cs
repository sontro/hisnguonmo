using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisActiveIngredient;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisActiveIngredientController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisActiveIngredientFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisActiveIngredientFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACTIVE_INGREDIENT>> result = new ApiResultObject<List<HIS_ACTIVE_INGREDIENT>>(null);
                if (param != null)
                {
                    HisActiveIngredientManager mng = new HisActiveIngredientManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ACTIVE_INGREDIENT> param)
        {
            try
            {
                ApiResultObject<HIS_ACTIVE_INGREDIENT> result = new ApiResultObject<HIS_ACTIVE_INGREDIENT>(null);
                if (param != null)
                {
                    HisActiveIngredientManager mng = new HisActiveIngredientManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ACTIVE_INGREDIENT> param)
        {
            try
            {
                ApiResultObject<HIS_ACTIVE_INGREDIENT> result = new ApiResultObject<HIS_ACTIVE_INGREDIENT>(null);
                if (param != null)
                {
                    HisActiveIngredientManager mng = new HisActiveIngredientManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_ACTIVE_INGREDIENT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisActiveIngredientManager mng = new HisActiveIngredientManager(param.CommonParam);
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
        [ActionName("TruncateAll")]
        public ApiResult TruncateAll()
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                HisActiveIngredientManager mng = new HisActiveIngredientManager(new CommonParam());
                result = mng.TruncateAll();
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_ACTIVE_INGREDIENT> result = new ApiResultObject<HIS_ACTIVE_INGREDIENT>(null);
                if (param != null)
                {
                    HisActiveIngredientManager mng = new HisActiveIngredientManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_ACTIVE_INGREDIENT>> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACTIVE_INGREDIENT>> result = new ApiResultObject<List<HIS_ACTIVE_INGREDIENT>>(null);
                if (param != null)
                {
                    HisActiveIngredientManager mng = new HisActiveIngredientManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<HIS_ACTIVE_INGREDIENT>> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACTIVE_INGREDIENT>> result = new ApiResultObject<List<HIS_ACTIVE_INGREDIENT>>(null);
                if (param != null)
                {
                    HisActiveIngredientManager mng = new HisActiveIngredientManager(param.CommonParam);
                    result = mng.UpdateList(param.ApiData);
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
