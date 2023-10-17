using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediStockPeriod;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMediStockPeriodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediStockPeriodFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMediStockPeriodFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_PERIOD>> result = new ApiResultObject<List<HIS_MEDI_STOCK_PERIOD>>(null);
                if (param != null)
                {
                    HisMediStockPeriodManager mng = new HisMediStockPeriodManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMediStockPeriodViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMediStockPeriodViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_STOCK_PERIOD>> result = new ApiResultObject<List<V_HIS_MEDI_STOCK_PERIOD>>(null);
                if (param != null)
                {
                    HisMediStockPeriodManager mng = new HisMediStockPeriodManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEDI_STOCK_PERIOD> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK_PERIOD> result = new ApiResultObject<HIS_MEDI_STOCK_PERIOD>(null);
                if (param != null)
                {
                    HisMediStockPeriodManager mng = new HisMediStockPeriodManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEDI_STOCK_PERIOD> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK_PERIOD> result = new ApiResultObject<HIS_MEDI_STOCK_PERIOD>(null);
                if (param != null)
                {
                    HisMediStockPeriodManager mng = new HisMediStockPeriodManager(param.CommonParam);
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
        [ActionName("UpdateInventory")]
        public ApiResult UpdateInventory(ApiParam<HisMediStockPeriodInventorySDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMediStockPeriodManager mng = new HisMediStockPeriodManager(param.CommonParam);
                    result = mng.UpdateInventory(param.ApiData);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMediStockPeriodManager mng = new HisMediStockPeriodManager(param.CommonParam);
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
        [ActionName("Approve")]
        public ApiResult Approve(ApiParam<HisMestPeriodApproveSDO> param)
        {
            try
            {
                ApiResultObject<HisMestPeriodApproveResultSDO> result = new ApiResultObject<HisMestPeriodApproveResultSDO>(null);
                if (param != null)
                {
                    HisMediStockPeriodManager mng = new HisMediStockPeriodManager(param.CommonParam);
                    result = mng.Approve(param.ApiData);
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
        [ActionName("Unapprove")]
        public ApiResult Unapprove(ApiParam<HisMestPeriodApproveSDO> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK_PERIOD> result = new ApiResultObject<HIS_MEDI_STOCK_PERIOD>(null);
                if (param != null)
                {
                    HisMediStockPeriodManager mng = new HisMediStockPeriodManager(param.CommonParam);
                    result = mng.Unapprove(param.ApiData);
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
