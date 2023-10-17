using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlanUser
{
    partial class HisEkipPlanUserGet : BusinessBase
    {
        internal HisEkipPlanUserGet()
            : base()
        {

        }

        internal HisEkipPlanUserGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EKIP_PLAN_USER> Get(HisEkipPlanUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanUserDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_EKIP_PLAN_USER> GetByEkipPlanId(long ekipPlanId)
        {
            HisEkipPlanUserFilterQuery filter = new HisEkipPlanUserFilterQuery();
            filter.EKIP_PLAN_ID = ekipPlanId;
            return this.Get(filter);
        }

        internal HIS_EKIP_PLAN_USER GetById(long id)
        {
            try
            {
                return GetById(id, new HisEkipPlanUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EKIP_PLAN_USER GetById(long id, HisEkipPlanUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanUserDAO.GetById(id, filter.Query());
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
