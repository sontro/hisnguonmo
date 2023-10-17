using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestMetyDepa;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMestMetyDepaController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestMetyDepaFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMestMetyDepaFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_METY_DEPA>> result = new ApiResultObject<List<HIS_MEST_METY_DEPA>>(null);
                if (param != null)
                {
                    HisMestMetyDepaManager mng = new HisMestMetyDepaManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEST_METY_DEPA> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_METY_DEPA> result = new ApiResultObject<HIS_MEST_METY_DEPA>(null);
                if (param != null)
                {
                    HisMestMetyDepaManager mng = new HisMestMetyDepaManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEST_METY_DEPA> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_METY_DEPA> result = new ApiResultObject<HIS_MEST_METY_DEPA>(null);
                if (param != null)
                {
                    HisMestMetyDepaManager mng = new HisMestMetyDepaManager(param.CommonParam);
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
                    HisMestMetyDepaManager mng = new HisMestMetyDepaManager(param.CommonParam);
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
                ApiResultObject<HIS_MEST_METY_DEPA> result = new ApiResultObject<HIS_MEST_METY_DEPA>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMestMetyDepaManager mng = new HisMestMetyDepaManager(param.CommonParam);
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
            ApiResultObject<HIS_MEST_METY_DEPA> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMestMetyDepaManager mng = new HisMestMetyDepaManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }


        [HttpPost]
        [ActionName("CreateByMedicine")]
        public ApiResult CreateByMedicine(ApiParam<HisMestMetyDepaByMedicineSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    HisMestMetyDepaManager mng = new HisMestMetyDepaManager(param.CommonParam);
                    result = mng.CreateByMedicine(param.ApiData);
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
