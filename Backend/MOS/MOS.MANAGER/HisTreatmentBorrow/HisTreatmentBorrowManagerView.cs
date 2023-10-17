using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    public partial class HisTreatmentBorrowManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT_BORROW>> GetView(HisTreatmentBorrowViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_BORROW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_BORROW> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentBorrowGet(param).GetView(filter);
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
