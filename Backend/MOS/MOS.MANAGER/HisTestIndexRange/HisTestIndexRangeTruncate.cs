using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexRange
{
    class HisTestIndexRangeTruncate : BusinessBase
    {
        internal HisTestIndexRangeTruncate()
            : base()
        {

        }

        internal HisTestIndexRangeTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_TEST_INDEX_RANGE data)
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
                    result = DAOWorker.HisTestIndexRangeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_TEST_INDEX_RANGE> listData)
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
                    result = DAOWorker.HisTestIndexRangeDAO.TruncateList(listData);
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

        internal bool TruncateByTestIndexId(long testIndexId)
        {
            bool result = false;
            try
            {
                List<HIS_TEST_INDEX_RANGE> list = new HisTestIndexRangeGet().GetByTestIndexId(testIndexId);
                if (IsNotNullOrEmpty(list))
                {
                    result = this.TruncateList(list);
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
