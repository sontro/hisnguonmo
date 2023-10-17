using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyUnit
{
    public partial class HisMestMetyUnitManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEST_METY_UNIT>> GetView(HisMestMetyUnitViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_METY_UNIT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_METY_UNIT> resultData = null;
                if (valid)
                {
                    resultData = new HisMestMetyUnitGet(param).GetView(filter);
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
