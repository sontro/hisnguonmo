using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexUnit
{
    class HisTestIndexUnitUpdate : BusinessBase
    {
        internal HisTestIndexUnitUpdate()
            : base()
        {

        }

        internal HisTestIndexUnitUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TEST_INDEX_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestIndexUnitCheck checker = new HisTestIndexUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data);
                valid = valid && checker.ExistsCode(data.TEST_INDEX_UNIT_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTestIndexUnitDAO.Update(data);
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

        internal bool UpdateList(List<HIS_TEST_INDEX_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTestIndexUnitCheck checker = new HisTestIndexUnitCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.ExistsCode(data.TEST_INDEX_UNIT_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTestIndexUnitDAO.UpdateList(listData);
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
