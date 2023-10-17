using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackage
{
    public partial class HisPackageManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PACKAGE>> GetView(HisPackageViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PACKAGE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PACKAGE> resultData = null;
                if (valid)
                {
                    resultData = new HisPackageGet(param).GetView(filter);
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
