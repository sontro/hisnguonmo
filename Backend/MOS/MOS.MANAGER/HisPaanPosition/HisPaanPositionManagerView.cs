using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPaanPosition
{
    public partial class HisPaanPositionManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PAAN_POSITION>> GetView(HisPaanPositionViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PAAN_POSITION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PAAN_POSITION> resultData = null;
                if (valid)
                {
                    resultData = new HisPaanPositionGet(param).GetView(filter);
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
