using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestType
{
    class HisImpMestTypeTruncate : BusinessBase
    {
        internal HisImpMestTypeTruncate()
            : base()
        {

        }

        internal HisImpMestTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_IMP_MEST_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestTypeCheck checker = new HisImpMestTypeCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisImpMestTypeDAO.Truncate(data);
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
