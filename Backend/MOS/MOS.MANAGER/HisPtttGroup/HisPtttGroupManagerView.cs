using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroup
{
    public partial class HisPtttGroupManager : BusinessBase
    {	
		[Logger]
        public ApiResultObject<List<V_HIS_PTTT_GROUP>> GetView(HisPtttGroupViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PTTT_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PTTT_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttGroupGet(param).GetView(filter);
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
