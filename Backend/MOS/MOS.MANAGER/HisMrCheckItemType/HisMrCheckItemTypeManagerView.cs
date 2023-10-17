using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItemType
{
    public partial class HisMrCheckItemTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MR_CHECK_ITEM_TYPE>> GetView(HisMrCheckItemTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MR_CHECK_ITEM_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MR_CHECK_ITEM_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisMrCheckItemTypeGet(param).GetView(filter);
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
