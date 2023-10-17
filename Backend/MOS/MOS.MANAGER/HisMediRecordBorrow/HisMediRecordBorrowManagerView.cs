using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecordBorrow
{
    public partial class HisMediRecordBorrowManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDI_RECORD_BORROW>> GetView(HisMediRecordBorrowViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_RECORD_BORROW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_RECORD_BORROW> resultData = null;
                if (valid)
                {
                    resultData = new HisMediRecordBorrowGet(param).GetView(filter);
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
