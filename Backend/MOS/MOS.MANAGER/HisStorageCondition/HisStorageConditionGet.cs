using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStorageCondition
{
    partial class HisStorageConditionGet : BusinessBase
    {
        internal HisStorageConditionGet()
            : base()
        {

        }

        internal HisStorageConditionGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_STORAGE_CONDITION> Get(HisStorageConditionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStorageConditionDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STORAGE_CONDITION GetById(long id)
        {
            try
            {
                return GetById(id, new HisStorageConditionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STORAGE_CONDITION GetById(long id, HisStorageConditionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStorageConditionDAO.GetById(id, filter.Query());
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
