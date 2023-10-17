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
        internal HisTestSampleTypeGet()
            : base()
        {

        }

        internal HisTestSampleTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TEST_SAMPLE_TYPE> Get(HisTestSampleTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTestSampleTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TEST_SAMPLE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisTestSampleTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TEST_SAMPLE_TYPE GetById(long id, HisTestSampleTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTestSampleTypeDAO.GetById(id, filter.Query());
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
