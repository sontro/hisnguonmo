using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTestIndexRange;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndex
{
    class HisTestIndexTruncate : BusinessBase
    {
        internal HisTestIndexTruncate()
            : base()
        {

        }

        internal HisTestIndexTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_TEST_INDEX data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestIndexCheck checker = new HisTestIndexCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    new HisTestIndexRangeTruncate(param).TruncateByTestIndexId(data.ID);
                    result = DAOWorker.HisTestIndexDAO.Truncate(data);
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
