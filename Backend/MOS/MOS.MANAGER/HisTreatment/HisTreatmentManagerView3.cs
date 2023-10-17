using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public partial class HisTreatmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT_3>> GetView3(HisTreatmentView3FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_3>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_3> resultData = null;
                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (HisBranchCFG.IS_ISOLATION && branch != null)
                    {
                        filter.BRANCH_ID = branch.ID;
                    }

                    resultData = new HisTreatmentGet(param).GetView3(filter);
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
