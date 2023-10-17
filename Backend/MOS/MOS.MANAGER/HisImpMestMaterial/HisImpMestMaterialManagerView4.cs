using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestMaterial
{
    public partial class HisImpMestMaterialManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL_4>> GetView4(HisImpMestMaterialView4FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL_4>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST_MATERIAL_4> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMaterialGet(param).GetView4(filter);
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
