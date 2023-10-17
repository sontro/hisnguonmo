using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMety
{
    public partial class HisPrepareMetyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PREPARE_METY>> GetView(HisPrepareMetyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PREPARE_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PREPARE_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisPrepareMetyGet(param).GetView(filter);
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
