using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPtttGroupBest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttGroup
{
    partial class HisPtttGroupTruncate : BusinessBase
    {
        internal HisPtttGroupTruncate()
            : base()
        {

        }

        internal HisPtttGroupTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_PTTT_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttGroupCheck checker = new HisPtttGroupCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    if (new HisPtttGroupBestTruncate(param).TruncateByPtttGroupId(data.ID))
                    {
                        result = DAOWorker.HisPtttGroupDAO.Truncate(data);
                    }
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
