using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTestSampleType
{
    partial class HisTestSampleTypeDelete : BusinessBase
    {
        internal HisTestSampleTypeDelete()
            : base()
        {

        }

        internal HisTestSampleTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TEST_SAMPLE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestSampleTypeCheck checker = new HisTestSampleTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TEST_SAMPLE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTestSampleTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TEST_SAMPLE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTestSampleTypeCheck checker = new HisTestSampleTypeCheck(param);
                List<HIS_TEST_SAMPLE_TYPE> listRaw = new List<HIS_TEST_SAMPLE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTestSampleTypeDAO.DeleteList(listData);
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
