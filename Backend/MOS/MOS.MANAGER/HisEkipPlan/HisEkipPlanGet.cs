using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlan
{
    partial class HisEkipPlanGet : BusinessBase
    {
        internal HisEkipPlanGet()
            : base()
        {

        }

        internal HisEkipPlanGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EKIP_PLAN> Get(HisEkipPlanFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EKIP_PLAN GetById(long id)
        {
            try
            {
                return GetById(id, new HisEkipPlanFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EKIP_PLAN GetById(long id, HisEkipPlanFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanDAO.GetById(id, filter.Query());
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
