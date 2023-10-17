using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterial
{
    public partial class HisMaterialManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MATERIAL_1>> GetView1(HisMaterialView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MATERIAL_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MATERIAL_1> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialGet(param).GetView1(filter);
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
