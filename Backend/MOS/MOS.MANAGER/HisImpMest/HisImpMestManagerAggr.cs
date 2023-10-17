using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Aggr;
using MOS.MANAGER.HisImpMest.Aggr.Approve;
using MOS.MANAGER.HisImpMest.Aggr.Unapprove;
using MOS.MANAGER.HisImpMest.Aggr.Unimport;
using MOS.MANAGER.HisImpMest.Chms;
using MOS.MANAGER.HisImpMest.Manu;
using MOS.MANAGER.HisImpMest.Moba;
using MOS.MANAGER.HisImpMest.Other;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMest
{
    public partial class HisImpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_IMP_MEST>> AggrCreate(HisImpMestAggrSDO data)
        {
            ApiResultObject<List<V_HIS_IMP_MEST>> result = new ApiResultObject<List<V_HIS_IMP_MEST>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<V_HIS_IMP_MEST> resultData = null;
                if (valid)
                {
                    new HisImpMestAggrCreate(param).AggrCreate(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<ImpMestAggrApprovalResultSDO> AggrApprove(ImpMestAggrApprovalSDO data)
        {
            ApiResultObject<ImpMestAggrApprovalResultSDO> result = new ApiResultObject<ImpMestAggrApprovalResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                ImpMestAggrApprovalResultSDO resultData = null;
                if (valid)
                {
                    new HisImpMestAggrApprove(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_IMP_MEST> AggrUnapprove(ImpMestAggrUnapprovalSDO data)
        {
            ApiResultObject<HIS_IMP_MEST> result = new ApiResultObject<HIS_IMP_MEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST resultData = null;
                if (valid)
                {
                    new HisImpMestAggrUnapprove(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> RemoveAggr(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisImpMestRemoveAggr(param).RemoveAggr(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_IMP_MEST> AggrUnimport(HIS_IMP_MEST data)
        {
            ApiResultObject<HIS_IMP_MEST> result = new ApiResultObject<HIS_IMP_MEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST resultData = null;
                if (valid)
                {
                    new HisImpMestAggrUnimport(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

    }
}
