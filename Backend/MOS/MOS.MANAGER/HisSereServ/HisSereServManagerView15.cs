using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    public partial class HisSereServManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERE_SERV_15>> GetView15(HisSereServView15FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_15>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_15> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServGet(param).GetView15(filter);
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
