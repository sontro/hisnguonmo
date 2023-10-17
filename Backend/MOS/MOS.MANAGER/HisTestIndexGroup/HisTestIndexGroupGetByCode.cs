using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexGroup
{
    partial class HisTestIndexGroupGet : BusinessBase
    {
        internal HIS_TEST_INDEX_GROUP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTestIndexGroupFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TEST_INDEX_GROUP GetByCode(string code, HisTestIndexGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTestIndexGroupDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
