using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHeinServiceType
{
    class HisHeinServiceTypeTruncate : BusinessBase
    {
        internal HisHeinServiceTypeTruncate()
            : base()
        {

        }

        internal HisHeinServiceTypeTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_HEIN_SERVICE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHeinServiceTypeCheck checker = new HisHeinServiceTypeCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisHeinServiceTypeDAO.Truncate(data);
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
