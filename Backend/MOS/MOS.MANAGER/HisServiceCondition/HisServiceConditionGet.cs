using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceCondition
{
    partial class HisServiceConditionGet : BusinessBase
    {
        internal HisServiceConditionGet()
            : base()
        {

        }

        internal HisServiceConditionGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_CONDITION> Get(HisServiceConditionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceConditionDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_CONDITION GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceConditionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_CONDITION GetById(long id, HisServiceConditionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceConditionDAO.GetById(id, filter.Query());
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
