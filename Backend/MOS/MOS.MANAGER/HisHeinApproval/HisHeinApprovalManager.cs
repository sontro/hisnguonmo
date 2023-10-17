using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHeinApproval
{
    public partial class HisHeinApprovalManager : BusinessBase
    {
        public HisHeinApprovalManager()
            : base()
        {

        }

        public HisHeinApprovalManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_HEIN_APPROVAL>> Get(HisHeinApprovalFilterQuery filter)
        {
            ApiResultObject<List<HIS_HEIN_APPROVAL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HEIN_APPROVAL> resultData = null;
                if (valid)
                {
                    resultData = new HisHeinApprovalGet(param).Get(filter);
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
        public ApiResultObject<HIS_HEIN_APPROVAL> ChangeLock(HIS_HEIN_APPROVAL data)
        {
            ApiResultObject<HIS_HEIN_APPROVAL> result = new ApiResultObject<HIS_HEIN_APPROVAL>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HEIN_APPROVAL resultData = null;
                if (valid && new HisHeinApprovalLock(param).ChangeLock(data))
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
        public ApiResultObject<V_HIS_HEIN_APPROVAL> Create(HIS_HEIN_APPROVAL data)
        {
            ApiResultObject<V_HIS_HEIN_APPROVAL> result = new ApiResultObject<V_HIS_HEIN_APPROVAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_HEIN_APPROVAL resultData = null;
                if (valid)
                {
                    new HisHeinApprovalCreate(param).Create(data, ref resultData);
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
                    resultData = new HisHeinApprovalTruncate(param).Truncate(id);
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
        public ApiResultObject<List<V_HIS_HEIN_APPROVAL>> GetView(HisHeinApprovalViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_HEIN_APPROVAL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_HEIN_APPROVAL> resultData = null;
                if (valid)
                {
                    resultData = new HisHeinApprovalGet(param).GetView(filter);
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
    }
}
