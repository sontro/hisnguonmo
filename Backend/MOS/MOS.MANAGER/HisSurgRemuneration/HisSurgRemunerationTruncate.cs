using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisSurgRemuDetail;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSurgRemuneration
{
    partial class HisSurgRemunerationTruncate : BusinessBase
    {
        internal HisSurgRemunerationTruncate()
            : base()
        {

        }

        internal HisSurgRemunerationTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSurgRemunerationCheck checker = new HisSurgRemunerationCheck(param);
                HIS_SURG_REMUNERATION raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = new HisSurgRemuDetailTruncate().TruncateBySurgRemunerationId(id)
                        && DAOWorker.HisSurgRemunerationDAO.Truncate(raw);
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
