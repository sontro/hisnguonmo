using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMatyDepa
{
    public partial class HisMestMatyDepaManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEST_MATY_DEPA>> GetView(HisMestMatyDepaViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_MATY_DEPA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_MATY_DEPA> resultData = null;
                if (valid)
                {
                    resultData = new HisMestMatyDepaGet(param).GetView(filter);
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
