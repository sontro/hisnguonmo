using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEquipmentSetMaty
{
    public partial class HisEquipmentSetMatyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EQUIPMENT_SET_MATY>> GetView(HisEquipmentSetMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EQUIPMENT_SET_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EQUIPMENT_SET_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisEquipmentSetMatyGet(param).GetView(filter);
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
