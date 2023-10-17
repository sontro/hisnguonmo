using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCareType
{
    partial class HisCareTypeTruncate : BusinessBase
    {
        internal HisCareTypeTruncate()
            : base()
        {

        }

        internal HisCareTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_CARE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareTypeCheck checker = new HisCareTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw);
                if (valid)
                {
                    result = DAOWorker.HisCareTypeDAO.Truncate(data);
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
