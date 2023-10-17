using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSuimIndex
{
    class HisSuimIndexTruncate : BusinessBase
    {
        internal HisSuimIndexTruncate()
            : base()
        {

        }

        internal HisSuimIndexTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SUIM_INDEX data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSuimIndexCheck checker = new HisSuimIndexCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisSuimIndexDAO.Truncate(data);
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
