using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProgram
{
    public partial class HisProgramManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PROGRAM>> GetView(HisProgramViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PROGRAM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PROGRAM> resultData = null;
                if (valid)
                {
                    resultData = new HisProgramGet(param).GetView(filter);
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
