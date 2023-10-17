using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentEndTypeExt
{
    public partial class HisTreatmentEndTypeExtManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT_END_TYPE_EXT>> GetView(HisTreatmentEndTypeExtViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_END_TYPE_EXT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_END_TYPE_EXT> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentEndTypeExtGet(param).GetView(filter);
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
