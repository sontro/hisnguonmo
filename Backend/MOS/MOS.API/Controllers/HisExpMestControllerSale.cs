using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
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
        [ActionName("SaleCreate")]
        public ApiResult SaleCreate(ApiParam<HisExpMestSaleSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.SaleCreate(param.ApiData);
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
        [ActionName("SaleUpdate")]
        public ApiResult SaleUpdate(ApiParam<HisExpMestSaleSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.SaleUpdate(param.ApiData);
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
        [ActionName("NotTaken")]
        public ApiResult NotTaken(ApiParam<HIS_EXP_MEST> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.NotTaken(param.ApiData);
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
        [ActionName("SaleCreateList")]
        public ApiResult SaleCreateList(ApiParam<List<HisExpMestSaleSDO>> param)
        {
            try
            {
                ApiResultObject<List<HisExpMestSaleResultSDO>> result = new ApiResultObject<List<HisExpMestSaleResultSDO>>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.SaleCreateList(param.ApiData);
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
        [ActionName("SaleCreateListSdo")]
        public ApiResult SaleCreateListSdo(ApiParam<HisExpMestSaleListSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestSaleListResultSDO> result = new ApiResultObject<HisExpMestSaleListResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.SaleCreateListSdo(param.ApiData);
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
        [ActionName("SaleCreateBillList")]
        public ApiResult SaleCreateBillList(ApiParam<List<HisExpMestSaleListSDO>> param)
        {
            try
            {
                ApiResultObject<List<HisExpMestSaleListResultSDO>> result = new ApiResultObject<List<HisExpMestSaleListResultSDO>>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.SaleCreateBillList(param.ApiData);
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
        [ActionName("SaleUpdateList")]
        public ApiResult SaleUpdateList(ApiParam<List<HisExpMestSaleSDO>> param)
        {
            try
            {
                ApiResultObject<List<HisExpMestSaleResultSDO>> result = new ApiResultObject<List<HisExpMestSaleResultSDO>>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.SaleUpdateList(param.ApiData);
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
        [ActionName("SaleUpdateListSdo")]
        public ApiResult SaleUpdateListSdo(ApiParam<HisExpMestSaleListSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestSaleListResultSDO> result = new ApiResultObject<HisExpMestSaleListResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.SaleUpdateListSdo(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisExpMestForSaleFilter>), "param")]
        [ActionName("GetForSale")]
        public ApiResult GetForSale(ApiParam<HisExpMestForSaleFilter> param)
        {
            try
            {
                ApiResultObject<HisExpMestForSaleSDO> result = new ApiResultObject<HisExpMestForSaleSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.GetForSale(param.ApiData);
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
