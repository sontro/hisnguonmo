using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackageDetail
{
    public partial class HisPackageDetailManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PACKAGE_DETAIL>> GetView(HisPackageDetailViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PACKAGE_DETAIL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PACKAGE_DETAIL> resultData = null;
                if (valid)
                {
                    resultData = new HisPackageDetailGet(param).GetView(filter);
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
