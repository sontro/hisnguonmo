using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint
{
    public partial class HisContactPointManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CONTACT_POINT>> GetView(HisContactPointViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CONTACT_POINT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CONTACT_POINT> resultData = null;
                if (valid)
                {
                    resultData = new HisContactPointGet(param).GetView(filter);
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
