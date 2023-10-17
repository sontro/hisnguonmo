using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidMaterialType
{
    public partial class HisBidMaterialTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BID_MATERIAL_TYPE>> GetView(HisBidMaterialTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BID_MATERIAL_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BID_MATERIAL_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisBidMaterialTypeGet(param).GetView(filter);
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
