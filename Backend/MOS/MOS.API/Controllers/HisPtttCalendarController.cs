using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPtttCalendar;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
	public partial class HisPtttCalendarController : BaseApiController
	{
		[HttpGet]
		[ApiParamFilter(typeof(ApiParam<HisPtttCalendarFilterQuery>), "param")]
		[ActionName("Get")]
		public ApiResult Get(ApiParam<HisPtttCalendarFilterQuery> param)
		{
			try
			{
				ApiResultObject<List<HIS_PTTT_CALENDAR>> result = new ApiResultObject<List<HIS_PTTT_CALENDAR>>(null);
				if (param != null)
				{
					HisPtttCalendarManager mng = new HisPtttCalendarManager(param.CommonParam);
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
		public ApiResult Create(ApiParam<HisPtttCalendarSDO> param)
		{
			try
			{
				ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
				if (param != null)
				{
					HisPtttCalendarManager mng = new HisPtttCalendarManager(param.CommonParam);
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
		[ActionName("Approve")]
		public ApiResult Approve(ApiParam<HisPtttCalendarSDO> param)
		{
			try
			{
				ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
				if (param != null)
				{
					HisPtttCalendarManager mng = new HisPtttCalendarManager(param.CommonParam);
					result = mng.Approve(param.ApiData);
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
		[ActionName("Unapprove")]
        public ApiResult Unapprove(ApiParam<HisPtttCalendarSDO> param)
		{
			try
			{
				ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
				if (param != null)
				{
					HisPtttCalendarManager mng = new HisPtttCalendarManager(param.CommonParam);
					result = mng.Unapprove(param.ApiData);
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
        [ActionName("UpdateInfo")]
        public ApiResult UpdateInfo(ApiParam<HisPtttCalendarSDO> param)
        {
            try
            {
                ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
                if (param != null)
                {
                    HisPtttCalendarManager mng = new HisPtttCalendarManager(param.CommonParam);
                    result = mng.UpdateInfo(param.ApiData);
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
        public ApiResult Delete(ApiParam<HisPtttCalendarSDO> param)
		{
			try
			{
				ApiResultObject<bool> result = new ApiResultObject<bool>(false);
				if (param != null)
				{
					HisPtttCalendarManager mng = new HisPtttCalendarManager(param.CommonParam);
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
				ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
				if (param != null && param.ApiData != null)
				{
					HisPtttCalendarManager mng = new HisPtttCalendarManager(param.CommonParam);
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
			ApiResultObject<HIS_PTTT_CALENDAR> result = null;
			if (param != null && param.ApiData != null)
			{
				HisPtttCalendarManager mng = new HisPtttCalendarManager(param.CommonParam);
				result = mng.Lock(param.ApiData);
			}
			return new ApiResult(result, this.ActionContext);
		}
	}
}
