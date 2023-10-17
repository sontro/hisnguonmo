using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTestIndexGroup
{
    partial class HisTestIndexGroupDelete : BusinessBase
    {
        internal HisTestIndexGroupDelete()
            : base()
        {

        }

        internal HisTestIndexGroupDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TEST_INDEX_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestIndexGroupCheck checker = new HisTestIndexGroupCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TEST_INDEX_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTestIndexGroupDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TEST_INDEX_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTestIndexGroupCheck checker = new HisTestIndexGroupCheck(param);
                List<HIS_TEST_INDEX_GROUP> listRaw = new List<HIS_TEST_INDEX_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTestIndexGroupDAO.DeleteList(listData);
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
