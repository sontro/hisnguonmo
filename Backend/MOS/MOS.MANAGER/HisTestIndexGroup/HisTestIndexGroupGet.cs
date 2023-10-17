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
        internal HisTestIndexGroupGet()
            : base()
        {

        }

        internal HisTestIndexGroupGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TEST_INDEX_GROUP> Get(HisTestIndexGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTestIndexGroupDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TEST_INDEX_GROUP GetById(long id)
        {
            try
            {
                return GetById(id, new HisTestIndexGroupFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TEST_INDEX_GROUP GetById(long id, HisTestIndexGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTestIndexGroupDAO.GetById(id, filter.Query());
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
