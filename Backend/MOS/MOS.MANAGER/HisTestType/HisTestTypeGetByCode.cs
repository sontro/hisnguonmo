using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestType
{
    partial class HisTestTypeGet : BusinessBase
    {
        internal HIS_TEST_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTestTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TEST_TYPE GetByCode(string code, HisTestTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTestTypeDAO.GetByCode(code, filter.Query());
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
