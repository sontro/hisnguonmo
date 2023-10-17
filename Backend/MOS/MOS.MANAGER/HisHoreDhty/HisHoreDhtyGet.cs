using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreDhty
{
    partial class HisHoreDhtyGet : BusinessBase
    {
        internal HisHoreDhtyGet()
            : base()
        {

        }

        internal HisHoreDhtyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_HORE_DHTY> Get(HisHoreDhtyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreDhtyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HORE_DHTY GetById(long id)
        {
            try
            {
                return GetById(id, new HisHoreDhtyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HORE_DHTY GetById(long id, HisHoreDhtyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreDhtyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_HORE_DHTY> GetByHoldReturnId(long holdReturnId)
        {
            HisHoreDhtyFilterQuery filter = new HisHoreDhtyFilterQuery();
            filter.HOLD_RETURN_ID = holdReturnId;
            return this.Get(filter);
        }
    }
}
