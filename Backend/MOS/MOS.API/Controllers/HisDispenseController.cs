using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDispense;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisDispenseController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDispenseFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDispenseFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DISPENSE>> result = new ApiResultObject<List<HIS_DISPENSE>>(null);
                if (param != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
        
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDispenseViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisDispenseViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DISPENSE>> result = new ApiResultObject<List<V_HIS_DISPENSE>>(null);
                if (param != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HisDispenseSDO> param)
        {
            try
            {
                ApiResultObject<HisDispenseHandlerResultSDO> result = new ApiResultObject<HisDispenseHandlerResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.Create(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HisDispenseUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HisDispenseHandlerResultSDO> result = new ApiResultObject<HisDispenseHandlerResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.Update(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_DISPENSE> result = new ApiResultObject<HIS_DISPENSE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.ChangeLock(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HisDispenseDeleteSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.Delete(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Confirm")]
        public ApiResult Confirm(ApiParam<HisDispenseConfirmSDO> param)
        {
            try
            {
                ApiResultObject<HisDispenseResultSDO> result = new ApiResultObject<HisDispenseResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.Confirm(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UnConfirm")]
        public ApiResult UnConfirm(ApiParam<HisDispenseConfirmSDO> param)
        {
            try
            {
                ApiResultObject<HisDispenseResultSDO> result = new ApiResultObject<HisDispenseResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.UnConfirm(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }


        [HttpPost]
        [ActionName("PackingCreate")]
        public ApiResult PackingCreate(ApiParam<HisPackingCreateSDO> param)
        {
            try
            {
                ApiResultObject<HisPackingResultSDO> result = new ApiResultObject<HisPackingResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.PackingCreate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("PackingUpdate")]
        public ApiResult PackingUpdate(ApiParam<HisPackingUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HisPackingResultSDO> result = new ApiResultObject<HisPackingResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.PackingUpdate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("PackingDelete")]
        public ApiResult PackingDelete(ApiParam<HisPackingSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.PackingDelete(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("PackingConfirm")]
        public ApiResult PackingConfirm(ApiParam<HisPackingSDO> param)
        {
            try
            {
                ApiResultObject<HisPackingResultSDO> result = new ApiResultObject<HisPackingResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.PackingConfirm(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("PackingUnconfirm")]
        public ApiResult PackingUnconfirm(ApiParam<HisPackingSDO> param)
        {
            try
            {
                ApiResultObject<HisPackingResultSDO> result = new ApiResultObject<HisPackingResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDispenseManager mng = new HisDispenseManager(param.CommonParam);
                    result = mng.PackingUnconfirm(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

    }
}