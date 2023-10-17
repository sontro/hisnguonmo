using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestStt
{
    class HisImpMestSttTruncate : BusinessBase
    {
        internal HisImpMestSttTruncate()
            : base()
        {

        }

        internal HisImpMestSttTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_IMP_MEST_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestSttCheck checker = new HisImpMestSttCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisImpMestSttDAO.Truncate(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
