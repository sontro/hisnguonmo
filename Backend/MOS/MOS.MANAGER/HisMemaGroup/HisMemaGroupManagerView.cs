using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMemaGroup
{
    public partial class HisMemaGroupManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEMA_GROUP>> GetView(HisMemaGroupViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEMA_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEMA_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisMemaGroupGet(param).GetView(filter);
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
