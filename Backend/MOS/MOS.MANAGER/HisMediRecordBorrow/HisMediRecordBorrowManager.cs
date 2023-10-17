using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecordBorrow
{
    public partial class HisMediRecordBorrowManager : BusinessBase
    {
        public HisMediRecordBorrowManager()
            : base()
        {

        }

        public HisMediRecordBorrowManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDI_RECORD_BORROW>> Get(HisMediRecordBorrowFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_RECORD_BORROW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_RECORD_BORROW> resultData = null;
                if (valid)
                {
                    resultData = new HisMediRecordBorrowGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDI_RECORD_BORROW> Create(HIS_MEDI_RECORD_BORROW data)
        {
            ApiResultObject<HIS_MEDI_RECORD_BORROW> result = new ApiResultObject<HIS_MEDI_RECORD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_RECORD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordBorrowCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDI_RECORD_BORROW> Update(HIS_MEDI_RECORD_BORROW data)
        {
            ApiResultObject<HIS_MEDI_RECORD_BORROW> result = new ApiResultObject<HIS_MEDI_RECORD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_RECORD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordBorrowUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDI_RECORD_BORROW> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDI_RECORD_BORROW> result = new ApiResultObject<HIS_MEDI_RECORD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_RECORD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordBorrowLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_RECORD_BORROW> Lock(long id)
        {
            ApiResultObject<HIS_MEDI_RECORD_BORROW> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_RECORD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordBorrowLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_RECORD_BORROW> Unlock(long id)
        {
            ApiResultObject<HIS_MEDI_RECORD_BORROW> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_RECORD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordBorrowLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMediRecordBorrowTruncate(param).Truncate(id);
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
        public ApiResultObject<HIS_MEDI_RECORD_BORROW> Receive(HIS_MEDI_RECORD_BORROW data)
        {
            ApiResultObject<HIS_MEDI_RECORD_BORROW> result = new ApiResultObject<HIS_MEDI_RECORD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_RECORD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordBorrowReceive(param).Run(data, ref resultData);
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
