using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexRange
{
    class HisTestIndexRangeCreate : BusinessBase
    {
        internal HisTestIndexRangeCreate()
            : base()
        {

        }

        internal HisTestIndexRangeCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TEST_INDEX_RANGE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestIndexRangeCheck checker = new HisTestIndexRangeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisTestIndexRangeDAO.Create(data);
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

        internal bool CreateList(List<HIS_TEST_INDEX_RANGE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTestIndexRangeCheck checker = new HisTestIndexRangeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisTestIndexRangeDAO.CreateList(listData);
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
