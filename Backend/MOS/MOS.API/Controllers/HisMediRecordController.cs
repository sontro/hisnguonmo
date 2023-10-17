using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediRecord;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMediRecordController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediRecordFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMediRecordFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_RECORD>> result = new ApiResultObject<List<HIS_MEDI_RECORD>>(null);
                if (param != null)
                {
                    HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEDI_RECORD> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_RECORD> result = new ApiResultObject<HIS_MEDI_RECORD>(null);
                if (param != null)
                {
                    HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEDI_RECORD> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_RECORD> result = new ApiResultObject<HIS_MEDI_RECORD>(null);
                if (param != null)
                {
                    HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_RECORD> result = new ApiResultObject<HIS_MEDI_RECORD>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
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
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("UnStore")]
        public ApiResult UnStore(ApiParam<long> param)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
                result = mng.UnStore(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Store")]
        public ApiResult Store(ApiParam<HisMediRecordStoreSDO> param)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
                result = mng.Store(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("StoreList")]
        public ApiResult StoreList(ApiParam<List<HisMediRecordStoreSDO>> param)
        {
            ApiResultObject<List<HIS_MEDI_RECORD>> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
                result = mng.Store(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
