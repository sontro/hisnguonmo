using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMaty
{
    public partial class HisServiceReqMatyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERVICE_REQ_MATY>> GetView(HisServiceReqMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_REQ_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_REQ_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqMatyGet(param).GetView(filter);
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
