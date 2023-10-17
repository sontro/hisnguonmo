using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRoom
{
    public partial class HisExecuteRoomManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXECUTE_ROOM_1>> GetView1(HisExecuteRoomView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXECUTE_ROOM_1>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXECUTE_ROOM_1> resultData = null;
                if (valid)
                {
                    resultData = new HisExecuteRoomGet(param).GetView1(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}
