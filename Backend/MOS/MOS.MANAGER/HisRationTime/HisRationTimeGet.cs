using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationTime
{
    partial class HisRationTimeGet : BusinessBase
    {
        internal HisRationTimeGet()
            : base()
        {

        }

        internal HisRationTimeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_RATION_TIME> Get(HisRationTimeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationTimeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_TIME GetById(long id)
        {
            try
            {
                return GetById(id, new HisRationTimeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_RATION_TIME> GetById(List<long> ids)
        {
            if (IsNotNullOrEmpty(ids))
            {
                HisRationTimeFilterQuery filter = new HisRationTimeFilterQuery();
                filter.IDs = ids;
                return this.Get(filter);
            }
            return null;
        }

        internal HIS_RATION_TIME GetById(long id, HisRationTimeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationTimeDAO.GetById(id, filter.Query());
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
