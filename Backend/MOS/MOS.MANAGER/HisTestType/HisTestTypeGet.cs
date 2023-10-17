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
        internal HisTestTypeGet()
            : base()
        {

        }

        internal HisTestTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TEST_TYPE> Get(HisTestTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTestTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TEST_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisTestTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TEST_TYPE GetById(long id, HisTestTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTestTypeDAO.GetById(id, filter.Query());
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
