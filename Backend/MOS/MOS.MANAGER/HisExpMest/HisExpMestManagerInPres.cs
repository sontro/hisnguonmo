using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.InPres.Approve;
using MOS.MANAGER.HisExpMest.InPres.Export;
using MOS.MANAGER.HisExpMest.InPres.Unapprove;
using MOS.MANAGER.HisExpMest.InPres.Unexport;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest
{
    public partial class HisExpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HIS_EXP_MEST> InPresApprove(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestInPresApprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST> InPresExport(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestInPresExport(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST> InPresUnapprove(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestInPresUnapprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST> InPresUnexport(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestInPresUnexport(param).Run(data, ref resultData);
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

    }
}
