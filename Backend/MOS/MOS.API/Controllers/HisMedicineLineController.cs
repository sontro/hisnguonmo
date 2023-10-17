using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicineLine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMedicineLineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineLineFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicineLineFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_LINE>> result = new ApiResultObject<List<HIS_MEDICINE_LINE>>(null);
                if (param != null)
                {
                    HisMedicineLineManager mng = new HisMedicineLineManager(param.CommonParam);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_MEDICINE_LINE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_LINE> result = new ApiResultObject<HIS_MEDICINE_LINE>(null);
                if (param != null)
                {
                    HisMedicineLineManager mng = new HisMedicineLineManager(param.CommonParam);
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
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<HIS_MEDICINE_LINE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_LINE> result = new ApiResultObject<HIS_MEDICINE_LINE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMedicineLineManager mng = new HisMedicineLineManager(param.CommonParam);
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
