using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    public partial class HisTreatmentBorrowManager : BusinessBase
    {
        public HisTreatmentBorrowManager()
            : base()
        {

        }

        public HisTreatmentBorrowManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT_BORROW>> Get(HisTreatmentBorrowFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_BORROW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_BORROW> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentBorrowGet(param).Get(filter);
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
        public ApiResultObject<HIS_TREATMENT_BORROW> Create(HIS_TREATMENT_BORROW data)
        {
            ApiResultObject<HIS_TREATMENT_BORROW> result = new ApiResultObject<HIS_TREATMENT_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentBorrowCreate(param).Create(data);
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
        public ApiResultObject<HIS_TREATMENT_BORROW> Update(HIS_TREATMENT_BORROW data)
        {
            ApiResultObject<HIS_TREATMENT_BORROW> result = new ApiResultObject<HIS_TREATMENT_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentBorrowUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TREATMENT_BORROW> Receive(HIS_TREATMENT_BORROW data)
        {
            ApiResultObject<HIS_TREATMENT_BORROW> result = new ApiResultObject<HIS_TREATMENT_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentBorrowReceive(param).Run(data);
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
        public ApiResultObject<HIS_TREATMENT_BORROW> ChangeLock(long id)
        {
            ApiResultObject<HIS_TREATMENT_BORROW> result = new ApiResultObject<HIS_TREATMENT_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentBorrowLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_BORROW> Lock(long id)
        {
            ApiResultObject<HIS_TREATMENT_BORROW> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentBorrowLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_BORROW> Unlock(long id)
        {
            ApiResultObject<HIS_TREATMENT_BORROW> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentBorrowLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTreatmentBorrowTruncate(param).Truncate(id);
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
