using ACS.API.Base;
using ACS.MANAGER.Token;
using ACS.SDO;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token;
using System;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public class TimerController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [ActionName("Sync")]
        public ApiResult Sync()
        {
            try
            {
                TokenManager mng = new TokenManager();
                DateTime dateTime = DateTime.Now;
                TimeZoneInfo timeZone = TimeZoneInfo.Local;// TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                TimerSDO timerSDO = new TimerSDO();
                timerSDO.DateNow = dateTime;
                var dateTimeUnspec = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
                DateTime DateUniversalTime = TimeZoneInfo.ConvertTimeToUtc(dateTimeUnspec, timeZone);
                timerSDO.UniversalTime = Inventec.Common.TypeConvert.Parse.ToInt64(DateUniversalTime.ToString("yyyyMMddHHmmss"));
                timerSDO.TimeZoneId = timeZone.Id;
                timerSDO.LocalTime = Inventec.Common.TypeConvert.Parse.ToInt64(TimeZoneInfo.ConvertTime(DateUniversalTime, timeZone).ToString("yyyyMMddHHmmss"));
                timerSDO.TimeZoneId = timeZone.Id;

                ApiResultObject<TimerSDO> result = new ApiResultObject<TimerSDO>(timerSDO, true);
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
