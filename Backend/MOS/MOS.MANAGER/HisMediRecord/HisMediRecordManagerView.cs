using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecord
{
    public partial class HisMediRecordManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDI_RECORD>> GetView(HisMediRecordViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_RECORD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_RECORD> resultData = null;
                if (valid)
                {
                    resultData = new HisMediRecordGet(param).GetView(filter);
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
