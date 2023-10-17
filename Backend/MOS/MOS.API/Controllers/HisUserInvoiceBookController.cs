using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisUserInvoiceBook;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisUserInvoiceBookController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisUserInvoiceBookFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisUserInvoiceBookFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_USER_INVOICE_BOOK>> result = new ApiResultObject<List<HIS_USER_INVOICE_BOOK>>(null);
                if (param != null)
                {
                    HisUserInvoiceBookManager mng = new HisUserInvoiceBookManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisUserInvoiceBookViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisUserInvoiceBookViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_USER_INVOICE_BOOK>> result = new ApiResultObject<List<V_HIS_USER_INVOICE_BOOK>>(null);
                if (param != null)
                {
                    HisUserInvoiceBookManager mng = new HisUserInvoiceBookManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisUserInvoiceBookSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_USER_INVOICE_BOOK>> result = new ApiResultObject<List<HIS_USER_INVOICE_BOOK>>(null);
                if (param != null)
                {
                    HisUserInvoiceBookManager mng = new HisUserInvoiceBookManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_USER_INVOICE_BOOK> param)
        {
            try
            {
                ApiResultObject<HIS_USER_INVOICE_BOOK> result = new ApiResultObject<HIS_USER_INVOICE_BOOK>(null);
                if (param != null)
                {
                    HisUserInvoiceBookManager mng = new HisUserInvoiceBookManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_USER_INVOICE_BOOK> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisUserInvoiceBookManager mng = new HisUserInvoiceBookManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_USER_INVOICE_BOOK> param)
        {
            try
            {
                ApiResultObject<HIS_USER_INVOICE_BOOK> result = new ApiResultObject<HIS_USER_INVOICE_BOOK>(null);
                if (param != null && param.ApiData != null)
                {
                    HisUserInvoiceBookManager mng = new HisUserInvoiceBookManager(param.CommonParam);
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
