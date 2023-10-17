using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediStockMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMediStockMetyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediStockMetyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMediStockMetyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMediStockMetyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMediStockMetyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<V_HIS_MEDI_STOCK_METY>>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMediStockMetyView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView(ApiParam<HisMediStockMetyView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_STOCK_METY_1>> result = new ApiResultObject<List<V_HIS_MEDI_STOCK_METY_1>>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_MEDI_STOCK_METY> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK_METY> result = new ApiResultObject<HIS_MEDI_STOCK_METY>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_MEDI_STOCK_METY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
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
        public ApiResult UpdateList(ApiParam<List<HIS_MEDI_STOCK_METY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_MEDI_STOCK_METY> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK_METY> result = new ApiResultObject<HIS_MEDI_STOCK_METY>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MEDI_STOCK_METY> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
                    result = mng.DeleteList(param.ApiData);
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
        public ApiResult ChangeLock(ApiParam<HIS_MEDI_STOCK_METY> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK_METY> result = new ApiResultObject<HIS_MEDI_STOCK_METY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
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
        [ActionName("CopyByMediStock")]
        public ApiResult CopyByMediStock(ApiParam<HisMestMetyCopyByMediStockSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
                    result = mng.CopyByMediStock(param.ApiData);
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
        [ActionName("CopyByMety")]
        public ApiResult CopyByMety(ApiParam<HisMestMetyCopyByMetySDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
                    result = mng.CopyByMety(param.ApiData);
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
        [ActionName("Import")]
        public ApiResult Import(ApiParam<List<HIS_MEDI_STOCK_METY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);
                if (param != null)
                {
                    HisMediStockMetyManager mng = new HisMediStockMetyManager(param.CommonParam);
                    result = mng.Import(param.ApiData);
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
