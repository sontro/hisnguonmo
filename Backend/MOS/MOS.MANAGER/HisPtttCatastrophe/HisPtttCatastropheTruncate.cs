using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttCatastrophe
{
    partial class HisPtttCatastropheTruncate : BusinessBase
    {
        internal HisPtttCatastropheTruncate()
            : base()
        {

        }

        internal HisPtttCatastropheTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_PTTT_CATASTROPHE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttCatastropheCheck checker = new HisPtttCatastropheCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CATASTROPHE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisPtttCatastropheDAO.Truncate(data);
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
