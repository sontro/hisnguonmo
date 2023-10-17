using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestTypeUser;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisImpMestTypeUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestTypeUserFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisImpMestTypeUserFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> result = new ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>>(null);
                if (param != null)
                {
                    HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_IMP_MEST_TYPE_USER> param)
        {
            try
            {
                ApiResultObject<HIS_IMP_MEST_TYPE_USER> result = new ApiResultObject<HIS_IMP_MEST_TYPE_USER>(null);
                if (param != null)
                {
                    HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_IMP_MEST_TYPE_USER>> param)
        {
            try
            {
                ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> result = new ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>>(null);
                if (param != null)
                {
                    HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_IMP_MEST_TYPE_USER> param)
        {
            try
            {
                ApiResultObject<HIS_IMP_MEST_TYPE_USER> result = new ApiResultObject<HIS_IMP_MEST_TYPE_USER>(null);
                if (param != null)
                {
                    HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
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
                    HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
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
                ApiResultObject<HIS_IMP_MEST_TYPE_USER> result = new ApiResultObject<HIS_IMP_MEST_TYPE_USER>(null);
                if (param != null && param.ApiData != null)
                {
                    HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
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
            ApiResultObject<HIS_IMP_MEST_TYPE_USER> result = null;
            if (param != null && param.ApiData != null)
            {
                HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
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
                    HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
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
        [ActionName("CopyByType")]
        public ApiResult CopyByType(ApiParam<HisImpMestTypeUserCopyByTypeSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> result = new ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>>(null);
                if (param != null)
                {
                    HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
                    result = mng.CopyByType(param.ApiData);
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
        [ActionName("CopyByLoginname")]
        public ApiResult CopyByLoginname(ApiParam<HisImpMestTypeUserCopyByLoginnameSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> result = new ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>>(null);
                if (param != null)
                {
                    HisImpMestTypeUserManager mng = new HisImpMestTypeUserManager(param.CommonParam);
                    result = mng.CopyByLoginname(param.ApiData);
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
