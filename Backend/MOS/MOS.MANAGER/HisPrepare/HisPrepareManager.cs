using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisPrepare.Create;
using MOS.MANAGER.HisPrepare.Update;
using MOS.MANAGER.HisPrepare.Approve.One;
using MOS.MANAGER.HisPrepare.Approve.List;
using MOS.MANAGER.HisPrepare.Unapprove;

namespace MOS.MANAGER.HisPrepare
{
    public partial class HisPrepareManager : BusinessBase
    {
        public HisPrepareManager()
            : base()
        {

        }

        public HisPrepareManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_PREPARE>> Get(HisPrepareFilterQuery filter)
        {
            ApiResultObject<List<HIS_PREPARE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PREPARE> resultData = null;
                if (valid)
                {
                    resultData = new HisPrepareGet(param).Get(filter);
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
        public ApiResultObject<HisPrepareResultSDO> Create(HisPrepareSDO data)
        {
            ApiResultObject<HisPrepareResultSDO> result = new ApiResultObject<HisPrepareResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPrepareResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareCreateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HisPrepareResultSDO> Update(HisPrepareSDO data)
        {
            ApiResultObject<HisPrepareResultSDO> result = new ApiResultObject<HisPrepareResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPrepareResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareUpdateSdo(param).Run(data,ref resultData);
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
        public ApiResultObject<HisPrepareResultSDO> Approve(HisPrepareApproveSDO data)
        {
            ApiResultObject<HisPrepareResultSDO> result = new ApiResultObject<HisPrepareResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPrepareResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareApprove(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_PREPARE>> ApproveList(HisPrepareApproveListSDO data)
        {
            ApiResultObject<List<HIS_PREPARE>> result = new ApiResultObject<List<HIS_PREPARE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_PREPARE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareApproveList(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_PREPARE> Unapprove(HisPrepareSDO data)
        {
            ApiResultObject<HIS_PREPARE> result = new ApiResultObject<HIS_PREPARE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PREPARE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareUnapprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_PREPARE> ChangeLock(long id)
        {
            ApiResultObject<HIS_PREPARE> result = new ApiResultObject<HIS_PREPARE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PREPARE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PREPARE> Lock(long id)
        {
            ApiResultObject<HIS_PREPARE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PREPARE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PREPARE> Unlock(long id)
        {
            ApiResultObject<HIS_PREPARE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PREPARE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPrepareTruncate(param).Truncate(id);
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
