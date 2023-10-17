using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisContactPoint;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisContactPointController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisContactPointFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisContactPointFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CONTACT_POINT>> result = new ApiResultObject<List<HIS_CONTACT_POINT>>(null);
                if (param != null)
                {
                    HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_CONTACT_POINT> param)
        {
            try
            {
                ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
                if (param != null)
                {
                    HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
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
        [ActionName("Save")]
        public ApiResult Save(ApiParam<HIS_CONTACT_POINT> param)
        {
            try
            {
                ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
                if (param != null)
                {
                    HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
                    result = mng.Save(param.ApiData);
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
        [ActionName("AddContactInfo")]
        public ApiResult AddContactInfo(ApiParam<HisContactSDO> param)
        {
            try
            {
                ApiResultObject<HisContactResultSDO> result = new ApiResultObject<HisContactResultSDO>(null);
                if (param != null)
                {
                    HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
                    result = mng.AddContactInfo(param.ApiData);
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
        [ActionName("SetContactLevel")]
        public ApiResult SetContactLevel(ApiParam<HisContactLevelSDO> param)
        {
            try
            {
                ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
                if (param != null)
                {
                    HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
                    result = mng.SetContactLevel(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_CONTACT_POINT> param)
        {
            try
            {
                ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
                if (param != null)
                {
                    HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
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
                    HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
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
                ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
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
            ApiResultObject<HIS_CONTACT_POINT> result = null;
            if (param != null && param.ApiData != null)
            {
                HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
