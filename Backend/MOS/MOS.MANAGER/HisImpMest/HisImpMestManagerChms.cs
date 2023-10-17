using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Aggr;
using MOS.MANAGER.HisImpMest.Chms;
using MOS.MANAGER.HisImpMest.Manu;
using MOS.MANAGER.HisImpMest.Moba;
using MOS.MANAGER.HisImpMest.Other;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMest
{
    public partial class HisImpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisImpMestResultSDO> ChmsCreate(HIS_IMP_MEST data)
        {
            ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestResultSDO resultData = null;
                if (valid)
                {
                    new HisImpMestChmsCreate(param).Create(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

    }
}
