using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskContract
{
    public partial class HisKskContractManager : BusinessBase
    {
		[Logger]
        public ApiResultObject<List<V_HIS_KSK_CONTRACT>> GetView(HisKskContractViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_KSK_CONTRACT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_KSK_CONTRACT> resultData = null;
                if (valid)
                {
                    resultData = new HisKskContractGet(param).GetView(filter);
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
