using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMilitaryRank
{
    partial class HisMilitaryRankTruncate : BusinessBase
    {
        internal HisMilitaryRankTruncate()
            : base()
        {

        }

        internal HisMilitaryRankTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MILITARY_RANK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMilitaryRankCheck checker = new HisMilitaryRankCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MILITARY_RANK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw);
                if (valid)
                {
                    result = DAOWorker.HisMilitaryRankDAO.Truncate(data);
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
