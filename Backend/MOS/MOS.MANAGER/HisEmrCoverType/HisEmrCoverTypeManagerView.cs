using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverType
{
    public partial class HisEmrCoverTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EMR_COVER_TYPE>> GetView(HisEmrCoverTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EMR_COVER_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EMR_COVER_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisEmrCoverTypeGet(param).GetView(filter);
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
