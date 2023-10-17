using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediReactSum
{
    public partial class HisMediReactSumManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDI_REACT_SUM>> GetView(HisMediReactSumViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_REACT_SUM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_REACT_SUM> resultData = null;
                if (valid)
                {
                    resultData = new HisMediReactSumGet(param).GetView(filter);
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
