using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexRange
{
    class HisTestIndexRangeDelete : BusinessBase
    {
        internal HisTestIndexRangeDelete()
            : base()
        {

        }

        internal HisTestIndexRangeDelete(Inventec.Core.CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TEST_INDEX_RANGE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestIndexRangeCheck checker = new HisTestIndexRangeCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTestIndexRangeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TEST_INDEX_RANGE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTestIndexRangeCheck checker = new HisTestIndexRangeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTestIndexRangeDAO.DeleteList(listData);
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
