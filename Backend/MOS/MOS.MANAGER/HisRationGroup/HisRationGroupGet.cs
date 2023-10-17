using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationGroup
{
    partial class HisRationGroupGet : BusinessBase
    {
        internal HisRationGroupGet()
            : base()
        {

        }

        internal HisRationGroupGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_RATION_GROUP> Get(HisRationGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationGroupDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_GROUP GetById(long id)
        {
            try
            {
                return GetById(id, new HisRationGroupFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_GROUP GetById(long id, HisRationGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationGroupDAO.GetById(id, filter.Query());
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
