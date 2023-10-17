using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMedicine
{
    public partial class HisExpMestMedicineManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<L_HIS_EXP_MEST_MEDICINE_1>> GetLView1(HisExpMestMedicineLView1FilterQuery filter)
        {
            ApiResultObject<List<L_HIS_EXP_MEST_MEDICINE_1>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<L_HIS_EXP_MEST_MEDICINE_1> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMedicineGet(param).GetLView1(filter);
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
