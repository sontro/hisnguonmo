using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialMaterial
{
    public partial class HisMaterialMaterialManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MATERIAL_MATERIAL>> GetView(HisMaterialMaterialViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MATERIAL_MATERIAL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MATERIAL_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialMaterialGet(param).GetView(filter);
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
