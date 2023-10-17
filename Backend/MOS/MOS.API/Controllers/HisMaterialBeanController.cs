using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMaterialBean;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMaterialBeanController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMaterialBeanFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMaterialBeanFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MATERIAL_BEAN>> result = new ApiResultObject<List<HIS_MATERIAL_BEAN>>(null);
                if (param != null)
                {
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialBeanViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMaterialBeanViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MATERIAL_BEAN>> result = new ApiResultObject<List<V_HIS_MATERIAL_BEAN>>(null);
                if (param != null)
                {
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMaterialBeanView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisMaterialBeanView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MATERIAL_BEAN_1>> result = new ApiResultObject<List<V_HIS_MATERIAL_BEAN_1>>(null);
                if (param != null)
                {
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
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
                ApiResultObject<List<HIS_MATERIAL_BEAN>> result = new ApiResultObject<List<HIS_MATERIAL_BEAN>>(null);
                if (param != null)
                {
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
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
        [ActionName("TakeBySerialAndPatientType")]
        public ApiResult TakeBySerialAndPatientType(ApiParam<TakeBeanBySerialSDO> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_BEAN> result = new ApiResultObject<HIS_MATERIAL_BEAN>(null);
                if (param != null)
                {
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
                    result = mng.TakeBySerialAndPatientType(param.ApiData);
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
                ApiResultObject<List<TakeMaterialBeanListResultSDO>> result = new ApiResultObject<List<TakeMaterialBeanListResultSDO>>(null);
                if (param != null)
                {
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
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
        [ActionName("TakeByMaterial")]
        public ApiResult Take(ApiParam<TakeBeanByMameSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MATERIAL_BEAN>> result = new ApiResultObject<List<HIS_MATERIAL_BEAN>>(null);
                if (param != null)
                {
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
                    result = mng.TakeByMaterial(param.ApiData);
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
        [ActionName("TakeListByMaterial")]
        public ApiResult TakeListByMaterial(ApiParam<List<TakeBeanByMameSDO>> param)
        {
            try
            {
                ApiResultObject<List<TakeMaterialBeanByMaterialListResultSDO>> result = new ApiResultObject<List<TakeMaterialBeanByMaterialListResultSDO>>(null);
                if (param != null)
                {
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
                    result = mng.TakeByMaterial(param.ApiData);
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
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
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
                    HisMaterialBeanManager mng = new HisMaterialBeanManager(param.CommonParam);
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
