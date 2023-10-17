using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodGiver
{
    partial class HisBloodGiverGet : BusinessBase
    {
        internal HisBloodGiverGet()
            : base()
        {

        }

        internal HisBloodGiverGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BLOOD_GIVER> Get(HisBloodGiverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBloodGiverDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BLOOD_GIVER GetById(long id)
        {
            try
            {
                return GetById(id, new HisBloodGiverFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BLOOD_GIVER GetById(long id, HisBloodGiverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBloodGiverDAO.GetById(id, filter.Query());
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
