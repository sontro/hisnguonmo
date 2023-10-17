using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMachineServMaty
{
    public partial class HisMachineServMatyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MACHINE_SERV_MATY>> GetView(HisMachineServMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MACHINE_SERV_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MACHINE_SERV_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMachineServMatyGet(param).GetView(filter);
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
