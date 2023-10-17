using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public partial class HisTreatmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<L_HIS_TREATMENT>> GetLView(HisTreatmentLViewFilterQuery filter)
        {
            ApiResultObject<List<L_HIS_TREATMENT>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<L_HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    filter.IS_RESTRICTED_KSK = HisKskContractCFG.RESTRICTED_ACCESSING;
                    resultData = new HisTreatmentGet(param).GetLView(filter);
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
        public ApiResultObject<List<L_HIS_TREATMENT_1>> GetLView1(HisTreatmentLView1FilterQuery filter)
        {
            ApiResultObject<List<L_HIS_TREATMENT_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<L_HIS_TREATMENT_1> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentGet(param).GetLView1(filter);
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
        public ApiResultObject<List<L_HIS_TREATMENT_2>> GetLView2(HisTreatmentLView2FilterQuery filter)
        {
            ApiResultObject<List<L_HIS_TREATMENT_2>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<L_HIS_TREATMENT_2> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentGet(param).GetLView2(filter);
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
        public ApiResultObject<List<L_HIS_TREATMENT_3>> GetLView3(HisTreatmentLView3FilterQuery filter)
        {
            ApiResultObject<List<L_HIS_TREATMENT_3>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<L_HIS_TREATMENT_3> resultData = null;
                if (valid)
                {
                    filter.IS_RESTRICTED_KSK = HisKskContractCFG.RESTRICTED_ACCESSING;
                    resultData = new HisTreatmentGet(param).GetLView3(filter);
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
