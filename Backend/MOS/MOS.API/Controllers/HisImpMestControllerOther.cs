using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisImpMestController : BaseApiController
    {

        [HttpPost]
        [ActionName("InitCreate")]
        public ApiResult InitCreate(ApiParam<HisImpMestInitSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestInitSDO> result = new ApiResultObject<HisImpMestInitSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.InitCreate(param.ApiData);
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
        [ActionName("InveCreate")]
        public ApiResult InveCreate(ApiParam<HisImpMestInveSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestInveSDO> result = new ApiResultObject<HisImpMestInveSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.InveCreate(param.ApiData);
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
        [ActionName("OtherCreate")]
        public ApiResult OtherCreate(ApiParam<HisImpMestOtherSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestOtherSDO> result = new ApiResultObject<HisImpMestOtherSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.OtherCreate(param.ApiData);
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
        [ActionName("InitUpdate")]
        public ApiResult InitUpdate(ApiParam<HisImpMestInitSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestInitSDO> result = new ApiResultObject<HisImpMestInitSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.InitUpdate(param.ApiData);
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
        [ActionName("InveUpdate")]
        public ApiResult InveUpdate(ApiParam<HisImpMestInveSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestInveSDO> result = new ApiResultObject<HisImpMestInveSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.InveUpdate(param.ApiData);
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
        [ActionName("OtherUpdate")]
        public ApiResult OtherUpdate(ApiParam<HisImpMestOtherSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestOtherSDO> result = new ApiResultObject<HisImpMestOtherSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.OtherUpdate(param.ApiData);
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
        [ActionName("DonationCreate")]
        public ApiResult DonationCreate(ApiParam<HisImpMestDonationSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestDonationSDO> result = new ApiResultObject<HisImpMestDonationSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.DonationCreate(param.ApiData);
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
        [ActionName("DonationUpdate")]
        public ApiResult DonationUpdate(ApiParam<HisImpMestDonationSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestDonationSDO> result = new ApiResultObject<HisImpMestDonationSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.DonationUpdate(param.ApiData);
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
