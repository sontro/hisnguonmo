using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBranch;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisBranchController : BaseApiController
    {
        //cho phep truy cap ma ko can dang nhap vi o man hinh dang nhap cho phep chon branch
        [HttpGet]
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<HisBranchFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBranchFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BRANCH>> result = new ApiResultObject<List<HIS_BRANCH>>(null);
                if (param != null)
                {
                    HisBranchManager mng = new HisBranchManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisBranchSDO> param)
        {
            try
            {
                ApiResultObject<HIS_BRANCH> result = new ApiResultObject<HIS_BRANCH>(null);
                if (param != null)
                {
                    HisBranchManager mng = new HisBranchManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisBranchSDO> param)
        {
            try
            {
                ApiResultObject<HIS_BRANCH> result = new ApiResultObject<HIS_BRANCH>(null);
                if (param != null)
                {
                    HisBranchManager mng = new HisBranchManager(param.CommonParam);
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
                    HisBranchManager mng = new HisBranchManager(param.CommonParam);
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
                ApiResultObject<HIS_BRANCH> result = new ApiResultObject<HIS_BRANCH>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBranchManager mng = new HisBranchManager(param.CommonParam);
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
        [AllowAnonymous]
        [ActionName("CreateWeb")]
        public ApiResult CreateWeb(ApiParam<HisBranchWebSDO> param)
        {
            try
            {
                ApiResultObject<HIS_BRANCH> result = new ApiResultObject<HIS_BRANCH>(null);
                if (param != null)
                {
                    HisBranchManager mng = new HisBranchManager(param.CommonParam);
                    result = mng.CreateWeb(param.ApiData);
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
