using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisDepartmentTran.Create;
using MOS.MANAGER.HisDepartmentTran.Hospitalize;
using MOS.MANAGER.HisDepartmentTran.Receive;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepartmentTran
{
    public partial class HisDepartmentTranManager : BusinessBase
    {
        public HisDepartmentTranManager()
            : base()
        {

        }

        public HisDepartmentTranManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_DEPARTMENT_TRAN>> Get(HisDepartmentTranFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEPARTMENT_TRAN>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEPARTMENT_TRAN> resultData = null;
                if (valid)
                {
                    resultData = new HisDepartmentTranGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_DEPARTMENT_TRAN>> GetView(HisDepartmentTranViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DEPARTMENT_TRAN>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DEPARTMENT_TRAN> resultData = null;
                if (valid)
                {
                    resultData = new HisDepartmentTranGet(param).GetView(filter);
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
        public ApiResultObject<V_HIS_DEPARTMENT_TRAN> GetFirstByTreatmentId(long treatmentId)
        {
            ApiResultObject<V_HIS_DEPARTMENT_TRAN> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                V_HIS_DEPARTMENT_TRAN resultData = null;
                if (valid)
                {
                    resultData = new HisDepartmentTranGet(param).GetViewFirstByTreatmentId(treatmentId);
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
        public ApiResultObject<V_HIS_DEPARTMENT_TRAN> GetLastByTreatmentId(HisDepartmentTranLastFilter filter)
        {
            ApiResultObject<V_HIS_DEPARTMENT_TRAN> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                V_HIS_DEPARTMENT_TRAN resultData = null;
                if (valid)
                {
                    resultData = new HisDepartmentTranGet(param).GetViewLastByTreatmentId(filter.TREATMENT_ID, filter.BEFORE_LOG_TIME);
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
        public ApiResultObject<HIS_DEPARTMENT_TRAN> Create(HisDepartmentTranSDO data)
        {
            ApiResultObject<HIS_DEPARTMENT_TRAN> result = new ApiResultObject<HIS_DEPARTMENT_TRAN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPARTMENT_TRAN resultData = null;
                if (valid)
                {
                    new HisDepartmentTranCreate(param).Create(data, false, ref resultData);
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
        public ApiResultObject<HIS_DEPARTMENT_TRAN> Receive(HisDepartmentTranReceiveSDO data)
        {
            ApiResultObject<HIS_DEPARTMENT_TRAN> result = new ApiResultObject<HIS_DEPARTMENT_TRAN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPARTMENT_TRAN resultData = null;
                if (valid)
                {
                    new HisDepartmentTranReceive(param).Run(data, ref resultData);
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
        public ApiResultObject<HisDepartmentTranHospitalizeResultSDO> Hospitalize(HisDepartmentTranHospitalizeSDO data)
        {
            ApiResultObject<HisDepartmentTranHospitalizeResultSDO> result = new ApiResultObject<HisDepartmentTranHospitalizeResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisDepartmentTranHospitalizeResultSDO resultData = null;
                if (valid)
                {
                    new HisDepartmentTranHospitalize(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_DEPARTMENT_TRAN> Update(HIS_DEPARTMENT_TRAN data)
        {
            ApiResultObject<HIS_DEPARTMENT_TRAN> result = new ApiResultObject<HIS_DEPARTMENT_TRAN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPARTMENT_TRAN resultData = null;
                if (valid && new HisDepartmentTranUpdate(param).Update(data))
                {
                    resultData = data; ;
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
        public ApiResultObject<HIS_DEPARTMENT_TRAN> ChangeLock(HIS_DEPARTMENT_TRAN data)
        {
            ApiResultObject<HIS_DEPARTMENT_TRAN> result = new ApiResultObject<HIS_DEPARTMENT_TRAN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_DEPARTMENT_TRAN resultData = null;
                if (valid && new HisDepartmentTranLock(param).ChangeLock(data))
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
                    resultData = new HisDepartmentTranTruncate(param).Truncate(id);
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
