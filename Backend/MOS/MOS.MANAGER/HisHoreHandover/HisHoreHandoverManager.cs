using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisHoreHandover.Create;
using MOS.MANAGER.HisHoreHandover.Update;
using MOS.MANAGER.HisHoreHandover.Delete;
using MOS.MANAGER.HisHoreHandover.Receive;
using MOS.MANAGER.HisHoreHandover.Unreceive;

namespace MOS.MANAGER.HisHoreHandover
{
    public partial class HisHoreHandoverManager : BusinessBase
    {
        public HisHoreHandoverManager()
            : base()
        {

        }

        public HisHoreHandoverManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_HORE_HANDOVER>> Get(HisHoreHandoverFilterQuery filter)
        {
            ApiResultObject<List<HIS_HORE_HANDOVER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HORE_HANDOVER> resultData = null;
                if (valid)
                {
                    resultData = new HisHoreHandoverGet(param).Get(filter);
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
        public ApiResultObject<HisHoreHandoverResultSDO> CreateSdo(HisHoreHandoverCreateSDO data)
        {
            ApiResultObject<HisHoreHandoverResultSDO> result = new ApiResultObject<HisHoreHandoverResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisHoreHandoverResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverCreateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HisHoreHandoverResultSDO> UpdateSdo(HisHoreHandoverCreateSDO data)
        {
            ApiResultObject<HisHoreHandoverResultSDO> result = new ApiResultObject<HisHoreHandoverResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisHoreHandoverResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverUpdateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_HORE_HANDOVER> ChangeLock(long id)
        {
            ApiResultObject<HIS_HORE_HANDOVER> result = new ApiResultObject<HIS_HORE_HANDOVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_HANDOVER resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_HORE_HANDOVER> Lock(long id)
        {
            ApiResultObject<HIS_HORE_HANDOVER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_HANDOVER resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_HORE_HANDOVER> Unlock(long id)
        {
            ApiResultObject<HIS_HORE_HANDOVER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_HANDOVER resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<bool> Delete(HisHoreHandoverSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisHoreHandoverTruncateSdo(param).Run(data);
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
        public ApiResultObject<HisHoreHandoverResultSDO> Receive(HisHoreHandoverSDO data)
        {
            ApiResultObject<HisHoreHandoverResultSDO> result = new ApiResultObject<HisHoreHandoverResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisHoreHandoverResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverReceive(param).Run(data, ref resultData);
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
        public ApiResultObject<HisHoreHandoverResultSDO> Unreceive(HisHoreHandoverSDO data)
        {
            ApiResultObject<HisHoreHandoverResultSDO> result = new ApiResultObject<HisHoreHandoverResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisHoreHandoverResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverUnreceive(param).Run(data, ref resultData);
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
