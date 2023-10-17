using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodRh
{
    partial class HisBloodRhTruncate : BusinessBase
    {
        internal HisBloodRhTruncate()
            : base()
        {

        }

        internal HisBloodRhTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_BLOOD_RH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodRhCheck checker = new HisBloodRhCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_RH raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw);
                if (valid)
                {
                    result = DAOWorker.HisBloodRhDAO.Truncate(data);
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
