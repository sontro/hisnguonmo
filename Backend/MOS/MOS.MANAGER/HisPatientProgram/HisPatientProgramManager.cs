using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientProgram
{
    public partial class HisPatientProgramManager : BusinessBase
    {
        public HisPatientProgramManager()
            : base()
        {

        }
        
        public HisPatientProgramManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PATIENT_PROGRAM>> Get(HisPatientProgramFilterQuery filter)
        {
            ApiResultObject<List<HIS_PATIENT_PROGRAM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PATIENT_PROGRAM> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientProgramGet(param).Get(filter);
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
        public ApiResultObject<HIS_PATIENT_PROGRAM> Create(HIS_PATIENT_PROGRAM data)
        {
            ApiResultObject<HIS_PATIENT_PROGRAM> result = new ApiResultObject<HIS_PATIENT_PROGRAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_PROGRAM resultData = null;
                if (valid && new HisPatientProgramCreate(param).Create(data))
                {
                    resultData = data;
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

		[Logger]
        public ApiResultObject<HIS_PATIENT_PROGRAM> Update(HIS_PATIENT_PROGRAM data)
        {
            ApiResultObject<HIS_PATIENT_PROGRAM> result = new ApiResultObject<HIS_PATIENT_PROGRAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_PROGRAM resultData = null;
                if (valid && new HisPatientProgramUpdate(param).Update(data))
                {
                    resultData = data;
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

		[Logger]
        public ApiResultObject<HIS_PATIENT_PROGRAM> ChangeLock(HIS_PATIENT_PROGRAM data)
        {
            ApiResultObject<HIS_PATIENT_PROGRAM> result = new ApiResultObject<HIS_PATIENT_PROGRAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_PROGRAM resultData = null;
                if (valid && new HisPatientProgramLock(param).ChangeLock(data))
                {
                    resultData = data;
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
                    resultData = new HisPatientProgramTruncate(param).Truncate(id);
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
