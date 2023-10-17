using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisKskContract;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisKskContractController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisKskContractFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisKskContractFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_KSK_CONTRACT>> result = new ApiResultObject<List<HIS_KSK_CONTRACT>>(null);
                if (param != null)
                {
                    HisKskContractManager mng = new HisKskContractManager(param.CommonParam);
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
        [ActionName("List")]
        [AllowAnonymous]
        public ApiResult List()
        {
            try
            {
                ApiResultObject<List<HisKskContractTDO>> result = new ApiResultObject<List<HisKskContractTDO>>(null);
                HisKskContractManager mng = new HisKskContractManager(new CommonParam());
                result = mng.GetTdo(null, null);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ActionName("List")]
        [AllowAnonymous]
        public ApiResult List(long? fromTime, long? toTime)
        {
            try
            {
                ApiResultObject<List<HisKskContractTDO>> result = new ApiResultObject<List<HisKskContractTDO>>(null);
                HisKskContractManager mng = new HisKskContractManager(new CommonParam());
                result = mng.GetTdo(fromTime, toTime);
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
        public ApiResult Create(ApiParam<KsKContractSDO> param)
        {
            try
            {
                ApiResultObject<HIS_KSK_CONTRACT> result = new ApiResultObject<HIS_KSK_CONTRACT>(null);
                if (param != null)
                {
                    HisKskContractManager mng = new HisKskContractManager(param.CommonParam);
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
        [ActionName("Import")]
        public ApiResult Import(ApiParam<HisKskContractSDO> param)
        {
            try
            {
                ApiResultObject<HisKskContractSDO> result = new ApiResultObject<HisKskContractSDO>(null);
                if (param != null)
                {
                    HisKskContractManager mng = new HisKskContractManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<KsKContractSDO> param)
        {
            try
            {
                ApiResultObject<HIS_KSK_CONTRACT> result = new ApiResultObject<HIS_KSK_CONTRACT>(null);
                if (param != null)
                {
                    HisKskContractManager mng = new HisKskContractManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_KSK_CONTRACT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisKskContractManager mng = new HisKskContractManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_KSK_CONTRACT> param)
        {
            try
            {
                ApiResultObject<HIS_KSK_CONTRACT> result = new ApiResultObject<HIS_KSK_CONTRACT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisKskContractManager mng = new HisKskContractManager(param.CommonParam);
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
