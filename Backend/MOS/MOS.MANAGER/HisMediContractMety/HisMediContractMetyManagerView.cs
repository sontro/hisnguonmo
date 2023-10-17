using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMety
{
    public partial class HisMediContractMetyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDI_CONTRACT_METY>> GetView(HisMediContractMetyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_CONTRACT_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_CONTRACT_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediContractMetyGet(param).GetView(filter);
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
