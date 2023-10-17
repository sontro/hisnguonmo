using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMaty
{
    public partial class HisMediContractMatyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDI_CONTRACT_MATY>> GetView(HisMediContractMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_CONTRACT_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_CONTRACT_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediContractMatyGet(param).GetView(filter);
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
