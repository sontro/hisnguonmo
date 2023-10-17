using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserAccountBook
{
    public partial class HisUserAccountBookManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_USER_ACCOUNT_BOOK>> GetView(HisUserAccountBookViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_USER_ACCOUNT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_USER_ACCOUNT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisUserAccountBookGet(param).GetView(filter);
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
