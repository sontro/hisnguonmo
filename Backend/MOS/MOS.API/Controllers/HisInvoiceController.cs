using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisInvoice;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisInvoiceController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisInvoiceFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisInvoiceFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_INVOICE>> result = new ApiResultObject<List<HIS_INVOICE>>(null);
                if (param != null)
                {
                    HisInvoiceManager mng = new HisInvoiceManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisInvoiceViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisInvoiceViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_INVOICE>> result = new ApiResultObject<List<V_HIS_INVOICE>>(null);
                if (param != null)
                {
                    HisInvoiceManager mng = new HisInvoiceManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisInvoiceSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_INVOICE> result = new ApiResultObject<V_HIS_INVOICE>(null);
                if (param != null)
                {
                    HisInvoiceManager mng = new HisInvoiceManager(param.CommonParam);
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
        [ActionName("Cancel")]
        public ApiResult Cancel(ApiParam<HIS_INVOICE> param)
        {
            try
            {
                ApiResultObject<HIS_INVOICE> result = new ApiResultObject<HIS_INVOICE>(null);
                if (param != null)
                {
                    HisInvoiceManager mng = new HisInvoiceManager(param.CommonParam);
                    result = mng.Cancel(param.ApiData);
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
        [ActionName("UpdateInfo")]
        public ApiResult UpdateInfo(ApiParam<HisInvoiceUpdateInfoSDO> param)
        {
            try
            {
                ApiResultObject<HIS_INVOICE> result = new ApiResultObject<HIS_INVOICE>(null);
                if (param != null)
                {
                    HisInvoiceManager mng = new HisInvoiceManager(param.CommonParam);
                    result = mng.UpdateInfo(param.ApiData);
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
        public ApiResult ChangeLock(ApiParam<HIS_INVOICE> param)
        {
            try
            {
                ApiResultObject<HIS_INVOICE> result = new ApiResultObject<HIS_INVOICE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisInvoiceManager mng = new HisInvoiceManager(param.CommonParam);
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
