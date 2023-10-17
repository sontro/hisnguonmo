using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLicenseClass
{
    public partial class HisLicenseClassManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_LICENSE_CLASS>> GetView(HisLicenseClassViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_LICENSE_CLASS>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_LICENSE_CLASS> resultData = null;
                if (valid)
                {
                    resultData = new HisLicenseClassGet(param).GetView(filter);
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
