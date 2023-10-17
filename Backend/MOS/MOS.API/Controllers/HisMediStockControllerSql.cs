using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMediStockController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<DHisMediStock1Filter>), "param")]
        [ActionName("GetDHisMediStock1")]
        public ApiResult GetDHisMediStock1(ApiParam<DHisMediStock1Filter> param)
        {
            try
            {
                ApiResultObject<List<D_HIS_MEDI_STOCK_1>> result = new ApiResultObject<List<D_HIS_MEDI_STOCK_1>>(null);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
                    result = mng.GetDHisMediStock1(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<DHisMediStock2Filter>), "param")]
        [ActionName("GetDHisMediStock2")]
        public ApiResult GetDHisMediStock1(ApiParam<DHisMediStock2Filter> param)
        {
            try
            {
                ApiResultObject<List<D_HIS_MEDI_STOCK_2>> result = new ApiResultObject<List<D_HIS_MEDI_STOCK_2>>(null);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
                    result = mng.GetDHisMediStock2(param.ApiData);
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
