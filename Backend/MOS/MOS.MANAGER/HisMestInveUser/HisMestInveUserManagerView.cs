using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestInveUser
{
    public partial class HisMestInveUserManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEST_INVE_USER>> GetView(HisMestInveUserViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_INVE_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_INVE_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisMestInveUserGet(param).GetView(filter);
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
