using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContact
{
    public partial class HisContactManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CONTACT>> GetView(HisContactViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CONTACT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CONTACT> resultData = null;
                if (valid)
                {
                    resultData = new HisContactGet(param).GetView(filter);
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
