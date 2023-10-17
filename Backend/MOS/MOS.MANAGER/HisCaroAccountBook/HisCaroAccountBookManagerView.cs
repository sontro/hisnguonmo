using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroAccountBook
{
    public partial class HisCaroAccountBookManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CARO_ACCOUNT_BOOK>> GetView(HisCaroAccountBookViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CARO_ACCOUNT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CARO_ACCOUNT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisCaroAccountBookGet(param).GetView(filter);
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
