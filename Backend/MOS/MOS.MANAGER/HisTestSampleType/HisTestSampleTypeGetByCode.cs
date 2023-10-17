using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestSampleType
{
    partial class HisTestSampleTypeGet : BusinessBase
    {
        internal HIS_TEST_SAMPLE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTestSampleTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TEST_SAMPLE_TYPE GetByCode(string code, HisTestSampleTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTestSampleTypeDAO.GetByCode(code, filter.Query());
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
