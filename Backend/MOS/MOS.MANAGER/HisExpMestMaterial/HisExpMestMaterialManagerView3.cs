using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMaterial
{
    public partial class HisExpMestMaterialManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL_3>> GetView3(HisExpMestMaterialView3FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL_3>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXP_MEST_MATERIAL_3> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMaterialGet(param).GetView3(filter);
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
