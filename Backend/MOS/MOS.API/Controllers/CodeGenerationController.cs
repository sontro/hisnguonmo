using Inventec.Common.Logging;
using Inventec.Token.Core;
using Inventec.Core;
using MOS.API.Base;
using MOS.SDO;
using System;
using System.Web.Http;
using System.Collections.Generic;
using MOS.MANAGER.CodeGenerator;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class CodeGenerationController : BaseApiController
    {
        [HttpPost]
        [ActionName("InCodeGetNext")]
        public ApiResult InCodeGetNext(ApiParam<string> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.InCodeGetNext(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("InCodeFinishUpdateDB")]
        public ApiResult InCodeFinishUpdateDB(ApiParam<string> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<bool> result = mng.InCodeFinishUpdateDB(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ExtraEndCodeGetNext")]
        public ApiResult ExtraEndCodeGetNext(ApiParam<string> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.ExtraEndCodeGetNext(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("StoreCodeGetNextOption1")]
        public ApiResult StoreCodeGetNextOption1(ApiParam<long> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.StoreCodeGetNextOption1(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("StoreCodeGetNextOption2")]
        public ApiResult StoreCodeGetNextOption2(ApiParam<StoreCodeGenerateSDO> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.StoreCodeGetNextOption2(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("StoreCodeGetNextOption34")]
        public ApiResult StoreCodeGetNextOption34(ApiParam<StoreCodeGenerateSDO> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.StoreCodeGetNextOption34(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("MediRecordStoreCodeGetNextOption1")]
        public ApiResult MediRecordStoreCodeGetNextOption1(ApiParam<long> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.MediRecordStoreCodeGetNextOption1(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("MediRecordStoreCodeGetNextOption2")]
        public ApiResult MediRecordStoreCodeGetNextOption2(ApiParam<MediRecordStoreCodeGenerateSDO> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.MediRecordStoreCodeGetNextOption2(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("MediRecordStoreCodeGetNextOption3")]
        public ApiResult MediRecordStoreCodeGetNextOption3(ApiParam<MediRecordStoreCodeGenerateSDO> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.MediRecordStoreCodeGetNextOption3(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("MediRecordStoreCodeGetNextOption4")]
        public ApiResult MediRecordStoreCodeGetNextOption4(ApiParam<MediRecordStoreCodeGenerateSDO> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.MediRecordStoreCodeGetNextOption4(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("MediRecordStoreCodeGetNextOption5")]
        public ApiResult MediRecordStoreCodeGetNextOption5(ApiParam<MediRecordStoreCodeGenerateSDO> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.MediRecordStoreCodeGetNextOption5(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }


        [HttpPost]
        [ActionName("ExtraEndCodeFinishUpdateDB")]
        public ApiResult ExtraEndCodeFinishUpdateDB(ApiParam<string> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<bool> result = mng.ExtraEndCodeFinishUpdateDB(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("StoreCodeFinishUpdateDB")]
        public ApiResult StoreCodeFinishUpdateDB(ApiParam<List<string>> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<bool> result = mng.StoreCodeFinishUpdateDB(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("MediRecordStoreCodeFinishUpdateDB")]
        public ApiResult MediRecordStoreCodeFinishUpdateDB(ApiParam<List<string>> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<bool> result = mng.MediRecordStoreCodeFinishUpdateDB(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("BarcodeGetNext")]
        public ApiResult BarcodeGetNext(ApiParam<long> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<string> result = mng.BarcodeGetNext(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("BarcodeFinishUpdateDB")]
        public ApiResult InCodeFinishUpdateDB(ApiParam<List<string>> param)
        {
            try
            {
                CodeGenerationManager mng = new CodeGenerationManager();
                ApiResultObject<bool> result = mng.BarcodeFinishUpdateDB(param.ApiData);
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
