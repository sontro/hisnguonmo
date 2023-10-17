using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExroRoom
{
    public partial class HisExroRoomManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXRO_ROOM>> GetView(HisExroRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXRO_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXRO_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisExroRoomGet(param).GetView(filter);
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
