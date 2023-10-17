using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisMedicalContract.CreateSdo;
using MOS.MANAGER.HisMedicalContract.UpdateSdo;
using MOS.MANAGER.HisMedicalContract.Import;

namespace MOS.MANAGER.HisMedicalContract
{
    public partial class HisMedicalContractManager : BusinessBase
    {
        public HisMedicalContractManager()
            : base()
        {

        }

        public HisMedicalContractManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDICAL_CONTRACT>> Get(HisMedicalContractFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICAL_CONTRACT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICAL_CONTRACT> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicalContractGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDICAL_CONTRACT> Create(HIS_MEDICAL_CONTRACT data)
        {
            ApiResultObject<HIS_MEDICAL_CONTRACT> result = new ApiResultObject<HIS_MEDICAL_CONTRACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICAL_CONTRACT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicalContractCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
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
        public ApiResultObject<HIS_MEDICAL_CONTRACT> Update(HIS_MEDICAL_CONTRACT data)
        {
            ApiResultObject<HIS_MEDICAL_CONTRACT> result = new ApiResultObject<HIS_MEDICAL_CONTRACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICAL_CONTRACT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicalContractUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
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
        public ApiResultObject<HIS_MEDICAL_CONTRACT> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDICAL_CONTRACT> result = new ApiResultObject<HIS_MEDICAL_CONTRACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICAL_CONTRACT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicalContractLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICAL_CONTRACT> Lock(long id)
        {
            ApiResultObject<HIS_MEDICAL_CONTRACT> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICAL_CONTRACT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicalContractLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICAL_CONTRACT> Unlock(long id)
        {
            ApiResultObject<HIS_MEDICAL_CONTRACT> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICAL_CONTRACT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicalContractLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMedicalContractTruncate(param).Truncate(id);
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
        public ApiResultObject<HIS_MEDICAL_CONTRACT> CreateSdo(HisMedicalContractSDO data)
        {
            ApiResultObject<HIS_MEDICAL_CONTRACT> result = new ApiResultObject<HIS_MEDICAL_CONTRACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICAL_CONTRACT resultData = null;
                if (valid)
                {
                    new HisMedicalContractCreateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_MEDICAL_CONTRACT> UpdateSdo(HisMedicalContractSDO data)
        {
            ApiResultObject<HIS_MEDICAL_CONTRACT> result = new ApiResultObject<HIS_MEDICAL_CONTRACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICAL_CONTRACT resultData = null;
                if (valid)
                {
                    new HisMedicalContractUpdateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_MEDICAL_CONTRACT>> Import(List<HIS_MEDICAL_CONTRACT> data)
        {
            ApiResultObject<List<HIS_MEDICAL_CONTRACT>> result = new ApiResultObject<List<HIS_MEDICAL_CONTRACT>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(data);
                List<HIS_MEDICAL_CONTRACT> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicalContractImport(param).Run(data);
                    resultData = isSuccess ? data : null;
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
    }
}
