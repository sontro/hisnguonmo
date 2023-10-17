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
        public ApiResultObject<List<V_HIS_MEDI_RECORD_1>> GetView1(HisMediRecordView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_RECORD_1>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_RECORD_1> resultData = null;
                if (valid)
                {
                    resultData = new HisMediRecordGet(param).GetView1(filter);
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
