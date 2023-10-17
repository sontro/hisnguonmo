using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathGet : BusinessBase
    {
        internal HisEventsCausesDeathGet()
            : base()
        {

        }

        internal HisEventsCausesDeathGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EVENTS_CAUSES_DEATH> Get(HisEventsCausesDeathFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEventsCausesDeathDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EVENTS_CAUSES_DEATH GetById(long id)
        {
            try
            {
                return GetById(id, new HisEventsCausesDeathFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EVENTS_CAUSES_DEATH GetById(long id, HisEventsCausesDeathFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEventsCausesDeathDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_EVENTS_CAUSES_DEATH> GetBySevereIllnessInfoId(long severeIllnessInfoId)
        {
            try
            {
                HisEventsCausesDeathFilterQuery filter = new HisEventsCausesDeathFilterQuery();
                filter.SEVERE_ILLNESS_INFO_ID = severeIllnessInfoId;
                return this.Get(filter);
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
