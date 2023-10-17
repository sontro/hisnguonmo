using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexUnit
{
    class HisTestIndexUnitCreate : BusinessBase
    {
        internal HisTestIndexUnitCreate()
            : base()
        {

        }

        internal HisTestIndexUnitCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TEST_INDEX_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestIndexUnitCheck checker = new HisTestIndexUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TEST_INDEX_UNIT_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisTestIndexUnitDAO.Create(data);
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

        internal bool CreateList(List<HIS_TEST_INDEX_UNIT> listData)
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
                    valid = valid && checker.ExistsCode(data.TEST_INDEX_UNIT_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisTestIndexUnitDAO.CreateList(listData);
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
