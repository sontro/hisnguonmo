using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndex
{
    class HisTestIndexCreate : BusinessBase
    {
        internal HisTestIndexCreate()
            : base()
        {

        }

        internal HisTestIndexCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TEST_INDEX data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestIndexCheck checker = new HisTestIndexCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TEST_INDEX_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisTestIndexDAO.Create(data);
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

        internal bool CreateList(List<HIS_TEST_INDEX> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTestIndexCheck checker = new HisTestIndexCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TEST_INDEX_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisTestIndexDAO.CreateList(listData);
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
