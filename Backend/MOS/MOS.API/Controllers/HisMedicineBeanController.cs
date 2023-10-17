using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicineBean;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMedicineBeanController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineBeanFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicineBeanFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_BEAN>> result = new ApiResultObject<List<HIS_MEDICINE_BEAN>>(null);
                if (param != null)
                {
                    HisMedicineBeanManager mng = new HisMedicineBeanManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineBeanViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMedicineBeanViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDICINE_BEAN>> result = new ApiResultObject<List<V_HIS_MEDICINE_BEAN>>(null);
                if (param != null)
                {
                    HisMedicineBeanManager mng = new HisMedicineBeanManager(param.CommonParam);
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
        [ActionName("Take")]
        public ApiResult Take(ApiParam<TakeBeanSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_BEAN>> result = new ApiResultObject<List<HIS_MEDICINE_BEAN>>(null);
                if (param != null)
                {
                    HisMedicineBeanManager mng = new HisMedicineBeanManager(param.CommonParam);
                    result = mng.Take(param.ApiData);
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
        [ActionName("TakeList")]
        public ApiResult TakeList(ApiParam<List<TakeBeanSDO>> param)
        {
            try
            {
                ApiResultObject<List<TakeMedicineBeanListResultSDO>> result = new ApiResultObject<List<TakeMedicineBeanListResultSDO>>(null);
                if (param != null)
                {
                    HisMedicineBeanManager mng = new HisMedicineBeanManager(param.CommonParam);
                    result = mng.Take(param.ApiData);
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
        [ActionName("TakeByMedicine")]
        public ApiResult Take(ApiParam<TakeBeanByMameSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_BEAN>> result = new ApiResultObject<List<HIS_MEDICINE_BEAN>>(null);
                if (param != null)
                {
                    HisMedicineBeanManager mng = new HisMedicineBeanManager(param.CommonParam);
                    result = mng.TakeByMedicine(param.ApiData);
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
        [ActionName("TakeListByMedicine")]
        public ApiResult TakeListByMedicine(ApiParam<List<TakeBeanByMameSDO>> param)
        {
            try
            {
                ApiResultObject<List<TakeMedicineBeanByMedicineListResultSDO>> result = new ApiResultObject<List<TakeMedicineBeanByMedicineListResultSDO>>(null);
                if (param != null)
                {
                    HisMedicineBeanManager mng = new HisMedicineBeanManager(param.CommonParam);
                    result = mng.TakeByMedicine(param.ApiData);
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
        [ActionName("Release")]
        public ApiResult ReleaseBean(ApiParam<ReleaseBeanSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMedicineBeanManager mng = new HisMedicineBeanManager(param.CommonParam);
                    result = mng.Release(param.ApiData);
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
        [ActionName("ReleaseAll")]
        public ApiResult ReleaseBeanAll(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMedicineBeanManager mng = new HisMedicineBeanManager(param.CommonParam);
                    result = mng.Release(param.ApiData);
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
