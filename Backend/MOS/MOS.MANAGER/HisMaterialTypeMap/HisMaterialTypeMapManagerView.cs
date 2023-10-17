using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialTypeMap
{
    public partial class HisMaterialTypeMapManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MATERIAL_TYPE_MAP>> GetView(HisMaterialTypeMapViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MATERIAL_TYPE_MAP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MATERIAL_TYPE_MAP> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeMapGet(param).GetView(filter);
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
