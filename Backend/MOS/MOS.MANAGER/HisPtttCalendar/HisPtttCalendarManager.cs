using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisPtttCalendar
{
    public partial class HisPtttCalendarManager : BusinessBase
    {
        public HisPtttCalendarManager()
            : base()
        {

        }

        public HisPtttCalendarManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_PTTT_CALENDAR>> Get(HisPtttCalendarFilterQuery filter)
        {
            ApiResultObject<List<HIS_PTTT_CALENDAR>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PTTT_CALENDAR> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttCalendarGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_PTTT_CALENDAR> Create(HisPtttCalendarSDO data)
        {
            ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CALENDAR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttCalendarCreate(param).Create(data, ref resultData);
                    resultData = isSuccess ? resultData : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_PTTT_CALENDAR> UpdateInfo(HisPtttCalendarSDO data)
        {
            ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CALENDAR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttCalendarUpdateInfo(param).Run(data, ref resultData);
                    resultData = isSuccess ? resultData : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_PTTT_CALENDAR> Approve(HisPtttCalendarSDO data)
        {
            ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CALENDAR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttCalendarApprove(param).Run(data, ref resultData);
                    resultData = isSuccess ? resultData : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_PTTT_CALENDAR> Unapprove(HisPtttCalendarSDO data)
        {
            ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CALENDAR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttCalendarUnapprove(param).Run(data, ref resultData);
                    resultData = isSuccess ? resultData : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_PTTT_CALENDAR> ChangeLock(long id)
        {
            ApiResultObject<HIS_PTTT_CALENDAR> result = new ApiResultObject<HIS_PTTT_CALENDAR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_CALENDAR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttCalendarLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_PTTT_CALENDAR> Lock(long id)
        {
            ApiResultObject<HIS_PTTT_CALENDAR> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_CALENDAR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttCalendarLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_PTTT_CALENDAR> Unlock(long id)
        {
            ApiResultObject<HIS_PTTT_CALENDAR> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_CALENDAR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttCalendarLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HisPtttCalendarSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisPtttCalendarTruncateSdo(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
