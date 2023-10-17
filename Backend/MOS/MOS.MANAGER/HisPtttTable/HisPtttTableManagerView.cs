using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttTable
{
    public partial class HisPtttTableManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PTTT_TABLE>> GetView(HisPtttTableViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PTTT_TABLE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PTTT_TABLE> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttTableGet(param).GetView(filter);
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
