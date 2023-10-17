using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServSegr;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceGroup
{
    class HisServiceGroupTruncate : BusinessBase
    {
        internal HisServiceGroupTruncate()
            : base()
        {

        }

        internal HisServiceGroupTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERVICE_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceGroupCheck checker = new HisServiceGroupCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisServiceGroupDAO.Truncate(data);
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
