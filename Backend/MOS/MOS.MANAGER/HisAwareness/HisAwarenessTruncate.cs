using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAwareness
{
    partial class HisAwarenessTruncate : BusinessBase
    {
        internal HisAwarenessTruncate()
            : base()
        {

        }

        internal HisAwarenessTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_AWARENESS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAwarenessCheck checker = new HisAwarenessCheck(param);
                valid = valid && IsNotNull(data);
                HIS_AWARENESS raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw);
                if (valid)
                {
                    result = DAOWorker.HisAwarenessDAO.Truncate(data);
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
