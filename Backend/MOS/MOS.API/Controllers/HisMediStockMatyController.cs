using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediStockMaty;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMediStockMatyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediStockMatyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMediStockMatyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMediStockMatyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMediStockMatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<V_HIS_MEDI_STOCK_MATY>>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMediStockMatyView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView(ApiParam<HisMediStockMatyView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_STOCK_MATY_1>> result = new ApiResultObject<List<V_HIS_MEDI_STOCK_MATY_1>>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEDI_STOCK_MATY> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK_MATY> result = new ApiResultObject<HIS_MEDI_STOCK_MATY>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_MEDI_STOCK_MATY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        public ApiResult UpdateList(ApiParam<List<HIS_MEDI_STOCK_MATY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEDI_STOCK_MATY> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK_MATY> result = new ApiResultObject<HIS_MEDI_STOCK_MATY>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MEDI_STOCK_MATY> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_MEDI_STOCK_MATY> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK_MATY> result = new ApiResultObject<HIS_MEDI_STOCK_MATY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        public ApiResult CopyByMediStock(ApiParam<HisMestMatyCopyByMediStockSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
        [ActionName("CopyByMaty")]
        public ApiResult CopyByMaty(ApiParam<HisMestMatyCopyByMatySDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
                    result = mng.CopyByMaty(param.ApiData);
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
        public ApiResult Import(ApiParam<List<HIS_MEDI_STOCK_MATY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);
                if (param != null)
                {
                    HisMediStockMatyManager mng = new HisMediStockMatyManager(param.CommonParam);
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
