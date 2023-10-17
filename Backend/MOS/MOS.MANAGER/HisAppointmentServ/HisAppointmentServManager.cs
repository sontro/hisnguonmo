using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisAppointmentServ
{
    public partial class HisAppointmentServManager : BusinessBase
    {
        public HisAppointmentServManager()
            : base()
        {

        }

        public HisAppointmentServManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_APPOINTMENT_SERV>> Get(HisAppointmentServFilterQuery filter)
        {
            ApiResultObject<List<HIS_APPOINTMENT_SERV>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_APPOINTMENT_SERV> resultData = null;
                if (valid)
                {
                    resultData = new HisAppointmentServGet(param).Get(filter);
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
        public ApiResultObject<List<HIS_APPOINTMENT_SERV>> Create(HisAppointmentServSDO data)
        {
            ApiResultObject<List<HIS_APPOINTMENT_SERV>> result = new ApiResultObject<List<HIS_APPOINTMENT_SERV>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_APPOINTMENT_SERV> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAppointmentServCreateSDO(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_APPOINTMENT_SERV>> Update(HisAppointmentServSDO data)
        {
            ApiResultObject<List<HIS_APPOINTMENT_SERV>> result = new ApiResultObject<List<HIS_APPOINTMENT_SERV>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_APPOINTMENT_SERV> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAppointmentServUpdateSDO(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_APPOINTMENT_SERV> ChangeLock(long id)
        {
            ApiResultObject<HIS_APPOINTMENT_SERV> result = new ApiResultObject<HIS_APPOINTMENT_SERV>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_APPOINTMENT_SERV resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAppointmentServLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_APPOINTMENT_SERV> Lock(long id)
        {
            ApiResultObject<HIS_APPOINTMENT_SERV> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_APPOINTMENT_SERV resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAppointmentServLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_APPOINTMENT_SERV> Unlock(long id)
        {
            ApiResultObject<HIS_APPOINTMENT_SERV> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_APPOINTMENT_SERV resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAppointmentServLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisAppointmentServTruncate(param).Truncate(id);
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
